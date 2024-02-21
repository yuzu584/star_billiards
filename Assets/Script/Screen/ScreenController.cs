using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// 画面の種類を管理
public class ScreenController : MonoBehaviour
{
    [SerializeField] private UIController uIController;                 // InspectorでUIControllerを指定
    [SerializeField] private StageController stageController;           // InspectorでStageControllerを指定
    [SerializeField] private PauseUIController pauseUIController;       // InspectorでPauseUIControllerを指定
    [SerializeField] private ScreenData screenData;                     // InspectorでScreenDataを指定

    // UIが描画可能かを管理する配列
    // 0 : タイトル画面
    // 1 : メインメニュー
    // 2 : ステージ選択画面
    // 3 : 設定画面
    // 4 : スキル選択画面
    // 5 : ゲーム画面
    // 6 : ポーズ画面
    // 7 : 
    // 8 : ステージクリア画面
    // 9 : ゲームオーバー画面
    [System.NonSerialized] public bool[] canUIDraw = new bool[AppConst.SCREEN_AMOUNT];

    [System.NonSerialized] public bool canStageDraw = false; // ステージを描画可能か

    public int screenNum = 0;    // 画面番号
    public int oldScreenNum = 0; // 1フレーム前の画面番号
    public delegate void ChangeScreen(); // 画面が遷移したときのデリゲート
    public ChangeScreen changeScreen;

    private bool changeStageClearScreen = false; // ステージクリア画面に遷移したかどうか

    void Update()
    {
        // 前回のフレームと現在のフレームで画面番号が異なったら
        if(screenNum != oldScreenNum)
        {
            changeScreen();
            oldScreenNum = screenNum;
        }

        // ゲーム中に戻るボタンが押されたら
        if (Input.GetButtonDown("Cancel") && screenNum == 5)
        {
            // ポーズ画面に遷移
            screenNum = 6;
        }

        // ステージをクリアかつ画面遷移していないなら
        if ((stageController.stageCrear) && (!changeStageClearScreen))
        {
            // ステージクリア画面に遷移済み
            changeStageClearScreen = true;

            // ステージクリア画面に遷移
            screenNum = 8;
        }
        // ステージ未クリアかつ画面遷移したなら
        else if ((!stageController.stageCrear) && (changeStageClearScreen))
        {
            // ステージクリア画面に未遷移
            changeStageClearScreen = false;
        }

        // UIが描画可能かを管理する配列を更新
        for (int i = 0; i < AppConst.SCREEN_AMOUNT; i++)
        {
            if(canUIDraw[i] != screenData.screenList[screenNum].uIDrawList[i])
                canUIDraw[i] = screenData.screenList[screenNum].uIDrawList[i];
        }

        // ステージが描画可能かを管理する配列を更新
        if (canStageDraw != screenData.screenList[screenNum].drawStage)
            canStageDraw = screenData.screenList[screenNum].drawStage;
    }
}
