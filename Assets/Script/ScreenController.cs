using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ʂ̎�ނ��Ǘ�
public class ScreenController : MonoBehaviour
{
    [SerializeField] private CursorController cursorController;   // Inspector��CursorController���w��
    [SerializeField] private UIController uIController;           // Inspector��UIController���w��
    [SerializeField] private StageController stageController;     // Inspector��StageController���w��
    [SerializeField] private PauseUIController pauseUIController; // Inspector��PauseUIController���w��

    // ��ʔԍ�
    // InGame     = 0
    // Pause      = 1
    // StageCrear = 2
    // Title      = 3
    public int screenNum = 0;

    bool changeStageClearScreen = false; // �X�e�[�W�N���A��ʂɑJ�ڂ������ǂ���

    void Update()
    {
        // �Q�[�����ɖ߂�{�^���������ꂽ��
        if(Input.GetButtonDown("Cancel") && screenNum == 0)
        {
            // �|�[�Y��ʂɑJ��
            screenNum = 1;

            // �}�E�X�J�[�\����\��
            cursorController.DrawCursol(true);

            // �|�[�Y��ʂ�UI��\��
            pauseUIController.DrawPauseUI(true);

            // ���Ԃ̗�����~�߂�
            Time.timeScale = 0.0f;
        }

        // �X�e�[�W���N���A�����Ȃ�
        if ((stageController.stageCrear) && (!changeStageClearScreen))
        {
            // �X�e�[�W�N���A��ʂɑJ�ڍς�
            changeStageClearScreen = true;

            // �X�e�[�W�N���A��ʂɑJ��
            screenNum = 2;

            // �}�E�X�J�[�\����\��
            cursorController.DrawCursol(true);
        }
    }
}
