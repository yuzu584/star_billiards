using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 画面の種類を管理
public class ScreenController : MonoBehaviour
{
    [SerializeField] private UIController uIController;                 // InspectorでUIControllerを指定
    [SerializeField] private StageController stageController;           // InspectorでStageControllerを指定
    [SerializeField] private PauseUIController pauseUIController;       // InspectorでPauseUIControllerを指定
    [SerializeField] private ScreenData screenData;                     // InspectorでScreenDataを指定

    // 画面番号
    // 0 : ゲーム画面
    // 1 : ポーズ画面
    // 2 : ステージクリア画面
    // 3 : メインメニュー
    // 4 : ステージ選択画面
    public int screenNum = 3;

    private bool changeStageClearScreen = false; // ステージクリア画面に遷移したかどうか

    void Update()
    {
        // ゲーム中に戻るボタンが押されたら
        if(Input.GetButtonDown("Cancel") && screenNum == 0)
        {
            // ポーズ画面に遷移
            screenNum = 1;

            // ポーズ画面のUIを表示
            pauseUIController.DrawPauseUI(true);
        }

        // ステージをクリアかつ画面遷移していないなら
        if ((stageController.stageCrear) && (!changeStageClearScreen))
        {
            // ステージクリア画面に遷移済み
            changeStageClearScreen = true;

            // ステージクリア画面に遷移
            screenNum = 2;
        }
        // ステージ未クリアかつ画面遷移したなら
        else if ((!stageController.stageCrear) && (changeStageClearScreen))
        {
            // ステージクリア画面に未遷移
            changeStageClearScreen = false;
        }
    }
}
