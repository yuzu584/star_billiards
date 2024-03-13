using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;
using UnityEngine.UI;

// ��ʂ̎�ނ��Ǘ�
public class ScreenController : Lerp
{
    [SerializeField] private UIController uIController;                 // Inspector��UIController���w��
    [SerializeField] private StageController stageController;           // Inspector��StageController���w��
    [SerializeField] private PauseUIController pauseUIController;       // Inspector��PauseUIController���w��
    [SerializeField] private ScreenData screenData;                     // Inspector��ScreenData���w��
    [SerializeField] private InputController input;                     // Inspector��InputController���w��
    [SerializeField] private Sound sound;                               // Inspector��Sound���w��
    [SerializeField] private Image switchImage;                         // ��ʑJ�ڎ��̉摜

    [System.NonSerialized] public bool canStageDraw = false;            // �X�e�[�W��`��\��
    [System.NonSerialized] public int oldScreenNum = 0;                 // �O��̉�ʔԍ�
    [System.NonSerialized] public int oldScreenLoot = 0;                // �O��̊K�w
    [System.NonSerialized] public int oldFrameScreenNum = 0;            // 1�t���[���O�̉�ʔԍ�
    [System.NonSerialized] public int oldFrameScreenLoot = 0;           // 1�t���[���O�̊K�w

    // UI���`��\�����Ǘ�����z��
    // 0 : �^�C�g�����
    // 1 : ���C�����j���[
    // 2 : �X�e�[�W�I�����
    // 3 : �ݒ���
    // 4 : �X�L���I�����
    // 5 : �Q�[�����
    // 6 : �|�[�Y���
    // 7 : 
    // 8 : �X�e�[�W�N���A���
    // 9 : �Q�[���I�[�o�[���

    [SerializeField] private int screenNum;  // ��ʔԍ�
    [SerializeField] private int screenLoot; // ��ʂ̊K�w
    public int ScreenNum                     // ��ʔԍ��̃v���p�e�B
    {
        get { return screenNum; }
        set { SwitchProcess(value); }
    }
    public int ScreenLoot                    // ��ʂ̊K�w�̃v���p�e�B
    {
        get { return screenLoot; }
        set { screenLoot = value; }
    }
    public delegate void ChangeScreen();     // ��ʂ��J�ڂ����Ƃ��̃f���Q�[�g
    public delegate void ChangeLoot();       // �K�w���J�ڂ����Ƃ��̃f���Q�[�g
    public ChangeScreen changeScreen;
    public ChangeScreen changeLoot;
    public Button focusBtn;                  // �t�H�[�J�X���Ă���{�^��
    public Button oldfocusBtn;               // �t�H�[�J�X����Ă����{�^��
    public Scrollbar focusScrollbar;         // �t�H�[�J�X���Ă���X�N���[���o�[

    private bool changeStageClearScreen = false; // �X�e�[�W�N���A��ʂɑJ�ڂ������ǂ���
    
    // ��ʑJ�ڏ���
    private void SwitchProcess(int num)
    {
        // �J�ڐ�̉�ʂ܂��͌��݂̉�ʂ��J�ڎ��ɃA�j���[�V�������s�����ƂɂȂ��Ă�����
        if ((screenData.screenList[num].enterAnim) || (screenData.screenList[screenNum].exitAnim))
        {
            // �A�j���[�V�������s��
            StopAll();

            StartCoroutine(SetScreenNum(num, true));
        }
        else
        {
            StartCoroutine(SetScreenNum(num, false));
        }
    }

    // ��ʔԍ���ݒ�
    private IEnumerator SetScreenNum(int num, bool orPlay)
    {
        if (orPlay)
        {
            // ��ʑJ�ڃA�j���[�V����
            input.DisableInputs();                           // InputSystem�̓��͂𖳌���
            Color c1 = new Color(0, 0, 0, 0);                // ����
            Color c2 = new Color(0, 0, 0, 1);                // ��
            switchImage.gameObject.SetActive(true);          // ��ʃT�C�Y��Image��L����
            switchImage.raycastTarget = true;                // ��ʃT�C�Y��Image�ɔ����t�^���ă}�E�X���͂��󂯕t���Ȃ��悤��
            yield return StartCoroutine(SwitchAnim(c1, c2)); // �A�j���[�V�������I���܂ő҂�

            // ��u�҂�
            yield return new WaitForSecondsRealtime(0.2f);
        }

        // ��ʔԍ���ݒ�
        screenNum = num;
        
        if(orPlay)
        {
            // ��ʑJ�ڃA�j���[�V����
            Color c1 = new Color(0, 0, 0, 0);                // ����
            Color c2 = new Color(0, 0, 0, 1);                // ��
            switchImage.raycastTarget = false;               // ��ʃT�C�Y��Image�ɔ���������ă}�E�X���͂��󂯕t����悤��
            yield return StartCoroutine(SwitchAnim(c2, c1)); // �A�j���[�V�������I���܂ő҂�
            switchImage.gameObject.SetActive(false);         // ��ʃT�C�Y��Image�𖳌���
        }
    }

    // ��ʑJ�ڎ��̃A�j���[�V����
    private IEnumerator SwitchAnim(Color c1, Color c2)
    {
        yield return StartCoroutine(Color_Image(switchImage, c1, c2, 0.5f));
    }

    void Start()
    {
        input.game_OnPauseDele += OpenPause;
        input.ui_OnMoveDele += ChangeBtnFocus;
        input.ui_OnMoveDele += MoveSlider;

        // UI_Positive���͎��̃C�x���g��o�^
        input.ui_OnPositiveDele += (float value) =>
        {
            // �����Đ�
            StartCoroutine(sound.Play(focusBtn.ClickSound));

            // �{�^���N���b�N���̏���
            focusBtn.ClickProcess();
        };

        // UI_Negative���͎��̃C�x���g��o�^
        input.ui_OnNegativeDele += (float value) =>
        {
            // �K�w��0����Ȃ�1������
            if (ScreenLoot > 0)
                ScreenLoot -= (int)value;

            // ���C�����j���[�Ȃ�^�C�g����ʂɖ߂�
            else if (ScreenNum == 1)
            {
                ScreenNum = 0;
            }
        };
    }

    void Update()
    {
        // �O��̃t���[���ƌ��݂̃t���[���ŉ�ʔԍ����قȂ�����
        if (screenNum != oldFrameScreenNum)
        {
            // �O��̉�ʔԍ���ۑ�
            oldScreenNum = oldFrameScreenNum;

            // 1�t���[���O�̉�ʔԍ��Ɍ��݂̉�ʔԍ�����
            oldFrameScreenNum = screenNum;

            // ��ʑJ�ڂ����Ƃ��̏���
            if(changeScreen !=  null)
                changeScreen();
        }

        // �O��̃t���[���ƌ��݂̃t���[���ŊK�w���قȂ�����
        if (ScreenLoot != oldFrameScreenLoot)
        {
            // �O��̊K�w��ۑ�
            oldScreenLoot = oldFrameScreenLoot;

            // 1�t���[���O�̊K�w�Ɍ��݂̊K�w����
            oldFrameScreenLoot = ScreenLoot;

            // �K�w���J�ڂ����Ƃ��̏���
            if (changeLoot != null)
                changeLoot();
        }

        // �X�e�[�W���N���A����ʑJ�ڂ��Ă��Ȃ��Ȃ�
        if ((stageController.stageCrear) && (!changeStageClearScreen))
        {
            // �X�e�[�W�N���A��ʂɑJ�ڍς�
            changeStageClearScreen = true;

            // �X�e�[�W�N���A��ʂɑJ��
            screenNum = 8;
        }
        // �X�e�[�W���N���A����ʑJ�ڂ����Ȃ�
        else if ((!stageController.stageCrear) && (changeStageClearScreen))
        {
            // �X�e�[�W�N���A��ʂɖ��J��
            changeStageClearScreen = false;
        }

        // �X�e�[�W���`��\�����Ǘ�����z����X�V
        if (canStageDraw != screenData.screenList[screenNum].drawStage)
            canStageDraw = screenData.screenList[screenNum].drawStage;
    }

    // �|�[�Y��ʂɑJ��
    void OpenPause(float value)
    {
        if (value > 0)
            screenNum = 6;
    }

    // �t�H�[�J�X����{�^����ς���
    void ChangeBtnFocus(Vector2 mVec)
    {
        float minInput = 0.5f; // ���͂��󂯕t����Œ�l

        if(focusBtn != null)
        {
            if ((mVec.x > minInput) && (focusBtn.buttonRight != null))
            {
                SetFocusBtn(focusBtn.buttonRight);
            }
            else if ((mVec.x < -minInput) && (focusBtn.buttonLeft != null))
            {
                SetFocusBtn(focusBtn.buttonLeft);
            }
            else if ((mVec.y < -minInput) && (focusBtn.buttonDown != null))
            {
                SetFocusBtn(focusBtn.buttonDown);
            }
            else if ((mVec.y > minInput) && (focusBtn.buttonUp != null))
            {
                SetFocusBtn(focusBtn.buttonUp);
            }
        }
    }

    // �X���C�_�[�𓮂���(�{�^����)
    void MoveSlider(Vector2 mVec)
    {
        // �t�H�[�J�X����Ă���{�^������OptionsSlider���擾�o������
        var sliderBtn = focusBtn.gameObject.GetComponent<OptionsSlider>();
        if (sliderBtn != null)
            sliderBtn.MoveSlider(mVec.x);
    }

    // �t�H�[�J�X����{�^����ݒ�
    public void SetFocusBtn(Button btn)
    {
        // �O��t�H�[�J�X����Ă����{�^���ƈقȂ�΃Z�b�g����
        if(btn != focusBtn)
        {
            oldfocusBtn = focusBtn;
            focusBtn = btn;

            // �t�H�[�J�X���ꂽ�Ƃ��̏���
            if(focusBtn != null)
                focusBtn.FocusProcess(true);

            // �t�H�[�J�X���O�ꂽ�Ƃ��̏���
            if(oldfocusBtn != null)
                oldfocusBtn.FocusProcess(false);
        }

        // �X�N���[���o�[�̃X�N���[������
        // �X�N���[�����K�v�ȍ��W���v�Z
        if(focusScrollbar != null)
        {
            float pos;
            var instance = ScrollBarController.instance;
            int num = instance.num;
            pos = instance.scrollBarStruct[num].rTransform.sizeDelta.y / 2;
            pos += instance.scrollBarStruct[num].rTransform.localPosition.y;

            // �t�H�[�J�X�����{�^�������؂ꂻ���ȍ��W�Ȃ�X�N���[��
            if (focusBtn.gameObject.transform.localPosition.y > pos)
                instance.Scroll(focusScrollbar, true);

            pos = -(instance.scrollBarStruct[num].rTransform.sizeDelta.y / 2);
            pos += instance.scrollBarStruct[num].rTransform.localPosition.y;
            if (focusBtn.gameObject.transform.localPosition.y < pos)
                instance.Scroll(focusScrollbar, false);
        }
    }
}
