using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ミッションのUIを管理
public class MissionUIController : Singleton<MissionUIController>
{
    [SerializeField] private StageData stageData;                         // InspectorでStageDataを指定

    private StageController stageCon;
    private UIController uICon;
    private PlanetAmount planetAmount;

    private void Start()
    {
        stageCon = StageController.instance;
        uICon = UIController.instance;
        planetAmount = PlanetAmount.instance;
    }

    // ミッションのUIを描画
    public void DrawMissionUI()
    {
        // ミッション番号を代入
        int missionNum = stageData.stageList[stageCon.stageNum].missionNum;

        // ステージの初期惑星数を代入
        int planet = stageData.stageList[stageCon.stageNum].planetNum;

        // ミッション番号によって分岐
        switch (missionNum)
        {
            case 0: // 全ての惑星を破壊

                // ミッションのテキストを設定
                uICon.missionUI.missionText.text = "Destroy all planets " + planetAmount.planetDestroyAmount + " / " + planet;
                break;
            case 1: // 時間内にゴールにたどり着け

                // ミッションのテキストを設定
                uICon.missionUI.missionText.text = "Reach the goal";
                break;
            default:
                break;
        }
    }
}
