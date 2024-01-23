using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージを管理
public class StageController : MonoBehaviour
{
    [SerializeField] private StageData stageData;                   // InspectorでStageDataを指定
    [SerializeField] private PlanetAmount planetAmount;             // InspectorでPlanetAmountを指定
    [SerializeField] private EnergyController energyController;     // InspectorでEnergyControllerを指定
    [SerializeField] private EnergyUIController energyUIController; // InspectorでEnergyUIControllerを指定
    [SerializeField] private SkillController skillController;       // InspectorでSkillControllerを指定
    [SerializeField] private SkillUIController skillUIController;   // InspectorでSkillUIControllerを指定
    [SerializeField] private PopupController popupController;       // InspectorでPopupControllerを指定
    [SerializeField] private PlayerController playerController;     // InspectorでPlayerControllerを指定
    [SerializeField] private CreateStage createStage;               // InspectorでCreateStageを指定
    [SerializeField] private ScreenController screenController;     // InspectorでScreenControllerを指定
    public int stageNum = 0;                                        // ステージ番号
    public bool stageCrear = false;                                 // ステージをクリアしたかどうか

    private int missionNum;              // ミッション番号
    private bool canCreateStage = false; // ステージ生成可能か
    private bool stageCreated = false;   // ステージを生成したか

    void Update()
    {
        // ミッション番号を代入
        if (missionNum != stageData.stageList[stageNum].missionNum)
            missionNum = stageData.stageList[stageNum].missionNum;

        // ミッションが"全ての惑星を破壊"かつクリア条件を達成したなら
        if (missionNum == 0 && (planetAmount.planetDestroyAmount >= stageData.stageList[stageNum].planet.Length))
        {
            // ステージクリア
            stageCrear = true;
        }

        // ステージ生成可能かつステージ未生成ならステージを生成
        if(((screenController.screenNum == 0) || (screenController.screenNum == 1) || (screenController.screenNum == 5) || (screenController.screenNum == 7)) && (!stageCreated))
            canCreateStage = true;

        // ステージ生成不可かつステージ生成済みならステージを削除
        else if ((screenController.screenNum != 0) && (screenController.screenNum != 1) && (screenController.screenNum != 5) && (screenController.screenNum != 7) && (stageCreated))
            canCreateStage = false;

        // ステージを生成/削除
        if(stageCreated != canCreateStage)
        {
            stageCreated = canCreateStage;
            createStage.Create(canCreateStage);
        }
    }

    // ステージに関する数値を初期化
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
