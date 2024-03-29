using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// �{�^���̐e�N���X
public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // ���`�⊮�ŐF��ύX���鎞�Ɏg�p����ϐ��̍\����
    [System.Serializable]
    protected struct LerpColor
    {
        public bool useLerp;     // ���`�⊮�ŐF��ύX���邩
        public Color startColor; // �ω��O�̐F
        public Color endColor;   // �ω���̐F
        public float fadeTime;   // �t�F�[�h����
    }

    // ���`�⊮�ō��W��ύX���鎞�Ɏg�p����ϐ��̍\����
    [System.Serializable]
    protected struct LerpPosition
    {
        public bool useLerp;     // ���`�⊮�ō��W��ύX���邩
        public Vector3 startPos; // �ω��O�̍��W
        public Vector3 endPos;   // �ω���̍��W
        public float fadeTime;   // �t�F�[�h����
    }

    // ���`�⊮�ŃX�P�[����ύX���鎞�Ɏg�p����ϐ��̍\����
    [System.Serializable]
    protected struct LerpScale
    {
        public bool useLerp;       // ���`�⊮�ŃX�P�[����ύX���邩
        public Vector2 startScale; // �ω��O�̃X�P�[��
        public Vector2 endScale;   // �ω���̃X�P�[��
        public float fadeTime;     // �t�F�[�h����
    }

    [System.Serializable]
    protected struct ImageStruct // �摜�̍\����
    {
        public Image image;               // �摜
        public LerpColor lerpColor;       // ���`�⊮�ŐF��ύX���鎞�Ɏg�p����ϐ��̍\����
        public LerpPosition lerpPosition; // ���`�⊮�ō��W��ύX���鎞�Ɏg�p����ϐ��̍\����
        public LerpScale lerpScale;       // ���`�⊮�ŃX�P�[����ύX���鎞�Ɏg�p����ϐ��̍\����
        public bool onPointerDraw;        // �|�C���^�[������Ă���Ƃ��̂ݕ`�悷�邩
    }

    [System.Serializable]
    protected struct TextStruct  // �e�L�X�g�̍\����
    {
        public Text text;                 // �e�L�X�g
        public LerpColor lerpColor;       // ���`�⊮�ŐF��ύX���鎞�Ɏg�p����ϐ��̍\����
        public LerpPosition lerpPosition; // ���`�⊮�ō��W��ύX���鎞�Ɏg�p����ϐ��̍\����
        public LerpScale lerpScale;       // ���`�⊮�ŃX�P�[����ύX���鎞�Ɏg�p����ϐ��̍\����
        public bool onPointerDraw;        // �|�C���^�[������Ă���Ƃ��̂ݕ`�悷�邩
    }

    [SerializeField] protected ImageStruct[] imageStructs;
    [SerializeField] protected TextStruct[] textStructs;

    // �{�^������������O���[�v
    public enum Group
    {
        None,
        SkillList,
    }

    // instance��������ϐ�
    protected ScreenController scrCon;
    protected Sound sound;
    protected InputController input;
    protected Focus focus;
    protected ButtonRecorder btnRec;

    protected Lerp lerp;

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

    public bool isStartFocus = false;                       // StartFocus �����s���ꂽ��

    // �|�C���^�[�ɂ���ăt�H�[�J�X���ꂽ��
    // true  : �|�C���^�[�ɂ���ăt�H�[�J�X���ꂽ
    // false : �|�C���^�[�ȊO(�R���g���[���[���͂Ȃ�)�Ńt�H�[�J�X���ꂽ
    public bool orPointer = false;

    // �}�E�X�|�C���^�[���{�^���̏�ɏ������
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �|�C���^�[�ɂ���ăt�H�[�J�X���ꂽ
        orPointer = true;

        // �����Đ�
        if (sound != null)
            StartCoroutine(sound.Play(EnterSound));

        // �t�H�[�J�X����Ă���{�^����ݒ�
        focus.SetFocusBtn(this);

        EnterProcess();

        orPointer = false;
    }

    // �}�E�X�|�C���^�[���{�^���̏ォ�痣�ꂽ��
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        ExitProcess();
    }

    // �{�^�����N���b�N���ꂽ��
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //�����Đ�
        if (sound != null)
            StartCoroutine(sound.Play(ClickSound));

        ClickProcess();
    }

    // �}�E�X�|�C���^�[����������̏���
    public virtual void EnterProcess()
    {
        Debug.Log("�|�C���^�[����������̏������ݒ肳��Ă��܂���B");
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    public virtual void ExitProcess()
    {
        Debug.Log("�|�C���^�[�����ꂽ���̏������ݒ肳��Ă��܂���B");
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    public virtual void ClickProcess()
    {
        Debug.Log("�N���b�N���̏������ݒ肳��Ă��܂���B");
    }

    // �{�^���̃A�j���[�V��������
    protected void BtnAnimProcess(ImageStruct[] childrenImageStructs, TextStruct[] childrenTextStructs, bool enterOrExit)
    {
        // Lerp �R���|�[�l���g�� null �Ȃ�擾
        lerp ??= gameObject.AddComponent<Lerp>();

        Color color1;
        Color color2;
        Vector3 pos1;
        Vector3 pos2;
        Vector2 scale1;
        Vector2 scale2;

        lerp.StopAll();

        // �|�C���^�[������Ă���Ƃ��̂ݕ`�悷��Ȃ�`��
        for (int i = 0; i < childrenImageStructs.Length; i++)
        {
            if ((childrenImageStructs[i].onPointerDraw) && (childrenImageStructs[i].image.enabled != enterOrExit))
                childrenImageStructs[i].image.enabled = enterOrExit;
        }
        for (int i = 0; i < childrenTextStructs.Length; i++)
        {
            if ((childrenTextStructs[i].onPointerDraw) && (childrenTextStructs[i].text.enabled != enterOrExit))
                childrenTextStructs[i].text.enabled = enterOrExit;
        }

        // �摜�̃A�j���[�V����
        for (int i = 0; i < childrenImageStructs.Length; i++)
        {
            // �F�̐��`�⊮���g�p����Ȃ�
            if (childrenImageStructs[i].lerpColor.useLerp)
            {
                // �|�C���^�[��������Ƃ������ꂽ�Ƃ����ŕ���
                if (enterOrExit)
                {
                    color1 = childrenImageStructs[i].lerpColor.startColor;
                    color2 = childrenImageStructs[i].lerpColor.endColor;
                }
                else
                {
                    color1 = childrenImageStructs[i].image.color;
                    color2 = childrenImageStructs[i].lerpColor.startColor;
                }

                // ���`�⊮
                StartCoroutine(lerp.Color_Image(childrenImageStructs[i].image, color1, color2, childrenImageStructs[i].lerpColor.fadeTime));
            }

            // ���W�̐��`�⊮���g�p����Ȃ�
            if (childrenImageStructs[i].lerpPosition.useLerp)
            {
                // �|�C���^�[��������Ƃ������ꂽ�Ƃ����ŕ���
                if (enterOrExit)
                {
                    pos1 = childrenImageStructs[i].lerpPosition.startPos;
                    pos2 = childrenImageStructs[i].lerpPosition.endPos;
                }
                else
                {
                    pos1 = childrenImageStructs[i].image.rectTransform.anchoredPosition;
                    pos2 = childrenImageStructs[i].lerpPosition.startPos;
                }

                // ���`�⊮
                StartCoroutine(lerp.Position_Image(childrenImageStructs[i].image, pos1, pos2, childrenImageStructs[i].lerpPosition.fadeTime));
            }

            // �X�P�[���̐��`�⊮���g�p����Ȃ�
            if (childrenImageStructs[i].lerpScale.useLerp)
            {
                // �|�C���^�[��������Ƃ������ꂽ�Ƃ����ŕ���
                if (enterOrExit)
                {
                    scale1 = childrenImageStructs[i].lerpScale.startScale;
                    scale2 = childrenImageStructs[i].lerpScale.endScale;
                }
                else
                {
                    scale1 = childrenImageStructs[i].image.rectTransform.localScale;
                    scale2 = childrenImageStructs[i].lerpScale.startScale;
                }

                // ���`�⊮
                StartCoroutine(lerp.Scale_Image(childrenImageStructs[i].image, scale1, scale2, childrenImageStructs[i].lerpScale.fadeTime));
            }
        }

        // �e�L�X�g�̃A�j���[�V����
        for (int i = 0; i < childrenTextStructs.Length; i++)
        {
            // �F�̐��`�⊮���g�p����Ȃ�
            if (childrenTextStructs[i].lerpColor.useLerp)
            {
                // �|�C���^�[��������Ƃ������ꂽ�Ƃ����ŕ���
                if (enterOrExit)
                {
                    color1 = childrenTextStructs[i].lerpColor.startColor;
                    color2 = childrenTextStructs[i].lerpColor.endColor;
                }
                else
                {
                    color1 = childrenTextStructs[i].text.color;
                    color2 = childrenTextStructs[i].lerpColor.startColor;
                }

                // ���`�⊮
                StartCoroutine(lerp.Color_Text(childrenTextStructs[i].text, color1, color2, childrenTextStructs[i].lerpColor.fadeTime));
            }

            // ���W�̐��`�⊮���g�p����Ȃ�
            if (childrenTextStructs[i].lerpPosition.useLerp)
            {
                // �|�C���^�[��������Ƃ������ꂽ�Ƃ����ŕ���
                if (enterOrExit)
                {
                    pos1 = childrenTextStructs[i].lerpPosition.startPos;
                    pos2 = childrenTextStructs[i].lerpPosition.endPos;
                }
                else
                {
                    pos1 = childrenTextStructs[i].text.rectTransform.anchoredPosition;
                    pos2 = childrenTextStructs[i].lerpPosition.startPos;
                }

                // ���`�⊮
                StartCoroutine(lerp.Position_Text(childrenTextStructs[i].text, pos1, pos2, childrenTextStructs[i].lerpPosition.fadeTime));
            }

            // �X�P�[���̐��`�⊮���g�p����Ȃ�
            if (childrenTextStructs[i].lerpScale.useLerp)
            {
                // �|�C���^�[��������Ƃ������ꂽ�Ƃ����ŕ���
                if (enterOrExit)
                {
                    scale1 = childrenTextStructs[i].lerpScale.startScale;
                    scale2 = childrenTextStructs[i].lerpScale.endScale;
                }
                else
                {
                    scale1 = childrenTextStructs[i].text.rectTransform.localScale;
                    scale2 = childrenTextStructs[i].lerpScale.startScale;
                }

                // ���`�⊮
                StartCoroutine(lerp.Scale_Text(childrenTextStructs[i].text, scale1, scale2, childrenTextStructs[i].lerpScale.fadeTime));
            }
        }
    }

    // �{�^���̏���������
    protected void BtnInit(ImageStruct[] childrenImageStructs, TextStruct[] childrenTextStructs)
    {
        // �{�^���̗v�f�����Z�b�g
        // ���`�⊮�A�j���[�V�������g�p����摜�̐��J��Ԃ�
        for (int i = 0; i < childrenImageStructs.Length; i++)
        {
            // �摜�̐F�����Z�b�g
            if (childrenImageStructs[i].lerpColor.useLerp)
                childrenImageStructs[i].image.color = childrenImageStructs[i].lerpColor.startColor;

            // �摜�̍��W�����Z�b�g
            if (childrenImageStructs[i].lerpPosition.useLerp)
                childrenImageStructs[i].image.rectTransform.position = childrenImageStructs[i].lerpPosition.startPos;

            // �摜�̃X�P�[�������Z�b�g
            if (childrenImageStructs[i].lerpScale.useLerp)
                childrenImageStructs[i].image.rectTransform.localScale = childrenImageStructs[i].lerpScale.startScale;

            // �|�C���^�[������Ă���Ƃ��̂ݕ`�悷��Ȃ��\��
            if ((childrenImageStructs[i].onPointerDraw) && (childrenImageStructs[i].image.enabled))
                childrenImageStructs[i].image.enabled = false;
        }
        // ���`�⊮�A�j���[�V�������g�p����e�L�X�g�̐��J��Ԃ�
        for (int i = 0; i < childrenTextStructs.Length; i++)
        {
            // �e�L�X�g�̐F�����Z�b�g
            if (childrenTextStructs[i].lerpColor.useLerp)
                childrenTextStructs[i].text.color = childrenTextStructs[i].lerpColor.startColor;

            // �e�L�X�g�̍��W�����Z�b�g
            if (childrenTextStructs[i].lerpPosition.useLerp)
                childrenTextStructs[i].text.rectTransform.anchoredPosition = childrenTextStructs[i].lerpPosition.startPos;

            // �e�L�X�g�̃X�P�[�������Z�b�g
            if (childrenTextStructs[i].lerpScale.useLerp)
                childrenTextStructs[i].text.rectTransform.localScale = childrenTextStructs[i].lerpScale.startScale;

            // �|�C���^�[������Ă���Ƃ��̂ݕ`�悷��Ȃ��\��
            if ((childrenTextStructs[i].onPointerDraw) && (childrenTextStructs[i].text.enabled))
                childrenTextStructs[i].text.enabled = false;
        }
    }

    // �t�H�[�J�X�֌W�̏���
    public void FocusProcess(bool isEnter)
    {
        if (gameObject.activeInHierarchy)
        {
            if (isEnter)
            {
                // �����Đ�
                if(sound != null)
                    StartCoroutine(sound.Play(EnterSound));

                EnterProcess();
            }
            else
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

        // �{�^�����ۑ��ς݂��� btnNum ���ݒ�ς݂Ȃ�t�H�[�J�X(�t�H�[�J�X�̗D��x : ��)
        if ((thisSaved) && (canFocus))
        {
            // �������L�^
            focus.SetFocusBtn(this);
        }
        // ���̃{�^�����ۑ�����Ă��Ȃ�����ԍŏ��̃{�^���Ȃ�t�H�[�J�X(�t�H�[�J�X�̗D��x : ��)
        else if ((somethingSaved) && (orFirst))
        {
            // �t�H�[�J�X
            focus.SetFocusBtn(this);
        }

        // �ŏ��Ƀt�H�[�J�X�����{�^���Ȃ�(�t�H�[�J�X�̗D��x : ��)
        if (defaultFocus)
        {
            // �t�H�[�J�X
            focus.SetFocusBtn(this);
        }
    }

    protected virtual void Start()
    {
        lerp ??= gameObject.AddComponent<Lerp>();

        scrCon ??= ScreenController.instance;
        focus ??= Focus.instance;
        sound = Sound.instance;
        input = InputController.instance;
        btnRec ??= ButtonRecorder.instance;

        // StartFocus ���K�w�J�ڎ��Ɉ�񂾂����s
        scrCon.changeLoot += StartFocus;

        // �ŏ��̃t�H�[�J�X����
        StartFocus();
    }

    protected virtual void OnEnable()
    {
        // �擾����Ă��Ȃ���Ύ擾
        scrCon ??= ScreenController.instance;
        focus ??= Focus.instance;
        btnRec ??= ButtonRecorder.instance;

        // �{�^���̏���������
        BtnInit(imageStructs, textStructs);
    }

    protected virtual void OnDestroy()
    {
        scrCon.changeLoot -= StartFocus;
    }
}
