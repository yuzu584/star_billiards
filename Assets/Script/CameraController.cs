using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// メインカメラを管理
public class CameraController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定
    [SerializeField] private TPSCamera tPSCamera;               // InspectorでTPSCameraを指定
    [SerializeField] private FOV fOV;                           // InspectorでFOVを指定
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

                // 座標と向きを変更
                transform.position = AppConst.DEFAULT_IN_GAME_POS;
                transform.rotation = Quaternion.Euler(AppConst.DEFAULT_IN_GAME_ANGLE);

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
            transform.position = AppConst.DEFAULT_STAGE_SELECT_POS;
            transform.rotation = Quaternion.Euler(AppConst.DEFAULT_STAGE_SELECT_ANGLE);

            // 視野角をリセット
            fOV.ResetFOV();
        }
    }
}
