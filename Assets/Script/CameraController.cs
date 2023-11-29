using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// メインカメラを管理
public class CameraController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定
    [SerializeField] private TPSCamera tPSCamera;               // InspectorでTPSCameraを指定
    [SerializeField] private FOV fOV;                           // InspectorでFOVを指定
    [SerializeField] private GameObject player;                 // プレイヤー

    private bool chasePlayer = false; // プレイヤーを追従しているかどうか

    static readonly Vector3 DEFAULT_STAGE_SELECT_POS = new Vector3(0.0f, 50.0f, 0.0f);    // ステージ選択画面のカメラの初期位置
    static readonly Vector3 DEFAULT_STAGE_SELECT_ANGLE = new Vector3(0.0f, -90.0f, 0.0f); // ステージ選択画面のカメラの初期の向き

    void Update()
    {
        // ゲーム画面なら
        if(screenController.screenNum == 0)
        {
            // プレイヤーを追従していなければ
            if (!chasePlayer)
            {
                // プレイヤーを追従
                chasePlayer = true;

                // カメラの座標を微調整
                transform.position = new Vector3(5.0f, 1.0f, 0.0f);

                // プレイヤーの子オブジェクトに設定
                transform.SetParent(player.transform, false);
            }

            // 視点移動
            tPSCamera.MoveCameraAngle();

            // 視野角を変更
            fOV.ChangeFOV();
        }
        // ステージ選択画面なら
        else if (screenController.screenNum == 4)
        {
            // 親子関係を解消
            if (chasePlayer)
                chasePlayer = false;

            if(transform.parent != null)
                transform.parent = null;

            // 座標と向きを変更
            transform.position = DEFAULT_STAGE_SELECT_POS;
            transform.rotation = Quaternion.Euler(DEFAULT_STAGE_SELECT_ANGLE);

            // 視野角をリセット
            fOV.ResetFOV();
        }
    }
}
