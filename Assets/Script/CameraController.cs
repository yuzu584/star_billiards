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

                // カメラの位置を微調整
                transform.position = new Vector3(5.0f, 1.0f, 0.0f);

                // プレイヤーの子オブジェクトに設定
                transform.SetParent(player.transform, false);
            }

            // 視点移動
            tPSCamera.MoveCameraAngle();
        }
    }
}
