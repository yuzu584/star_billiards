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
    public GameObject stagePrefab; // ステージのプレハブ
    public int fixedStarNum;       // ステージに含まれる恒星の数
    public int planetNum;          // ステージに含まれる惑星の数
    public int missionNum;         // ミッション番号
    public int timeLimit;          // 制限時間
}