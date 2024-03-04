using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 惑星を破壊
public class DestroyPlanet : MonoBehaviour
{
    [SerializeField] private StageData stageData; // InspectorでStageDataを指定
    [SerializeField] private ScreenController screenController;
    [SerializeField] private PopupController popupController;
    [SerializeField] private StageController stageController;
    [SerializeField] private PlanetAmount planetAmount;
    [SerializeField] private MissionUIController missionUIController;

    // 惑星を破壊
    public void DestroyPlanetPrpcess(GameObject obj)
    {
        // ゲーム中に惑星と衝突したら
        if ((obj.CompareTag("Planet")) && (screenController.ScreenNum == 5))
        {
            // 惑星が破壊された旨を伝えるポップアップを描画
            popupController.DrawDestroyPlanetPopUp(obj.name);

            // ミッションが"全ての惑星を破壊"なら
            if (stageData.stageList[stageController.stageNum].missionNum == 0)
            {
                // 惑星を破壊した数をカウント
                planetAmount.planetDestroyAmount++;

                // ミッションUIを更新
                missionUIController.DrawMissionUI();
            }

            // 惑星を削除
            Destroy(obj);
        }
    }
}
