using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �~�b�V������UI���Ǘ�
public class MissionUIController : MonoBehaviour
{
    [SerializeField] private StageData stageData;                         // Inspector��StageData���w��
    [SerializeField] private StageController stageController;             // Inspector��StageController���w��
    [SerializeField] private UIController uIController;                   // Inspector��UIController���w��
    [SerializeField] private DestroyPlanet destroyPlanet;                 // Inspector��DestroyPlanet���w��

    // �~�b�V������UI��`��
    public void DrawMissionUI(bool draw)
    {
        // �`�悷��Ȃ�
        if (draw)
        {
            // �~�b�V�����ԍ�����
            int missionNum = stageData.stageList[stageController.stageNum].missionNum;

            // �X�e�[�W�̏����f��������
            int planetAmount = stageData.stageList[stageController.stageNum].planetAmount;

            // �~�b�V�����ԍ��ɂ���ĕ���
            switch (missionNum)
            {
                case 0: // �S�Ă̘f����j��

                    // �~�b�V�����̃e�L�X�g��ݒ�
                    uIController.missionUI.missionText.text = "Destroy all planets " + destroyPlanet.planetDestroyAmount + " / " + planetAmount;
                    break;
                case 1: // ���ԓ��ɃS�[���ɂ��ǂ蒅��

                    // �~�b�V�����̃e�L�X�g��ݒ�
                    uIController.missionUI.missionText.text = "Reach the goal";
                    break;
                default:
                    break;
            }
        }

        // �\��/��\���؂�ւ�
        if(uIController.missionUI.missionText.enabled != draw)
        {
            uIController.missionUI.missionText.enabled = draw;
        }
    }
}
