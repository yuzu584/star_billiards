using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W���Ǘ�
public class StageController : MonoBehaviour
{
    [SerializeField] private StageData stageData;                   // Inspector��StageData���w��
    [SerializeField] private PlanetAmount planetAmount;             // Inspector��PlanetAmount���w��
    [SerializeField] private EnergyController energyController;     // Inspector��EnergyController���w��
    [SerializeField] private EnergyUIController energyUIController; // Inspector��EnergyUIController���w��
    [SerializeField] private SkillController skillController;       // Inspector��SkillController���w��
    [SerializeField] private SkillUIController skillUIController;   // Inspector��SkillUIController���w��
    [SerializeField] private PopupController popupController;       // Inspector��PopupController���w��
    [SerializeField] private PlayerController playerController;     // Inspector��PlayerController���w��
    public int stageNum = 0;                                        // �X�e�[�W�ԍ�
    public bool stageCrear = false;                                 // �X�e�[�W���N���A�������ǂ���

    private int missionNum; // �~�b�V�����ԍ�

    void Update()
    {
        // �~�b�V�����ԍ�����
        if (missionNum != stageData.stageList[stageNum].missionNum)
            missionNum = stageData.stageList[stageNum].missionNum;

        // �~�b�V������"�S�Ă̘f����j��"���N���A������B�������Ȃ�
        if (missionNum == 0 && (planetAmount.planetDestroyAmount >= stageData.stageList[stageNum].planet.Length))
        {
            // �X�e�[�W�N���A
            stageCrear = true;
        }
    }

    // �X�e�[�W�Ɋւ��鐔�l��������
    public void Init()
    {
        stageCrear = false;
        planetAmount.planetDestroyAmount = 0;
        popupController.InitPopUp();
        playerController.Init(
            energyController,
            energyUIController,
            skillController,
            skillUIController);
    }
}
