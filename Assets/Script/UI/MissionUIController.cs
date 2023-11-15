using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ミッションのUIを管理
public class MissionUIController : MonoBehaviour
{
    [SerializeField] private StageData stageData;                         // InspectorでStageDataを指定
    [SerializeField] private StageController stageController;             // InspectorでStageControllerを指定
    [SerializeField] private UIController uIController;                   // InspectorでUIControllerを指定
    [SerializeField] private DestroyPlanet destroyPlanet;                 // InspectorでDestroyPlanetを指定

    // ミッションのUIを描画
    public void DrawMissionUI(bool draw)
    {
        // 描画するなら
        if (draw)
        {
            // ミッション番号を代入
            int missionNum = stageData.stageList[stageController.stageNum].missionNum;

            // ステージの初期惑星数を代入
            int planetAmount = stageData.stageList[stageController.stageNum].planetAmount;

            // ミッション番号によって分岐
            switch (missionNum)
            {
                case 0: // 全ての惑星を破壊

                    // ミッションのテキストを設定
                    uIController.missionUI.missionText.text = "Destroy all planets " + destroyPlanet.planetDestroyAmount + " / " + planetAmount;
                    break;
                case 1: // 時間内にゴールにたどり着け

                    // ミッションのテキストを設定
                    uIController.missionUI.missionText.text = "Reach the goal";
                    break;
                default:
                    break;
            }
        }

        // 表示/非表示切り替え
        if(uIController.missionUI.missionText.enabled != draw)
        {
            uIController.missionUI.missionText.enabled = draw;
        }
    }
}
