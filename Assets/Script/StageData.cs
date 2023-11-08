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
    public string stageName;    // 名前
    public int fixedStarAmount; // 恒星数
    public int planetAmount;    // 惑星数
    public int missionNum;      // ミッション番号
}