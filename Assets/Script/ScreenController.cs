using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ʂ̎�ނ��Ǘ�
public class ScreenController : MonoBehaviour
{
    [SerializeField] private UIController uIController;                 // Inspector��UIController���w��
    [SerializeField] private StageController stageController;           // Inspector��StageController���w��
    [SerializeField] private PauseUIController pauseUIController;       // Inspector��PauseUIController���w��
    [SerializeField] private ScreenData screenData;                     // Inspector��ScreenData���w��

    // ��ʔԍ�
    // 0 : �Q�[�����
    // 1 : �|�[�Y���
    // 2 : �X�e�[�W�N���A���
    // 3 : ���C�����j���[
    // 4 : �X�e�[�W�I�����
    public int screenNum = 3;

    private bool changeStageClearScreen = false; // �X�e�[�W�N���A��ʂɑJ�ڂ������ǂ���

    void Update()
    {
        // �Q�[�����ɖ߂�{�^���������ꂽ��
        if(Input.GetButtonDown("Cancel") && screenNum == 0)
        {
            // �|�[�Y��ʂɑJ��
            screenNum = 1;

            // �|�[�Y��ʂ�UI��\��
            pauseUIController.DrawPauseUI(true);
        }

        // �X�e�[�W���N���A����ʑJ�ڂ��Ă��Ȃ��Ȃ�
        if ((stageController.stageCrear) && (!changeStageClearScreen))
        {
            // �X�e�[�W�N���A��ʂɑJ�ڍς�
            changeStageClearScreen = true;

            // �X�e�[�W�N���A��ʂɑJ��
            screenNum = 2;
        }
        // �X�e�[�W���N���A����ʑJ�ڂ����Ȃ�
        else if ((!stageController.stageCrear) && (changeStageClearScreen))
        {
            // �X�e�[�W�N���A��ʂɖ��J��
            changeStageClearScreen = false;
        }
    }
}
