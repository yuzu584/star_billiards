using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スクリーンのリスト
[CreateAssetMenu(menuName = "MyScriptable/Create ScreenData")]
public class ScreenData : ScriptableObject
{
    public List<ScreenDataContent> screenList;
}

// 階層のクラス
[System.Serializable]
public class LootStr
{
    public string name;
    public bool enterAnim = false;
    public bool exitAnim = false;
}

// スクリーンのリスト
[System.Serializable]
public class ScreenDataContent
{
    public string screenName;       // 名前
    public bool drawCursol;         // カーソルを表示するか
    public float timeScale = 1.0f;  // 時間が流れる速さ
    public bool drawStage = false;  // ステージを描画するか
    public bool enterAnim = false;  // この画面に遷移した時にアニメーションを行うか
    public bool exitAnim = false;   // この画面から遷移した時にアニメーションを行うか
    public int inputType;           // InputSystemのどのActionMapの入力を受け取るか
    public LootStr[] loot;          // 階層ごとの情報
}
