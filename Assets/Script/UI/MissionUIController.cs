using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �~�b�V������UI���Ǘ�
public class MissionUIController : MonoBehaviour
{
    [SerializeField] private Text mText;                        // �~�b�V�����̃e�L�X�g
    [SerializeField] private StageData stageData;               // Inspector��StageData���w��

    private StageController stageCon;
    private PlanetAmount planetAmount;
    private DestroyPlanet dp;
    private Localize localize;

    private void Start()
    {
        stageCon = StageController.instance;
        planetAmount = PlanetAmount.instance;
        dp = DestroyPlanet.instance;
        localize = Localize.instance;

        // �t�H���g��ݒ�
        mText.font = localize.GetFont();

        // �f���j�󎞂�UI���X�V
        dp.DPdele += Draw;

        Draw();
    }

    private void OnDestroy()
    {
        dp.DPdele -= Draw;
    }

    // �~�b�V������UI��`��
    void Draw()
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
                mText.text = localize.GetString("mission_name", "destroy_the_planet") + " " + planetAmount.planetDestroyAmount + " / " + planet;
                break;
            case 1: // ���ԓ��ɃS�[���ɂ��ǂ蒅��

                // �~�b�V�����̃e�L�X�g��ݒ�
                mText.text = localize.GetString("mission_name", "reach_the_goal");
                break;
            default:
                break;
        }
    }
}
