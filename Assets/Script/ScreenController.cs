using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ʂ̎�ނ��Ǘ�
public class ScreenController : MonoBehaviour
{
    [SerializeField] private CursorController cursorController; // CursorController�^�̕ϐ�
    [SerializeField] private UIController uIController;         // UIController�^�̕ϐ�

    // ��ʔԍ�
    // InGame = 0
    // Pause  = 1
    public int screenNum = 0;

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
            uIController.DrawPauseUI(true);

            // ���Ԃ̗�����~�߂�
            Time.timeScale = 0.0f;
        }
    }
}
