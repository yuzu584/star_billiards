using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 惑星のステータスのリスト
[CreateAssetMenu(menuName = "MyScriptable/Create planetData")]
public class planetData : ScriptableObject
{
    public List<planetDataContent> planetList = new List<planetDataContent>();
}

// 惑星のステータスの内容
[System.Serializable]
public class planetDataContent
{
    [SerializeField] string planetName; // 名前
    [SerializeField] int scale;         // 大きさ
    [SerializeField] Material material; // マテリアル
    [SerializeField] bool fixedStar;    // 恒星か否か
}