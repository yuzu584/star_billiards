using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージごとのスコアを管理する
public class StageScore : Singleton<StageScore>
{
    [SerializeField] private StageData stageData;

    public int[] score;

    private void Start()
    {
        // スコア配列の長さをステージの数にする
        score = new int[stageData.stageList.Count];
    }
}
