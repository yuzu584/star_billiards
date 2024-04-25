using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

// �{�^���̐e�N���X
public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public KeyGuide.KeyGuideIconAndTextType[] keyGuideTypes;

    // �{�^������������O���[�v
    public enum Group
    {
        None,
        SkillList,
    }

    // �{�^���̉����t�@�C���̗񋓌^
    public enum BtnSounds
    {
        EnterSound,
        ClickSound,
    }

    // instance��������ϐ�
    protected ScreenController scrCon;
    protected Sound sound;
    protected InputController input;
    protected Focus focus;
    protected ButtonRecorder btnRec;
    protected KeyGuideUI keyGuideUI;

    // �X�N���[���ƊK�w���܂Ƃ߂��\����
    [System.Serializable]
    public struct ScreenAndLoot
    {
        public ScreenController.ScreenType scrType;
        public int scrLoot;
    }

    [SerializeField] protected bool defaultFocus = false;   // �ŏ��Ƀt�H�[�J�X����{�^����
    public ScreenAndLoot scrAndLoot;                        // �X�N���[���ƊK�w���܂Ƃ߂��\����
    public AudioClip EnterSound;                            // �|�C���^�[����������ɍĐ����鉹���t�@�C��
    public AudioClip ClickSound;                            // �{�^���N���b�N���ɍĐ����鉹���t�@�C��
    public Button buttonUp;                                 // �����̏�Ɉʒu����{�^��
    public Button buttonDown;                               // �����̉��Ɉʒu����{�^��
    public Button buttonLeft;                               // �����̍��Ɉʒu����{�^��
    public Button buttonRight;                              // �����̉E�Ɉʒu����{�^��
    public Group group;                                     // ���̃{�^������������O���[�v
    public int btnNum;                                      // ���̃{�^���̔ԍ�(��Ƀt�H�[�J�X���Ă����{�^����ۑ����邽�߂Ɏg�p����)

    protected bool isStartFocus = false;                    // StartFocus �����s���ꂽ��
    protected bool animating = false;                       // �{�^���̃A�j���[�V��������

    protected UILerper uiLerper;

    // �|�C���^�[�ɂ���ăt�H�[�J�X���ꂽ��
    // true  : �|�C���^�[�ɂ���ăt�H�[�J�X���ꂽ
    // false : �|�C���^�[�ȊO(�R���g���[���[���͂Ȃ�)�Ńt�H�[�J�X���ꂽ
    public bool orPointer = false;

    // �}�E�X�|�C���^�[���{�^���̏�ɏ������
    public virtual void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �|�C���^�[�ɂ���ăt�H�[�J�X���ꂽ
        orPointer = true;

        EnterProcess();

        orPointer = false;
    }

    // �}�E�X�|�C���^�[���{�^���̏ォ�痣�ꂽ��
    public virtual void OnPointerExit(PointerEventData pointerEventData)
    {
        //ExitProcess();
    }

    // �{�^�����N���b�N���ꂽ��
    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        ClickProcess();
    }

    // �}�E�X�|�C���^�[����������̏���
    public virtual void EnterProcess()
    {
        // �����Đ�
        PlayBtnSound(BtnSounds.EnterSound);

        // �{�^���̃A�j���[�V��������
        if((uiLerper != null) && (!animating)) uiLerper.AnimProcess(true);
        animating = true;

        // �t�H�[�J�X����Ă���{�^����ݒ�
        focus.SetFocusBtn(this);

        DrawKeyGuide();
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    public virtual void ExitProcess()
    {
        // �{�^���̃A�j���[�V��������
        if ((uiLerper != null) && (animating)) uiLerper.AnimProcess(false);
        animating = false;
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    public virtual void ClickProcess()
    {
        //�����Đ�
        PlayBtnSound(BtnSounds.ClickSound);
    }

    // �{�^���̉����Đ�
    public void PlayBtnSound(BtnSounds btnSounds)
    {
        sound ??= Sound.instance;

        // �Đ����鉹�ɂ���ĕ���
        switch (btnSounds)
        {
            case BtnSounds.EnterSound:
                sound.Play(EnterSound);
                break;
            case BtnSounds.ClickSound:
                sound.Play(ClickSound);
                break;
        }
    }

    // �t�H�[�J�X�֌W�̏���
    public void FocusProcess(bool isEnter)
    {
        if (gameObject.activeInHierarchy)
        {
            // �|�C���^�[������ăt�H�[�J�X���ꂽ�ۂ� EnterProcess ���s�ς݂Ȃ̂�
            // �|�C���^�[������Ė����Ƃ��̂� EnterProcess �����s
            if ((isEnter) && (!orPointer))
            {
                EnterProcess();
            }
            else if(!orPointer)
            {
                ExitProcess();
            }
        }
    }

    // �ŏ��̃t�H�[�J�X����
    void StartFocus()
    {
        // �ŏ��̃t�H�[�J�X���������s�ς݂Ȃ�I��
        if (isStartFocus) return;
        
        isStartFocus = true;

        // ���̃{�^�����ۑ��ς݂�
        bool thisSaved = (btnRec.savedBtn[(int)scrCon.Screen].num[scrCon.ScreenLoot] == btnNum);

        // ���̃{�^�����ۑ�����Ă��Ȃ���(�ۑ�����Ă���� -1 �ȊO�ɂȂ��Ă���)
        bool somethingSaved = (btnRec.savedBtn[(int)scrCon.Screen].num[scrCon.ScreenLoot] == -1);

        // �{�^���ԍ����t�H�[�J�X�\�Ȕԍ���( -1 �������Ă�����t�H�[�J�X�s��)
        bool canFocus = (btnNum != -1);

        // ���̃{�^�����ŏ��̃{�^����(btnNum �͘A�ԂŐݒ肳��Ă���̂� 0 �������Ă���΍ŏ��̃{�^���Ƃ������ƂɂȂ�)
        bool orFirst = (btnNum == 0);

        // �ŏ��Ƀt�H�[�J�X�����{�^���Ȃ�(�t�H�[�J�X�̗D��x : ��)
        if (defaultFocus)
        {
            // �t�H�[�J�X
            focus.SetFocusBtn(this);

            // �t�H�[�J�X���̃f�o�b�O�������L���Ȃ當������o��
            if (focus.focusDebugs.buttonStartFocusLog)
                Debug.Log($"type[{GetType()}] name[{name}] ���f�t�H���g�Ńt�H�[�J�X");
        }
        // �{�^�����ۑ��ς݂��� btnNum ���ݒ�ς݂Ȃ�t�H�[�J�X(�t�H�[�J�X�̗D��x : ��)
        else if ((thisSaved) && (canFocus))
        {
            // �������L�^
            focus.SetFocusBtn(this);

            // �t�H�[�J�X���̃f�o�b�O�������L���Ȃ當������o��
            if (focus.focusDebugs.buttonStartFocusLog)
                Debug.Log($"type[{GetType()}] name[{name}] ��ۑ��ς݂Ńt�H�[�J�X");
        }
        // ���̃{�^�����ۑ�����Ă��Ȃ�����ԍŏ��̃{�^���Ȃ�t�H�[�J�X(�t�H�[�J�X�̗D��x : ��)
        else if ((somethingSaved) && (orFirst))
        {
            // �t�H�[�J�X
            focus.SetFocusBtn(this);

            // �t�H�[�J�X���̃f�o�b�O�������L���Ȃ當������o��
            if (focus.focusDebugs.buttonStartFocusLog)
                Debug.Log($"type[{GetType()}] name[{name}] ���{�^���ԍ�0�Ńt�H�[�J�X");
        }
    }

    // �L�[����K�C�h��UI��`��
    void DrawKeyGuide()
    {
        keyGuideUI ??= KeyGuideUI.instance;
        keyGuideUI.DrawGuide(keyGuideTypes);
    }

    protected virtual void Start()
    {
        scrCon ??= ScreenController.instance;
        focus ??= Focus.instance;
        sound ??= Sound.instance;
        input = InputController.instance;
        btnRec ??= ButtonRecorder.instance;
        keyGuideUI ??= KeyGuideUI.instance;

        // StartFocus ���K�w�J�ڎ��Ɉ�񂾂����s
        scrCon.changeLoot += StartFocus;

        // �ŏ��̃t�H�[�J�X����
        StartFocus();
    }

    protected virtual void OnEnable()
    {
        uiLerper ??= GetComponent<UILerper>();

        // �擾����Ă��Ȃ���Ύ擾
        scrCon ??= ScreenController.instance;
        focus ??= Focus.instance;
        btnRec ??= ButtonRecorder.instance;

        // UI�v�f�̏���������
        if (uiLerper != null) uiLerper.Init();
    }

    protected virtual void OnDestroy()
    {
        scrCon.changeLoot -= StartFocus;
    }
}
