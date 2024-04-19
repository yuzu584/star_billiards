using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ミッションのUIを管理
public class MissionUIController : MonoBehaviour
{
    [SerializeField] private Text mText;                        // ミッションのテキスト
    [SerializeField] private StageData stageData;               // InspectorでStageDataを指定

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

        // フォントを設定
        mText.font = localize.GetFont();

        // 惑星破壊時にUIを更新
        dp.DPdele += Draw;

        Draw();
    }

    private void OnDestroy()
    {
        dp.DPdele -= Draw;
    }

    // ミッションのUIを描画
    void Draw()
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
                mText.text = localize.GetString("mission_name", "destroy_the_planet") + " " + planetAmount.planetDestroyAmount + " / " + planet;
                break;
            case 1: // 時間内にゴールにたどり着け

                // ミッションのテキストを設定
                mText.text = localize.GetString("mission_name", "reach_the_goal");
                break;
            default:
                break;
        }
    }
}
