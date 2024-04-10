using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム内で使用する定数をまとめた名前空間
namespace AppConst
{
    public static class Const_Camera
    {
        public static readonly Vector3 DEFAULT_STAGE_SELECT_POS = new (0.0f, 100.0f, 0.0f);                 // ステージ選択画面のカメラの初期位置
        public static readonly Vector3 DEFAULT_STAGE_SELECT_ANGLE = new (30.0f, -90.0f, 0.0f);              // ステージ選択画面のカメラの初期の向き
        public static readonly Vector3 CAMERA_DEFAULT_IN_GAME_POS = new (5.0f, 1.0f, 0.0f);                 // ゲーム画面のカメラの初期位置
        public static readonly Vector3 CAMERA_DEFAULT_IN_GAME_ANGLE = new (0.0f, -90.0f, 0.0f);             // ゲーム画面のカメラの初期の向き
        public const float CAMERA_DEFAULT_SPEED_RATE = 1.0f;                                                // 視点移動速度の倍率の初期値
        public const float CAMERA_SLOW_SPEED_RATE = 0.5f;                                                   // 視点移動速度低下時の倍率
    }

    public static class Const_Player
    {
        public const float SPEED_MAINTENANCE_RATE = 1.008f;                                                 // プレイヤーの速度維持率
        public const float SPEED_REDUCTION_RATE = 0.95f;                                                    // プレイヤーの速度減少率
        public const float SPEED_THRESHOLD = 0.01f;                                                         // プレイヤーの速度のしきい値
        public const float PLAYER_DEFAULT_SPEED = 1.0f;                                                     // プレイヤーの移動速度の初期値
        public const int DEFAULT_CHARGE_SPEED = 1;                                                          // チャージ速度の初期値
        public const int DEFAULT_BOUNCE_POWER = 100;                                                        // 反発力の初期値
        public static readonly Vector3 PLAYER_DEFAULT_SCALE = new (0.5f, 0.5f, 0.5f);                       // プレイヤーのスケールの初期値
        public const float PREDICTION_LINE_START_WIDTH = 0.3f;                                              // 軌道予測線の始点の太さ
        public const float PREDICTION_LINE_END_WIDTH = 1.0f;                                                // 軌道予測線の終点の太さ
        public const float SPHERE_RAY_WIDTH = 10.0f;                                                        // 球体のRayの太さ
        public static readonly Vector3 PLATER_DEFAULT_POSITION = new (500.0f, 0.0f, 0.0f);                  // プレイヤーの初期位置
    }

    public static class Const_Skill
    {
        public const int SKILL_NUM = 10;                                                                    // スキルの数

        // スキルの構造体配列
        public static readonly Skill[] SKILLS = new Skill[SKILL_NUM]
        {
            // 使用エネルギー量, 効果時間, クールダウンを設定
            new (20    , 3.0f  , 10.0f ),
            new (20    , 3.0f  , 10.0f ),
            new (80    , 3.0f  , 10.0f ),
            new (100   , 3.0f  , 0.5f  ),
            new (50    , 3.0f  , 0.5f  ),
            new (50    , 3.0f  , 0.5f  ),
            new (100   , 3.0f  , 10.0f ),
            new (50    , 3.0f  , 10.0f ),
            new (20    , 3.0f  , 0.5f  ),
            new (50    , 3.0f  , 0.5f  ),
        };

        public const int CHARGE_SPEED_INCREASE_AMOUNT = 1;                                                  // スーパーチャージ使用時のチャージ速度増加量
        public const int BOUNCE_POWER_INCREASE_AMOUNT = 100;                                                // パワーサージ使用時の反発力増加量
        public const float SIZE_INCREASE_RATE = 5.0f;                                                       // 巨大化使用時のサイズ増加倍率
        public static readonly Color32 DEFAULT_SKILL_GAUGE_COLOR = new (255, 255, 255, 100);                // スキルゲージの初期色
        public static readonly Color32 CAN_USE_SKILL_GAUGE_COLOR = new (255, 255, 100, 200);                // スキル使用可能時のスキルゲージの色
        public const int SKILL_SLOT_AMOUNT = 3;                                                             // スキルスロットの数
    }

    public static class Const_Button
    {
        public static readonly Color32 SKILLSLOT_SELECTIMAGE_DEFAULT_COLOR = new(255, 255, 255, 10);        // スキルスロットの選択しているかを表す画像の初期色
        public static readonly Color32 SKILLSLOT_SELECTIMAGE_SELECT_COLOR = new(255, 255, 255, 200);        // スキルスロットの選択しているかを表す画像の選択時の色
    }

    public static class Const_System
    {
        public const float DEFAULT_TIME_SCALE = 1.0f;                                                       // 時間の速さの初期値
        public const float SLOW_TIME_SCALE = 0.1f;                                                          // スローモーション時の時間の速さ
    }

    // スキルの構造体
    public struct Skill
    {
        public int energyUsage;
        public float effectTime;
        public float coolDown;

        // コンストラクタ
        public Skill(int energyUsage, float effectTime, float coolDown)
        {
            this.energyUsage = energyUsage;
            this.effectTime = effectTime;
            this.coolDown = coolDown;
        }
    }
}