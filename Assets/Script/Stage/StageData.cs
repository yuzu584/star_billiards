using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージのリスト
[CreateAssetMenu(menuName = "MyScriptable/Create StageData")]
public class StageData : ScriptableObject
{
    public List<StageDataContent> stageList = new List<StageDataContent>();
}

// ステージのリスト
[System.Serializable]
public class StageDataContent
{
    public string stageName;       // 名前
    public GameObject[] fixedStar; // 恒星
    public GameObject[] planet;    // 惑星
    public int missionNum;         // ミッション番号
    public int timeLimit;          // 制限時間
}