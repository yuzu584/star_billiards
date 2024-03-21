using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;
using UnityEngine.UI;

// 画面の種類を管理
[DefaultExecutionOrder(-100)]
public class ScreenController : Singleton<ScreenController>
{
    [SerializeField] private ScreenData scrData;
    [SerializeField] private Image switchImage;                         // 画面遷移時の画像

    private StageController stageCon;
    private InputController input;

    [System.NonSerialized] public int oldScreenNum = 0;                 // 前回の画面番号
    [System.NonSerialized] public int oldScreenLoot = 0;                // 前回の階層
    [System.NonSerialized] public int oldFrameScreenNum = 0;            // 1フレーム前の画面番号
    [System.NonSerialized] public int oldFrameScreenLoot = 0;           // 1フレーム前の階層
    private Lerp lerp;

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

    [SerializeField] private int screenNum;  // 画面番号
    [SerializeField] private int screenLoot; // 画面の階層
    public int ScreenNum                     // 画面番号のプロパティ
    {
        get { return screenNum; }
        set { SwitchProcess(value); }
    }
    public int ScreenLoot                    // 画面の階層のプロパティ
    {
        get { return screenLoot; }
        set { screenLoot = value; }
    }
    public delegate void ChangeScreen();     // 画面が遷移したときのデリゲート
    public delegate void ChangeLoot();       // 階層が遷移したときのデリゲート
    public ChangeScreen changeScreen;
    public ChangeScreen changeLoot;
    
    // 画面遷移処理
    private void SwitchProcess(int num)
    {
        // 遷移先の画面または現在の画面が遷移時にアニメーションを行うことになっていたら
        if ((scrData.screenList[num].enterAnim) || (scrData.screenList[screenNum].exitAnim))
        {
            // アニメーションを行う
            lerp.StopAll();

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
            input.DisableInputs();                           // InputSystemの入力を無効化
            Color c1 = new Color(0, 0, 0, 0);                // 透明
            Color c2 = new Color(0, 0, 0, 1);                // 黒
            switchImage.gameObject.SetActive(true);          // 画面サイズのImageを有効化
            switchImage.raycastTarget = true;                // 画面サイズのImageに判定を付与してマウス入力を受け付けないように
            yield return StartCoroutine(SwitchAnim(c1, c2)); // アニメーションが終わるまで待つ

            // 一瞬待つ
            yield return new WaitForSecondsRealtime(0.2f);
        }

        // 画面番号を設定
        screenNum = num;
        
        if(orPlay)
        {
            // 画面遷移アニメーション
            Color c1 = new Color(0, 0, 0, 0);                // 透明
            Color c2 = new Color(0, 0, 0, 1);                // 黒
            switchImage.raycastTarget = false;               // 画面サイズのImageに判定を消してマウス入力を受け付けるように
            yield return StartCoroutine(SwitchAnim(c2, c1)); // アニメーションが終わるまで待つ
            switchImage.gameObject.SetActive(false);         // 画面サイズのImageを無効化
        }
    }

    // 画面遷移時のアニメーション
    private IEnumerator SwitchAnim(Color c1, Color c2)
    {
        yield return StartCoroutine(lerp.Color_Image(switchImage, c1, c2, 0.5f));
    }

    void Start()
    {
        stageCon = StageController.instance;
        input = InputController.instance;

        lerp = gameObject.AddComponent<Lerp>();
        input.game_OnPauseDele += OpenPause;
        stageCon.stageCrearDele += () => { screenNum = 8; };

        // UI_Negative入力時のイベントを登録
        input.ui_OnNegativeDele += (float value) =>
        {
            // 階層が0より上なら1下げる
            if (ScreenLoot > 0)
                ScreenLoot -= (int)value;

            // メインメニューならタイトル画面に戻る
            else if (ScreenNum == 1)
            {
                ScreenNum = 0;
            }
        };
    }

    // ポーズ画面に遷移
    void OpenPause(float value)
    {
        if (value > 0)
            screenNum = 6;
    }
}
