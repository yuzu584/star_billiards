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

    [System.NonSerialized] public bool canStageDraw = false; // �X�e�[�W��`��\��

    private int screenNum;               // ��ʔԍ�
    public int ScreenNum                 // ��ʔԍ��̃v���p�e�B
    {
        get { return screenNum; }
        set { SwitchProcess(value); }
    }
    [System.NonSerialized] public int oldScreenNum = 0;         // �O��̉�ʔԍ�
    [System.NonSerialized] public int oldFrameScreenNum = 0;    // 1�t���[���O�̉�ʔԍ�
    public delegate void ChangeScreen(); // ��ʂ��J�ڂ����Ƃ��̃f���Q�[�g
    public ChangeScreen changeScreen;

    private bool changeStageClearScreen = false; // �X�e�[�W�N���A��ʂɑJ�ڂ������ǂ���
    private float pauseInputValue;               // �|�[�Y��ʂ��J���{�^���̓���
    
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
            Color c1 = new Color(0, 0, 0, 0);
            Color c2 = new Color(0, 0, 0, 1);
            switchImage.gameObject.SetActive(true);
            switchImage.raycastTarget = true;
            yield return StartCoroutine(SwitchAnim(c1, c2));
        }

        // ��ʔԍ���ݒ�
        screenNum = num;
        
        if(orPlay)
        {
            // ��ʑJ�ڃA�j���[�V����
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

    void Srart()
    {
        changeScreen();
    }

    void Update()
    {
        // �|�[�Y��ʂ��J���{�^���̓��͂��擾
        pauseInputValue = input.Game_Pause;

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

        // �Q�[�����ɖ߂�{�^���������ꂽ��
        if ((pauseInputValue > 0) && (screenNum == 5))
        {
            // �|�[�Y��ʂɑJ��
            screenNum = 6;
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
}
