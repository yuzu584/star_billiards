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

    private InputController input;
    private FOV fov;
    private Lerp lerp;

    [System.NonSerialized] public ScreenType oldScreen = 0;             // 前回の画面
    [System.NonSerialized] public int oldScreenLoot = 0;                // 前回の階層
    [System.NonSerialized] public ScreenType oldFrameScreen = 0;        // 1フレーム前の画面
    [System.NonSerialized] public int oldFrameScreenLoot = 0;           // 1フレーム前の階層


    public enum ScreenType
    {
        Title,          // タイトル画面
        MainMenu,       // メインメニュー
        StageSelect,    // ステージ選択画面
        Options,        // 設定画面
        SkillSelect,    // スキル選択画面
        InGame,         // ゲーム画面
        Pause,          // ポーズ画面
        PlanetInfo,     // 惑星情報画面
        StageClear,     // ステージクリア画面
        GameOver,       // ゲームオーバー画面
    }

    [SerializeField] private ScreenType screen;     // 画面
    [SerializeField] private int screenLoot;        // 画面の階層
    public ScreenType Screen                        // 画面のプロパティ
    {
        get { return screen; }
        set { SwitchProcess(value); }
    }
    public int ScreenLoot                           // 画面の階層のプロパティ
    {
        get { return screenLoot; }
        set { screenLoot = value; }
    }
    public delegate void ChangeScreen();            // 画面が遷移したときのデリゲート
    public delegate void ChangeLoot();              // 階層が遷移したときのデリゲート
    public ChangeScreen changeScreen;
    public ChangeScreen changeLoot;
    
    // 画面遷移処理
    private void SwitchProcess(ScreenType scr)
    {
        // 遷移先の画面または現在の画面が遷移時にアニメーションを行うことになっていたら
        if ((scrData.screenList[(int)scr].enterAnim) || (scrData.screenList[(int)screen].exitAnim))
        {
            // アニメーションを行う
            lerp.StopAll();

            StartCoroutine(SetScreen(scr, true));
        }
        else
        {
            StartCoroutine(SetScreen(scr, false));
        }
    }

    // 画面を設定
    private IEnumerator SetScreen(ScreenType scr, bool orPlay)
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

        // 画面を設定
        screen = scr;
        
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
        input = InputController.instance;
        fov = FOV.instance;

        lerp = gameObject.AddComponent<Lerp>();
        input.game_OnPauseDele += OpenPause;

        // UI_Negative入力時のイベントを登録
        input.ui_OnNegativeDele += (float value) =>
        {
            // 階層が0より上なら1下げる
            if (ScreenLoot > 0)
                ScreenLoot -= (int)value;

            // メインメニューならタイトル画面に戻る
            else if (Screen == ScreenType.MainMenu)
            {
                Screen = ScreenType.Title;
            }
        };

        // 画面遷移時に階層をリセット
        changeScreen += () =>
        {
            ScreenLoot = 0;
        };

        // 画面遷移先が視野角をリセットする画面なら視野角をリセット
        changeScreen += () =>
        {
            if (scrData.screenList[(int)Screen].resetFov)
                fov.ResetFOV();
        };
    }

    // ポーズ画面に遷移
    void OpenPause(float value)
    {
        if (value > 0)
            screen = ScreenType.Pause;
    }
}
