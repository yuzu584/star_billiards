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
    Screen              = 000,  // 画面関係
    StageName           = 010,  // ステージ名
    Mission             = 020,  // ミッション名
    ConfigTop           = 030,  // 設定画面の項目の名前
    ConfigContent       = 031,  // 設定画面で設定可能な項目の名前
    SkillName           = 040,  // スキル名
    SkillParameter      = 041,  // スキルのパラメーター(消費エネルギーや効果時間など)
    SkillDetails        = 042,  // スキルの効果
    Player              = 050,  // プレイヤー関連
    Message             = 060,  // 主にポップアップで出る文章
    System              = 100,  // システム
    None                = 101,
}

// 文字列の種類
// 要素が増減してもいいように数値を指定
public enum StringType
{
    // 画面
    PleaseAnyKey        = 000,  // タイトル画面にあるテキスト
    StageSelect         = 001,  // ステージ選択
    Options             = 002,  // 設定
    SkillSelect         = 003,  // スキル選択
    ExitGame            = 004,  // ゲーム終了
    Pause               = 005,  // ポーズ
    ReturnToGame        = 006,  // ゲーム再開
    ReturnToMainMenu    = 007,  // メインメニューに戻る

    // ステージ名
    Stage1              = 100,
    Stage2              = 101,
    Stage3              = 102,
    Stage4              = 103,
    Stage5              = 104,

    // ミッション名
    DestroyPlanet       = 200,  // 惑星を破壊しろ
    ReachTheGoal        = 201,  // ゴールにたどり着け

    // 設定画面_Top
    GamePlay            = 300,  // ゲームプレイ
    Video               = 301,  // ビデオ
    Audio               = 302,  // オーディオ
    KeyConfig           = 303,  // キーコンフィグ
    Language            = 304,  // 言語

    // 設定項目

    // スキル名
    SuperCharge         = 500,  // スーパーチャージ
    PowerSurge          = 501,  // パワーサージ
    Huge                = 502,  // 巨大化
    GravityWave         = 503,  // 重力波
    Frieze              = 504,  // フリーズ
    GrapplingHook       = 505,  // グラップリングフック
    Slow                = 506,  // スロー
    InertialControl     = 507,  // インナーシャルコントロール
    Blink               = 508,  // ブリンク
    TeleportAnchor      = 509,  // テレポートアンカー

    // スキルのパラメーター
    EnergyCost          = 530,  // エネルギー消費量
    EffectTime          = 531,  // 効果時間
    CoolDown            = 532,  // クールダウン
    EffectDetails       = 533,  // スキルの効果

    // スキルの効果
    SuperChargeDetails      = 560,  // スーパーチャージの効果
    PowerSurgeDetails       = 561,  // パワーサージの効果
    HugeDetails             = 562,  // 巨大化の効果
    GravityWaveDetails      = 563,  // 重力波の効果
    FriezeDetails           = 564,  // フリーズの効果
    GrapplingHookDetails    = 565,  // グラップリングフックの効果
    SlowDetails             = 566,  // スローの効果
    InertialControlDetails  = 567,  // インナーシャルコントロールの効果
    BlinkDetails            = 568,  // ブリンクの効果
    TeleportAnchorDetails   = 569,  // テレポートアンカーの効果

    // プレイヤー関連
    Charge              = 600,  // チャージ

    // 主にポップアップで出る文章
    WasDecided          = 700,  // 確定しました
    PleaseSelect3Skills = 701,  // スキルを3つ選んでいないときのテキスト
    ExitGameText        = 702,  // ゲーム終了時のテキスト

    // システム
    OK                  = 1000, // 決定
    Cancel              = 1001, // キャンセル
    Apply               = 1002, // 確定
    Reset               = 1003, // リセット
    Start               = 1004, // スタート

    None                = 1100,
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
