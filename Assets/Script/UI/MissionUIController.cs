using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �~�b�V������UI���Ǘ�
public class MissionUIController : Singleton<MissionUIController>
{
    [SerializeField] private StageData stageData;                         // Inspector��StageData���w��
    [SerializeField] private StageController stageController;             // Inspector��StageController���w��
    [SerializeField] private UIController uIController;                   // Inspector��UIController���w��
    [SerializeField] private PlanetAmount planetAmount;                   // Inspector��PlanetAmount���w��

    // �~�b�V������UI��`��
    public void DrawMissionUI()
    {
        // �~�b�V�����ԍ�����
        int missionNum = stageData.stageList[stageController.stageNum].missionNum;

        // �X�e�[�W�̏����f��������
        int planet = stageData.stageList[stageController.stageNum].planetNum;

        // �~�b�V�����ԍ��ɂ���ĕ���
        switch (missionNum)
        {
            case 0: // �S�Ă̘f����j��

                // �~�b�V�����̃e�L�X�g��ݒ�
                uIController.missionUI.missionText.text = "Destroy all planets " + planetAmount.planetDestroyAmount + " / " + planet;
                break;
            case 1: // ���ԓ��ɃS�[���ɂ��ǂ蒅��

                // �~�b�V�����̃e�L�X�g��ݒ�
                uIController.missionUI.missionText.text = "Reach the goal";
                break;
            default:
                break;
        }
    }
}
