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
    public int stageNum = 0;                                        // ステージ番号
    public bool stageCrear = false;                                 // ステージをクリアしたかどうか

    private int missionNum; // ミッション番号

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
