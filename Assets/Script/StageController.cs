using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージを管理
public class StageController : MonoBehaviour
{
    [SerializeField] private StageData stageData;         // InspectorでStageDataを指定
    [SerializeField] private DestroyPlanet destroyPlanet; // InspectorでDestroyPlanetを指定
    public int stageNum = 0;                              // ステージ番号
    public bool stageCrear = false;                       // ステージをクリアしたかどうか

    private int missionNum; // ミッション番号

    void Update()
    {
        // ミッション番号を代入
        if (missionNum != stageData.stageList[stageNum].missionNum)
            missionNum = stageData.stageList[stageNum].missionNum;

        // ミッションが"全ての惑星を破壊"かつクリア条件を達成したなら
        if (missionNum == 0 && (destroyPlanet.planetDestroyAmount >= stageData.stageList[stageNum].planet.Length))
        {
            // ステージクリア
            stageCrear = true;
        }
    }
}
