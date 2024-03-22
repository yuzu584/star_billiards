using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ミッションのUIを管理
public class MissionUIController : Singleton<MissionUIController>
{
    [SerializeField] private StageData stageData;                         // InspectorでStageDataを指定
    [SerializeField] private StageController stageController;             // InspectorでStageControllerを指定
    [SerializeField] private UIController uIController;                   // InspectorでUIControllerを指定
    [SerializeField] private PlanetAmount planetAmount;                   // InspectorでPlanetAmountを指定

    // ミッションのUIを描画
    public void DrawMissionUI()
    {
        // ミッション番号を代入
        int missionNum = stageData.stageList[stageController.stageNum].missionNum;

        // ステージの初期惑星数を代入
        int planet = stageData.stageList[stageController.stageNum].planetNum;

        // ミッション番号によって分岐
        switch (missionNum)
        {
            case 0: // 全ての惑星を破壊

                // ミッションのテキストを設定
                uIController.missionUI.missionText.text = "Destroy all planets " + planetAmount.planetDestroyAmount + " / " + planet;
                break;
            case 1: // 時間内にゴールにたどり着け

                // ミッションのテキストを設定
                uIController.missionUI.missionText.text = "Reach the goal";
                break;
            default:
                break;
        }
    }
}
