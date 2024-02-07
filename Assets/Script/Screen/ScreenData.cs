using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// スクリーンのリスト
[CreateAssetMenu(menuName = "MyScriptable/Create ScreenData")]
public class ScreenData : ScriptableObject
{
    public List<ScreenDataContent> screenList;
}

// スクリーンのリスト
[System.Serializable]
public class ScreenDataContent
{
    public string screenName;                                    // 名前
    public bool drawCursol;                                      // カーソルを表示するか
    public float timeScale = 1.0f;                               // 時間が流れる速さ
    public bool drawStage = false;                               // ステージを描画するか
    public bool[] uIDrawList = new bool[AppConst.SCREEN_AMOUNT]; // 描画するUIの配列
}
