using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �~�b�V������UI���Ǘ�
public class MissionUIController : Singleton<MissionUIController>
{
    [SerializeField] private StageData stageData;                         // Inspector��StageData���w��

    private StageController stageCon;
    private UIController uICon;
    private PlanetAmount planetAmount;

    private void Start()
    {
        stageCon = StageController.instance;
        uICon = UIController.instance;
        planetAmount = PlanetAmount.instance;
    }

    // �~�b�V������UI��`��
    public void DrawMissionUI()
    {
        // �~�b�V�����ԍ�����
        int missionNum = stageData.stageList[stageCon.stageNum].missionNum;

        // �X�e�[�W�̏����f��������
        int planet = stageData.stageList[stageCon.stageNum].planetNum;

        // �~�b�V�����ԍ��ɂ���ĕ���
        switch (missionNum)
        {
            case 0: // �S�Ă̘f����j��

                // �~�b�V�����̃e�L�X�g��ݒ�
                uICon.missionUI.missionText.text = "Destroy all planets " + planetAmount.planetDestroyAmount + " / " + planet;
                break;
            case 1: // ���ԓ��ɃS�[���ɂ��ǂ蒅��

                // �~�b�V�����̃e�L�X�g��ݒ�
                uICon.missionUI.missionText.text = "Reach the goal";
                break;
            default:
                break;
        }
    }
}
