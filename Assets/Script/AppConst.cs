using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Const
{
    public static class AppConst
    {
        public static readonly Vector3 DEFAULT_STAGE_SELECT_POS = new Vector3(0.0f, 100.0f, 0.0f);     // ステージ選択画面のカメラの初期位置
        public static readonly Vector3 DEFAULT_STAGE_SELECT_ANGLE = new Vector3(30.0f, -90.0f, 0.0f);  // ステージ選択画面のカメラの初期の向き
        public static readonly Vector3 CAMERA_DEFAULT_IN_GAME_POS = new Vector3(5.0f, 1.0f, 0.0f);     // ゲーム画面のカメラの初期位置
        public static readonly Vector3 CAMERA_DEFAULT_IN_GAME_ANGLE = new Vector3(0.0f, -90.0f, 0.0f); // ゲーム画面のカメラの初期の向き
        public const float SPEED_MAINTENANCE_RATE = 1.008f;                                            // プレイヤーの速度維持率
        public const float SPEED_REDUCTION_RATE = 0.95f;                                               // プレイヤーの速度減少率
        public const float SPEED_THRESHOLD = 0.01f;                                                    // プレイヤーの速度のしきい値
        public const int SKILL_NUM = 4;                                                                // スキルの数
        public static readonly string[] SKILL_NAME = new string[]                                      // スキル名
        {
            "SuperCharge",
            "PowerSurge",
            "Huge",
            "GravityWave",
        };
        public static readonly int[] SKILL_ENERGY_USAGE = new int[]                                  // スキルのエネルギー消費量
        {
            10,
            20,
            100,
            50,
        };
        public static readonly float[] SKILL_COOLDOWN = new float[]                                  // スキルのクールダウン
        {
            1.0f,
            1.0f,
            1.0f,
            1.0f,
        };
        public static readonly float[] SKILL_EFFECT_TIME = new float[]                               // スキルの効果時間
        {
            10.0f,
            10.0f,
            10.0f,
            1.0f,
        };
        public const float PLAYER_DEFAULT_SPEED = 1.0f;                                              // プレイヤーの移動速度の初期値
        public const int DEFAULT_CHARGE_SPEED = 1;                                                   // チャージ速度の初期値
        public const int DEFAULT_BOUNCE_POWER = 100;                                                 // 反発力の初期値
        public static readonly Vector3 PLAYER_DEFAULT_SCALE = new Vector3(0.5f, 0.5f, 0.5f);         // プレイヤーのスケールの初期値
        public const int CHARGE_SPEED_INCREASE_AMOUNT = 1;                                           // スーパーチャージ使用時のチャージ速度増加量
        public const int BOUNCE_POWER_INCREASE_AMOUNT = 100;                                         // パワーサージ使用時の反発力増加量
        public const float SIZE_INCREASE_RATE = 5.0f;                                                // 巨大化使用時のサイズ増加倍率
        public const float PREDICTION_LINE_START_WIDTH = 0.1f;                                       // 軌道予測線の始点の太さ
        public const float PREDICTION_LINE_END_WIDTH = 1.0f;                                         // 軌道予測線の終点の太さ
        public const float SPHERE_RAY_WIDTH = 10.0f;                                                 // 球体のRayの太さ
        public const float DEFAULT_TIME_SCALE = 1.0f;                                                // 時間の速さの初期値
        public const float SLOW_TIME_SCALE = 0.1f;                                                   // スローモーション時の時間の速さ
        public static readonly Vector3 PLATER_DEFAULT_POSITION = new Vector3(500.0f, 0.0f, 0.0f);    // プレイヤーの初期位置
        public static readonly Color32 DEFAULT_SKILL_GAUGE_COLOR = new Color32(255, 255, 255, 100);  // スキルゲージの初期色
        public static readonly Color32 CAN_USE_SKILL_GAUGE_COLOR = new Color32(255, 255, 100, 200);    // スキル使用可能時のスキルゲージの色
    }
}
