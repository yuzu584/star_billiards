using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 惑星を破壊
public class DestroyPlanet : Singleton<DestroyPlanet>
{
    [SerializeField] private StageData stageData; // InspectorでStageDataを指定

    private ScreenController scrCon;
    private PopupController popupCon;
    private StageController stageCon;
    private PlanetAmount planetAmount;
    private MissionUIController missionUICon;

    private void Start()
    {
        scrCon = ScreenController.instance;
        popupCon = PopupController.instance;
        stageCon = StageController.instance;
        planetAmount = PlanetAmount.instance;
        missionUICon = MissionUIController.instance;
    }

    // 惑星を破壊
    public void DestroyPlanetProcess(GameObject obj)
    {
        // ゲーム中に惑星と衝突したら
        if ((obj.CompareTag("Planet")) && (scrCon.ScreenNum == 5))
        {
            // 惑星が破壊された旨を伝えるポップアップを描画
            popupCon.DrawDestroyPlanetPopUp(obj.name);

            // ミッションが"全ての惑星を破壊"なら
            if (stageData.stageList[stageCon.stageNum].missionNum == 0)
            {
                // 惑星を破壊した数をカウント
                planetAmount.planetDestroyAmount++;

                // ミッションUIを更新
                missionUICon.DrawMissionUI();
            }

            // 惑星を削除
            Destroy(obj);
        }
    }
}
