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

    // UIが描画可能かを管理する配列
    // 0 : タイトル画面
    // 1 : メインメニュー
    // 2 : ステージ選択画面
    // 3 : 設定画面
    // 4 : スキル選択画面
    // 5 : ゲーム画面
    // 6 : ポーズ画面
    // 7 : 惑星リスト画面
    // 8 : ステージクリア画面
    public bool[] canUIDraw = new bool[9];

    public bool canStageDraw = false; // ステージを描画可能か

    public int screenNum = 1; // 画面番号

    private bool changeStageClearScreen = false; // ステージクリア画面に遷移したかどうか

    void Update()
    {
        // ゲーム中に戻るボタンが押されたら
        if(Input.GetButtonDown("Cancel") && screenNum == 5)
        {
            // ポーズ画面に遷移
            screenNum = 6;

            // ポーズ画面のUIを表示
            pauseUIController.DrawPauseUI(true);
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
        for (int i = 0; i < 9; i++)
        {
            if(canUIDraw[i] != screenData.screenList[screenNum].uIDrawList[i])
                canUIDraw[i] = screenData.screenList[screenNum].uIDrawList[i];
        }

        // ステージが描画可能かを管理する配列を更新
        for (int i = 0;i < 9; i++)
        {
            if (canStageDraw != screenData.screenList[screenNum].drawStage)
                canStageDraw = screenData.screenList[screenNum].drawStage;
        }
    }
}
