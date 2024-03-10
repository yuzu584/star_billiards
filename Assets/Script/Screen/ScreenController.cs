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
    [SerializeField] private Image switchImage;                         // ��ʑJ�ڎ��̉摜

    [System.NonSerialized] public bool canStageDraw = false;    // �X�e�[�W��`��\��
    [System.NonSerialized] public int oldScreenNum = 0;         // �O��̉�ʔԍ�
    [System.NonSerialized] public int oldFrameScreenNum = 0;    // 1�t���[���O�̉�ʔԍ�

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

    private int screenNum;               // ��ʔԍ�
    private int screenLoot;              // ��ʂ̊K�w
    public int ScreenNum                 // ��ʔԍ��̃v���p�e�B
    {
        get { return screenNum; }
        set { SwitchProcess(value); }
    }
    public int ScreenLoot                // ��ʂ̊K�w�̃v���p�e�B
    {
        get { return screenLoot; }
        set { screenLoot = value; }
    }
    public delegate void ChangeScreen(); // ��ʂ��J�ڂ����Ƃ��̃f���Q�[�g
    public ChangeScreen changeScreen;
    public Button focusBtn;          // �t�H�[�J�X���Ă���{�^��
    public Button oldfocusBtn;       // �t�H�[�J�X����Ă����{�^��
    public bool canMoveFocus = true; // �t�H�[�J�X�Ώۂ�ύX�ł��邩

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
            canMoveFocus = false;
            Color c1 = new Color(0, 0, 0, 0);
            Color c2 = new Color(0, 0, 0, 1);
            switchImage.gameObject.SetActive(true);
            switchImage.raycastTarget = true;
            yield return StartCoroutine(SwitchAnim(c1, c2));

            // ��u�҂�
            yield return new WaitForSecondsRealtime(0.2f);
        }

        // ��ʔԍ���ݒ�
        screenNum = num;
        
        if(orPlay)
        {
            // ��ʑJ�ڃA�j���[�V����
            canMoveFocus = true;
            Color c1 = new Color(0, 0, 0, 0);
            Color c2 = new Color(0, 0, 0, 1);
            switchImage.raycastTarget = false;
            yield return StartCoroutine(SwitchAnim(c2, c1));
            switchImage.gameObject.SetActive(false);
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
            changeScreen();
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
        if((focusBtn != null) && (canMoveFocus))
        {
            if ((mVec.x > 0) && (focusBtn.buttonRight != null))
                SetFocusBtn(focusBtn.buttonRight);

            if ((mVec.x < 0) && (focusBtn.buttonLeft != null))
                SetFocusBtn(focusBtn.buttonLeft);

            if ((mVec.y < 0) && (focusBtn.buttonDown != null))
                SetFocusBtn(focusBtn.buttonDown);

            if ((mVec.y > 0) && (focusBtn.buttonUp != null))
                SetFocusBtn(focusBtn.buttonUp);

            // �t�H�[�J�X���ꂽ�Ƃ��̏���
            focusBtn.FocusProcess(true);

            // �t�H�[�J�X���O�ꂽ�Ƃ��̏���
            oldfocusBtn.FocusProcess(false);
        }
    }

    // �t�H�[�J�X����{�^����ݒ�
    public void SetFocusBtn(Button btn)
    {
        // �O��t�H�[�J�X����Ă����{�^���ƈقȂ�΃Z�b�g����
        if(btn != focusBtn)
        {
            oldfocusBtn = focusBtn;
            focusBtn = btn;
        }
    }
}
