using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 各言語のテキスト
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData")]
public class StringTextData : ScriptableObject
{
    public Fonts[] fonts;
    public StringData[] stringData;
}

// 言語の種類
public enum LanguageType
{
    English,    // 英語
    Japanese,   // 日本語
}

// 文字列の種類
// 要素が増減してもいいように数値を指定
public enum StringType
{
    // タイトル画面
    PleaseAnyKey = 000,

    // メインメニュー
    StageSelect = 100,
    Options = 101,
    SkillSelect = 102,
    ExitGame = 103,

    // ステージ選択画面
    Stage1 = 200,
    Stage2 = 201,
    Stage3 = 202,
    Stage4 = 203,
    Stage5 = 204,

    // 設定画面
    GamePlay = 300,
    Video = 301,
    Audio = 302,
    KeyConfig = 303,
    Language = 304,

    // スキル選択画面
    SuperCharge = 400,
    PowerSurge = 401,
    Huge = 402,
    GravityWave = 403,
    Frieze = 404,
    GrapplingHook = 405,
    Slow = 406,
    InertialControl = 407,
    Blink = 408,
    TeleportAnchor = 409,

    // ゲーム画面

    // ポーズ画面
    Pause = 600,
    ReturnToGame = 601,
    ReturnToMainMenu = 602,

    // 惑星情報画面

    // ステージクリア・ゲームオーバー画面

    // その他
    OK = 1000,
    Cancel = 1001,

    None = 2000,
}

// 言語ごとの構造体
[System.Serializable]
public struct StringData
{
    public StringType type;
    public Strings[] strings;
}

// 文字列の構造体
[System.Serializable]
public struct Strings
{
    public LanguageType language;
    public string text;
}

[System.Serializable]
public struct Fonts
{
    public LanguageType language;
    public Font font;
}
