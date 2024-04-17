using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Screen                  = 000,  // 画面関係
    StageName               = 010,  // ステージ名
    Mission                 = 020,  // ミッション
    ConfigTop               = 030,  // 設定画面の項目の名前
    ConfigContent           = 031,  // 設定画面で設定可能な項目の名前
    SkillName               = 040,  // スキル名
    SkillParameter          = 041,  // スキルのパラメーター(消費エネルギーや効果時間など)
    SkillDetails            = 042,  // スキルの効果
    Player                  = 050,  // プレイヤー関連
    Message                 = 060,  // 主にポップアップで出る文章
    StarName                = 070,  // 星の名前
    KeyGuide                = 080,  // キー説明のテキスト
    System                  = 100,  // システム
    None                    = 101,
}

// 文字列の列挙型の構造体
[System.Serializable]
public struct StringEnumStruct
{
    public EnumScreen screen;
    public EnumStageName stageName;
    public EnumMission mission;
    public EnumConfigTop configTop;
    public AppParams.ParamsKey configContent;
    public EnumSkillName skillName;
    public EnumSkillParameter skillParameter;
    public EnumSkillDetails skillDetails;
    public EnumPlayer player;
    public EnumMessage message;
    public EnumStarName starName;
    public EnumKeyGuide keyGuide;
    public EnumSystem system;
}

// 画面関係の列挙型
public enum EnumScreen
{
    PleaseAnyKey            = 000,  // タイトル画面にあるテキスト
    StageSelect             = 001,  // ステージ選択
    Options                 = 002,  // 設定
    SkillSelect             = 003,  // スキル選択
    ExitGame                = 004,  // ゲーム終了
    Pause                   = 005,  // ポーズ
    ReturnToGame            = 006,  // ゲーム再開
    ReturnToMainMenu        = 007,  // メインメニューに戻る
    None                    = 100,
}

// ステージ名の列挙型
public enum EnumStageName
{
    Stage1                  = 000,
    Stage2                  = 001,
    Stage3                  = 002,
    Stage4                  = 003,
    Stage5                  = 004,
    None                    = 100,
}

// ミッションの列挙型
public enum EnumMission
{
    DestroyPlanet           = 000,  // 惑星を破壊しろ
    ReachTheGoal            = 001,  // ゴールにたどり着け
    None                    = 100,
}

// 設定画面の項目の名前の列挙型
public enum EnumConfigTop
{
    GamePlay                = 000,  // ゲームプレイ
    Video                   = 001,  // ビデオ
    Audio                   = 002,  // オーディオ
    KeyConfig               = 003,  // キーコンフィグ
    Language                = 004,  // 言語
    None                    = 100,
}

// スキル名の列挙型
public enum EnumSkillName
{
    SuperCharge             = 000,  // スーパーチャージ
    PowerSurge              = 001,  // パワーサージ
    Huge                    = 002,  // 巨大化
    GravityWave             = 003,  // 重力波
    Frieze                  = 004,  // フリーズ
    GrapplingHook           = 005,  // グラップリングフック
    Slow                    = 006,  // スロー
    InertialControl         = 007,  // インナーシャルコントロール
    Blink                   = 008,  // ブリンク
    TeleportAnchor          = 009,  // テレポートアンカー
    None                    = 100,
}

// スキルのパラメーターの列挙型
public enum EnumSkillParameter
{
    EnergyCost              = 000,  // エネルギー消費量
    EffectTime              = 001,  // 効果時間
    CoolDown                = 002,  // クールダウン
    EffectDetails           = 003,  // スキルの効果
    None                    = 100,
}

// スキルの効果の文章の列挙型
public enum EnumSkillDetails
{
    SuperCharge             = 000,  // スーパーチャージの効果
    PowerSurge              = 001,  // パワーサージの効果
    Huge                    = 002,  // 巨大化の効果
    GravityWave             = 003,  // 重力波の効果
    Frieze                  = 004,  // フリーズの効果
    GrapplingHook           = 005,  // グラップリングフックの効果
    Slow                    = 006,  // スローの効果
    InertialControl         = 007,  // インナーシャルコントロールの効果
    Blink                   = 008,  // ブリンクの効果
    TeleportAnchor          = 009,  // テレポートアンカーの効果
    None                    = 100,
}

// プレイヤー関連の列挙型
public enum EnumPlayer
{
    Charge                  = 000,  // チャージ
    None                    = 100,
}

// 主にポップアップで出る文章の列挙型
public enum EnumMessage
{
    WasDecided              = 000,  // 確定しました
    PleaseSelect3Skills     = 001,  // スキルを3つ選んでいないときのテキスト
    ExitGameText            = 002,  // ゲーム終了時のテキスト
    WasDestroyed            = 003,  // 惑星が破壊されたときのメッセージ
    None                    = 100,
}

// 星の名前の列挙型
public enum EnumStarName
{
    Sun                     = 000,  // 太陽
    Mercury                 = 001,  // 水星
    Venus                   = 002,  // 金星
    Earth                   = 003,  // 地球
    Mars                    = 004,  // 火星
    Jupiter                 = 005,  // 木星
    Saturn                  = 006,  // 土星
    Uranus                  = 007,  // 天王星
    Neptune                 = 008,  // 海王星
    BlackHole               = 009,  // ブラックホール
    None                    = 100,
}

// キー説明のテキストの列挙型
public enum EnumKeyGuide
{
    Positive                = 000,  // Positive 入力
    Negative                = 001,  // Negative 入力
    MoveCursol              = 002,  // 上下左右入力(カーソル移動)
    None                    = 100,
}

// システムの列挙型
public enum EnumSystem
{
    OK                      = 000,  // 決定
    Cancel                  = 001,  // キャンセル
    Apply                   = 002,  // 確定
    Reset                   = 003,  // リセット
    Start                   = 004,  // スタート
    Score                   = 005,  // スコア
    HighScore               = 006,  // ハイスコア
    NewRecord               = 007,  // 新記録
    None                    = 100,
}

[System.Serializable]
public struct Fonts
{
    public LanguageType language;
    public Font font;
}

// 画面関係の文字列を扱う構造体
[System.Serializable]
public struct ScreenStrings
{
    public EnumScreen type;
    public string[] text;
}

// ステージ名の文字列を扱う構造体
[System.Serializable]
public struct StageNameStrings
{
    public EnumStageName type;
    public string[] text;
}

// ミッションの文字列を扱う構造体
[System.Serializable]
public struct MissionStrings
{
    public EnumMission type;
    public string[] text;
}

// 設定画面の項目の名前の文字列を扱う構造体
[System.Serializable]
public struct ConfigTopStrings
{
    public EnumConfigTop type;
    public string[] text;
}

// 設定画面で設定可能な項目の名前の文字列を扱う構造体
[System.Serializable]
public struct ConfigContentStrings
{
    public AppParams.ParamsKey type;
    public string[] text;
}

// スキル名の文字列を扱う構造体
[System.Serializable]
public struct SkillNameStrings
{
    public EnumSkillName type;
    public string[] text;
}

// スキルのパラメーターの文字列を扱う構造体
[System.Serializable]
public struct SkillParameterStrings
{
    public EnumSkillParameter type;
    public string[] text;
}

// スキルの効果の文字列を扱う構造体
[System.Serializable]
public struct SkillDetailsStrings
{
    public EnumSkillDetails type;
    public string[] text;
}

// プレイヤー関連の文字列を扱う構造体
[System.Serializable]
public struct PlayerStrings
{
    public EnumPlayer type;
    public string[] text;
}

// 主にポップアップで出る文字列を扱う構造体
[System.Serializable]
public struct MessageStrings
{
    public EnumMessage type;
    public string[] text;
}

// 星の名前の文字列を扱う構造体
[System.Serializable]
public struct StarNameStrings
{
    public EnumStarName type;
    public string[] text;
}

// キー説明のテキストを扱う構造体
[System.Serializable]
public struct KeyGuideStrings
{
    public EnumKeyGuide type;
    public string[] text;
}

// システムの文字列を扱う構造体
[System.Serializable]
public struct SystemStrings
{
    public EnumSystem type;
    public string[] text;
}