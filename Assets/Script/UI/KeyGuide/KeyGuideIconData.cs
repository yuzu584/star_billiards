using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// キー操作ガイドの画像を管理
[CreateAssetMenu(menuName = "MyScriptable/Create KeyGuideIconData")]
public class KeyGuideIconData : ScriptableObject
{
    public KeyTypeAndIcons[] keyTypeAndIcons;
}

// キー入力の種類の列挙型
public enum KeyType
{
    Positive,
    Negative,
    MoveCursol,
}

// アイコンをまとめる構造体
[System.Serializable]
public struct KeyIcons
{
    public Sprite KeybordAndMouse;      // キーボートとマウス使用時のアイコン
    public Sprite GamePad;              // ゲームパッド使用時のアイコン
}

// 入力の種類に応じたアイコンを格納する構造体
[System.Serializable]
public struct KeyTypeAndIcons
{
    public KeyType KeyType;
    public KeyIcons KeyIcons;
}