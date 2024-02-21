using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// ��ʂ̎�ނ��Ǘ�
public class ScreenController : MonoBehaviour
{
    [SerializeField] private UIController uIController;                 // Inspector��UIController���w��
    [SerializeField] private StageController stageController;           // Inspector��StageController���w��
    [SerializeField] private PauseUIController pauseUIController;       // Inspector��PauseUIController���w��
    [SerializeField] private ScreenData screenData;                     // Inspector��ScreenData���w��

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
    [System.NonSerialized] public bool[] canUIDraw = new bool[AppConst.SCREEN_AMOUNT];

    [System.NonSerialized] public bool canStageDraw = false; // �X�e�[�W��`��\��

    public int screenNum = 0;    // ��ʔԍ�
    public int oldScreenNum = 0; // 1�t���[���O�̉�ʔԍ�
    public delegate void ChangeScreen(); // ��ʂ��J�ڂ����Ƃ��̃f���Q�[�g
    public ChangeScreen changeScreen;

    private bool changeStageClearScreen = false; // �X�e�[�W�N���A��ʂɑJ�ڂ������ǂ���

    void Update()
    {
        // �O��̃t���[���ƌ��݂̃t���[���ŉ�ʔԍ����قȂ�����
        if(screenNum != oldScreenNum)
        {
            changeScreen();
            oldScreenNum = screenNum;
        }

        // �Q�[�����ɖ߂�{�^���������ꂽ��
        if (Input.GetButtonDown("Cancel") && screenNum == 5)
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

        // UI���`��\�����Ǘ�����z����X�V
        for (int i = 0; i < AppConst.SCREEN_AMOUNT; i++)
        {
            if(canUIDraw[i] != screenData.screenList[screenNum].uIDrawList[i])
                canUIDraw[i] = screenData.screenList[screenNum].uIDrawList[i];
        }

        // �X�e�[�W���`��\�����Ǘ�����z����X�V
        if (canStageDraw != screenData.screenList[screenNum].drawStage)
            canStageDraw = screenData.screenList[screenNum].drawStage;
    }
}
