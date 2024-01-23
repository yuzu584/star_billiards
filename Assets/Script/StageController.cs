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
    [SerializeField] private CreateStage createStage;               // Inspector��CreateStage���w��
    [SerializeField] private ScreenController screenController;     // Inspector��ScreenController���w��
    public int stageNum = 0;                                        // �X�e�[�W�ԍ�
    public bool stageCrear = false;                                 // �X�e�[�W���N���A�������ǂ���

    private int missionNum;              // �~�b�V�����ԍ�
    private bool canCreateStage = false; // �X�e�[�W�����\��
    private bool stageCreated = false;   // �X�e�[�W�𐶐�������

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

        // �X�e�[�W�����\���X�e�[�W�������Ȃ�X�e�[�W�𐶐�
        if(((screenController.screenNum == 0) || (screenController.screenNum == 1) || (screenController.screenNum == 5) || (screenController.screenNum == 7)) && (!stageCreated))
            canCreateStage = true;

        // �X�e�[�W�����s���X�e�[�W�����ς݂Ȃ�X�e�[�W���폜
        else if ((screenController.screenNum != 0) && (screenController.screenNum != 1) && (screenController.screenNum != 5) && (screenController.screenNum != 7) && (stageCreated))
            canCreateStage = false;

        // �X�e�[�W�𐶐�/�폜
        if(stageCreated != canCreateStage)
        {
            stageCreated = canCreateStage;
            createStage.Create(canCreateStage);
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
