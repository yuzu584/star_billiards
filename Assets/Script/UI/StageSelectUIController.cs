using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �X�e�[�W�I����ʂ�UI���Ǘ�
public class StageSelectUIController : MonoBehaviour
{
    [SerializeField] private StageData stageData;             // Inspector��StageData���w��
    [SerializeField] private StageController stageController; // Inspector��StageController���w��
    [SerializeField] private GameObject[] stageIcon;          // �X�e�[�W�̃A�C�R��

    // �X�e�[�W����`��
    public void DrawStageInfo(Text name, Text mission)
    {
        // �X�e�[�W����ݒ�
        name.text = stageData.stageList[stageController.stageNum].stageName;

        // �~�b�V��������ݒ�
        switch (stageData.stageList[stageController.stageNum].missionNum)
        {
            case 0: // �S�Ă̘f����j��
                mission.text = "Destroy all planets";
                break;
            case 1: // ���ԓ��ɃS�[���ɂ��ǂ蒅��
                mission.text = "Reach the goal";
                break;
        }
    }

    // �X�e�[�W�̃A�C�R����\��/��\��
    public void DrawStageIcon(bool draw)
    {
        // �X�e�[�W�̃A�C�R����\��/��\���؂�ւ�
        for (int i = 0; i < stageIcon.Length;  i++)
        {
            stageIcon[i].SetActive(draw);
        }
    }
}
