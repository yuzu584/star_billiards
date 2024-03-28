using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// メインカメラを管理
public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player; // プレイヤー

    private ScreenController scrCon;
    private FOV fov;

    private bool chasePlayer = false;           // プレイヤーを追従しているかどうか
    
    void Start()
    {
        scrCon = ScreenController.instance;
        fov = FOV.instance;
    }

    void Update()
    {
        // ゲーム画面なら
        if(scrCon.Screen == ScreenController.ScreenType.InGame)
        {
            // プレイヤーを追従していなければ
            if (!chasePlayer)
            {
                // プレイヤーを追従
                chasePlayer = true;

                // 座標と向きを変更
                transform.position = AppConst.CAMERA_DEFAULT_IN_GAME_POS;
                transform.rotation = Quaternion.Euler(AppConst.CAMERA_DEFAULT_IN_GAME_ANGLE);

                // プレイヤーの子オブジェクトに設定
                transform.SetParent(player.transform, false);
            }

            // 視野角を変更
            fov.ChangeFOV();
        }
        // ステージ選択画面なら
        else if (scrCon.Screen == ScreenController.ScreenType.StageSelect)
        {
            // 親子関係を解消
            if (chasePlayer)
                chasePlayer = false;

            if(transform.parent != null)
                transform.parent = null;

            // 座標と向きを変更
            transform.position = AppConst.DEFAULT_STAGE_SELECT_POS;
            transform.rotation = Quaternion.Euler(AppConst.DEFAULT_STAGE_SELECT_ANGLE);
        }
    }
}
