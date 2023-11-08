using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 自分に衝突した惑星を削除
public class DestroyPlanet : MonoBehaviour
{
    [SerializeField] private UIController uIController;       // InspectorでUIControllerを指定
    [SerializeField] private StageData stageData;             // InspectorでStageDataを指定
    [SerializeField] private StageController stageController; // InspectorでStageControllerを指定

    [System.NonSerialized] public int planetDestroyAmount = 0; // 惑星を破壊した数

    // もし何かと衝突したら
    void OnCollisionEnter(Collision collision)
    {
        // 惑星と衝突したら
        if (collision.gameObject.CompareTag("Planet"))
        {
            // ポップアップの数をカウント
            uIController.popupAmount++;

            // 惑星が破壊された旨を伝えるポップアップを描画
            StartCoroutine(uIController.DrawDestroyPlanetPopup(collision.gameObject.name));

            // ミッションが"全ての惑星を破壊"なら
            if(stageData.stageList[stageController.stageNum].missionNum == 0)
            {
                // 惑星を破壊した数をカウント
                planetDestroyAmount++;

                // ミッションUIを更新
                uIController.DrawMissionUI();
            }

            // 惑星を削除
            Destroy(collision.gameObject);
        }
    }
}
