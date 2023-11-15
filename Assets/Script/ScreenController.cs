using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 画面の種類を管理
public class ScreenController : MonoBehaviour
{
    [SerializeField] private UIController uIController;                 // InspectorでUIControllerを指定
    [SerializeField] private StageController stageController;           // InspectorでStageControllerを指定
    [SerializeField] private PauseUIController pauseUIController;       // InspectorでPauseUIControllerを指定

    public int screenNum = 3; // 画面番号

    bool changeStageClearScreen = false; // ステージクリア画面に遷移したかどうか

    void Update()
    {
        // ゲーム中に戻るボタンが押されたら
        if(Input.GetButtonDown("Cancel") && screenNum == 0)
        {
            // ポーズ画面に遷移
            screenNum = 1;

            // ポーズ画面のUIを表示
            pauseUIController.DrawPauseUI(true);

            // 時間の流れを止める
            Time.timeScale = 0.0f;
        }

        // ステージをクリアかつ画面遷移していないなら
        if ((stageController.stageCrear) && (!changeStageClearScreen))
        {
            // ステージクリア画面に遷移済み
            changeStageClearScreen = true;

            // ステージクリア画面に遷移
            screenNum = 2;
        }

        // メインメニューなら
        if(screenNum == 3)
        {
            // 時間の流れを止める
            Time.timeScale = 0.0f;
        }
    }
}
