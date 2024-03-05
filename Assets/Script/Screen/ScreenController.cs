using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;
using UnityEngine.UI;

// 画面の種類を管理
public class ScreenController : Lerp
{
    [SerializeField] private UIController uIController;                 // InspectorでUIControllerを指定
    [SerializeField] private StageController stageController;           // InspectorでStageControllerを指定
    [SerializeField] private PauseUIController pauseUIController;       // InspectorでPauseUIControllerを指定
    [SerializeField] private ScreenData screenData;                     // InspectorでScreenDataを指定
    [SerializeField] private InputController input;                     // InspectorでInputControllerを指定
    [SerializeField] private Image switchImage;                         // 画面遷移時の画像

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

    [System.NonSerialized] public bool canStageDraw = false; // ステージを描画可能か

    private int screenNum;               // 画面番号
    public int ScreenNum                 // 画面番号のプロパティ
    {
        get { return screenNum; }
        set { SwitchProcess(value); }
    }
    [System.NonSerialized] public int oldScreenNum = 0;         // 前回の画面番号
    [System.NonSerialized] public int oldFrameScreenNum = 0;    // 1フレーム前の画面番号
    public delegate void ChangeScreen(); // 画面が遷移したときのデリゲート
    public ChangeScreen changeScreen;

    private bool changeStageClearScreen = false; // ステージクリア画面に遷移したかどうか
    private float pauseInputValue;               // ポーズ画面を開くボタンの入力
    
    // 画面遷移処理
    private void SwitchProcess(int num)
    {
        // 遷移先の画面または現在の画面が遷移時にアニメーションを行うことになっていたら
        if ((screenData.screenList[num].enterAnim) || (screenData.screenList[screenNum].exitAnim))
        {
            // アニメーションを行う
            StopAll();

            StartCoroutine(SetScreenNum(num, true));
        }
        else
        {
            StartCoroutine(SetScreenNum(num, false));
        }
    }

    // 画面番号を設定
    private IEnumerator SetScreenNum(int num, bool orPlay)
    {
        if (orPlay)
        {
            // 画面遷移アニメーション
            Color c1 = new Color(0, 0, 0, 0);
            Color c2 = new Color(0, 0, 0, 1);
            switchImage.gameObject.SetActive(true);
            switchImage.raycastTarget = true;
            yield return StartCoroutine(SwitchAnim(c1, c2));
        }

        // 画面番号を設定
        screenNum = num;
        
        if(orPlay)
        {
            // 画面遷移アニメーション
            Color c1 = new Color(0, 0, 0, 0);
            Color c2 = new Color(0, 0, 0, 1);
            switchImage.raycastTarget = false;
            yield return StartCoroutine(SwitchAnim(c2, c1));
            switchImage.gameObject.SetActive(false);
        }
    }

    // 画面遷移時のアニメーション
    private IEnumerator SwitchAnim(Color c1, Color c2)
    {
        yield return StartCoroutine(Color_Image(switchImage, c1, c2, 0.5f));
    }

    void Srart()
    {
        changeScreen();
    }

    void Update()
    {
        // ポーズ画面を開くボタンの入力を取得
        pauseInputValue = input.Game_Pause;

        // 前回のフレームと現在のフレームで画面番号が異なったら
        if (screenNum != oldFrameScreenNum)
        {
            // 前回の画面番号を保存
            oldScreenNum = oldFrameScreenNum;

            // 1フレーム前の画面番号に現在の画面番号を代入
            oldFrameScreenNum = screenNum;

            // 画面遷移したときの処理
            changeScreen();
        }

        // ゲーム中に戻るボタンが押されたら
        if ((pauseInputValue > 0) && (screenNum == 5))
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

        // ステージが描画可能かを管理する配列を更新
        if (canStageDraw != screenData.screenList[screenNum].drawStage)
            canStageDraw = screenData.screenList[screenNum].drawStage;
    }
}
