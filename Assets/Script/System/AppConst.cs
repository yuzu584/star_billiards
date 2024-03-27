using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Const
{
    public static class AppConst
    {
        public static readonly Vector3 DEFAULT_STAGE_SELECT_POS = new Vector3(0.0f, 100.0f, 0.0f);          // ステージ選択画面のカメラの初期位置
        public static readonly Vector3 DEFAULT_STAGE_SELECT_ANGLE = new Vector3(30.0f, -90.0f, 0.0f);       // ステージ選択画面のカメラの初期の向き
        public static readonly Vector3 CAMERA_DEFAULT_IN_GAME_POS = new Vector3(5.0f, 1.0f, 0.0f);          // ゲーム画面のカメラの初期位置
        public static readonly Vector3 CAMERA_DEFAULT_IN_GAME_ANGLE = new Vector3(0.0f, -90.0f, 0.0f);      // ゲーム画面のカメラの初期の向き
        public const float SPEED_MAINTENANCE_RATE = 1.008f;                                                 // プレイヤーの速度維持率
        public const float SPEED_REDUCTION_RATE = 0.95f;                                                    // プレイヤーの速度減少率
        public const float SPEED_THRESHOLD = 0.01f;                                                         // プレイヤーの速度のしきい値
        public const int SKILL_NUM = 10;                                                                    // スキルの数
        public static readonly string[] SKILL_NAME = new string[SKILL_NUM]                                  // スキル名
        {
            "SuperCharge",
            "PowerSurge",
            "Huge",
            "GravityWave",
            "Frieze",
            "GrapplingHook",
            "Slow",
            "InertialControl",
            "Blink",
            "TeleportAnchor",
        };
        public static readonly int[] SKILL_ENERGY_USAGE = new int[SKILL_NUM]                                // スキルのエネルギー消費量
        {
            20,
            20,
            80,
            100,
            50,
            50,
            100,
            50,
            20,
            50,
        };
        public static readonly float[] SKILL_COOLDOWN = new float[SKILL_NUM]                                // スキルのクールダウン
        {
            3.0f,
            3.0f,
            5.0f,
            5.0f,
            3.0f,
            3.0f,
            5.0f,
            3.0f,
            3.0f,
            5.0f,
        };
        public static readonly float[] SKILL_EFFECT_TIME = new float[SKILL_NUM]                             // スキルの効果時間
        {
            10.0f,
            10.0f,
            10.0f,
            0.5f,
            0.5f,
            0.5f,
            10.0f,
            10.0f,
            0.5f,
            0.5f,
        };
        public static readonly string[] SKILL_DETAILS = new string[SKILL_NUM]                               // スキルの効果を述べた文章
        {
            "Charge speed x2",
            "Player mass x2",
            "Player size x2",
            "Emit a shock wave",
            "Stop the movement",
            "Attract the planets",
            "Slow down time",
            "Easier to control inertia",
            "Move quickly",
            "Set up a teleport anchor. Reuse the skill to move to the teleport anchor location.",
        };
        public const float PLAYER_DEFAULT_SPEED = 1.0f;                                                     // プレイヤーの移動速度の初期値
        public const int DEFAULT_CHARGE_SPEED = 1;                                                          // チャージ速度の初期値
        public const int DEFAULT_BOUNCE_POWER = 100;                                                        // 反発力の初期値
        public static readonly Vector3 PLAYER_DEFAULT_SCALE = new Vector3(0.5f, 0.5f, 0.5f);                // プレイヤーのスケールの初期値
        public const int CHARGE_SPEED_INCREASE_AMOUNT = 1;                                                  // スーパーチャージ使用時のチャージ速度増加量
        public const int BOUNCE_POWER_INCREASE_AMOUNT = 100;                                                // パワーサージ使用時の反発力増加量
        public const float SIZE_INCREASE_RATE = 5.0f;                                                       // 巨大化使用時のサイズ増加倍率
        public const float PREDICTION_LINE_START_WIDTH = 0.3f;                                              // 軌道予測線の始点の太さ
        public const float PREDICTION_LINE_END_WIDTH = 1.0f;                                                // 軌道予測線の終点の太さ
        public const float SPHERE_RAY_WIDTH = 10.0f;                                                        // 球体のRayの太さ
        public const float DEFAULT_TIME_SCALE = 1.0f;                                                       // 時間の速さの初期値
        public const float SLOW_TIME_SCALE = 0.1f;                                                          // スローモーション時の時間の速さ
        public static readonly Vector3 PLATER_DEFAULT_POSITION = new Vector3(500.0f, 0.0f, 0.0f);           // プレイヤーの初期位置
        public static readonly Color32 DEFAULT_SKILL_GAUGE_COLOR = new Color32(255, 255, 255, 100);         // スキルゲージの初期色
        public static readonly Color32 CAN_USE_SKILL_GAUGE_COLOR = new Color32(255, 255, 100, 200);         // スキル使用可能時のスキルゲージの色
        public const int SKILL_SLOT_AMOUNT = 3;                                                             // スキルスロットの数
        public const int SCREEN_AMOUNT = 10;                                                                // スクリーンの数
        public const float CAMERA_DEFAULT_SPEED_RATE = 1.0f;                                                // 視点移動速度の倍率の初期値
        public const float CAMERA_SLOW_SPEED_RATE = 0.5f;                                                   // 視点移動速度低下時の倍率
    }
}