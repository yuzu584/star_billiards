using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// メインカメラを管理
public class CameraController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定
    [SerializeField] private TPSCamera tPSCamera;               // InspectorでTPSCameraを指定
    [SerializeField] private GameObject player;                 // プレイヤー

    private bool chasePlayer = false; // プレイヤーを追従しているかどうか

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
        }
        // ステージ選択画面なら
        else if (screenController.screenNum == 4)
        {
            // 親子関係を解消
            chasePlayer = false;
            transform.parent = null;

            // 座標と向きを変更
            transform.position = new Vector3(0.0f, 50.0f, 0.0f);
            transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
    }
}
