using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 各言語のテキスト
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData")]
public class StringTextData : ScriptableObject
{
    public Fonts[] fonts;
    public Strings[] strings;
}

// 言語の種類
public enum LanguageType
{
    English,    // 英語
    Japanese,   // 日本語
}

// 文字列のグループ
// 要素が増減してもいいように数値を指定
public enum StringGroup
{
    Screen = 000,
    StageName = 010,
    Mission = 020,
    ConfigTop = 030,
    ConfigContent = 031,
    SkillName = 040,
    Player = 050,
    System = 100,
    None = 101,
}

// 文字列の種類
// 要素が増減してもいいように数値を指定
public enum StringType
{
    // 画面
    PleaseAnyKey = 000,
    StageSelect = 001,
    Options = 002,
    SkillSelect = 003,
    ExitGame = 004,
    Pause = 005,
    ReturnToGame = 006,
    ReturnToMainMenu = 007,

    // ステージ名
    Stage1 = 100,
    Stage2 = 101,
    Stage3 = 102,
    Stage4 = 103,
    Stage5 = 104,

    // ミッション
    DestroyPlanet = 200,
    ReachTheGoal = 201,

    // 設定画面_Top
    GamePlay = 300,
    Video = 301,
    Audio = 302,
    KeyConfig = 303,
    Language = 304,

    // 設定項目

    // スキル名
    SuperCharge = 500,
    PowerSurge = 501,
    Huge = 502,
    GravityWave = 503,
    Frieze = 504,
    GrapplingHook = 505,
    Slow = 506,
    InertialControl = 507,
    Blink = 508,
    TeleportAnchor = 509,

    // プレイヤー関連
    Charge = 600,

    // システム
    OK = 1000,
    Cancel = 1001,
    Apply = 1002,
    Reset = 1003,
    Start = 1004,

    None = 1100,
}

// 言語ごとの構造体
[System.Serializable]
public struct StringData
{
    public StringType type;
    public string[] text;
}

[System.Serializable]
public struct Fonts
{
    public LanguageType language;
    public Font font;
}

[System.Serializable]
public struct Strings
{
    public StringGroup group;
    public StringData[] stringData;
}
