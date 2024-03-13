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
    [SerializeField] private Sound sound;                               // InspectorでSoundを指定
    [SerializeField] private Image switchImage;                         // 画面遷移時の画像

    [System.NonSerialized] public bool canStageDraw = false;            // ステージを描画可能か
    [System.NonSerialized] public int oldScreenNum = 0;                 // 前回の画面番号
    [System.NonSerialized] public int oldScreenLoot = 0;                // 前回の階層
    [System.NonSerialized] public int oldFrameScreenNum = 0;            // 1フレーム前の画面番号
    [System.NonSerialized] public int oldFrameScreenLoot = 0;           // 1フレーム前の階層

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
    public Button focusBtn;                  // フォーカスしているボタン
    public Button oldfocusBtn;               // フォーカスされていたボタン
    public Scrollbar focusScrollbar;         // フォーカスしているスクロールバー

    private bool changeStageClearScreen = false; // ステージクリア画面に遷移したかどうか
    
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
        yield return StartCoroutine(Color_Image(switchImage, c1, c2, 0.5f));
    }

    void Start()
    {
        input.game_OnPauseDele += OpenPause;
        input.ui_OnMoveDele += ChangeBtnFocus;
        input.ui_OnMoveDele += MoveSlider;

        // UI_Positive入力時のイベントを登録
        input.ui_OnPositiveDele += (float value) =>
        {
            // 音を再生
            StartCoroutine(sound.Play(focusBtn.ClickSound));

            // ボタンクリック時の処理
            focusBtn.ClickProcess();
        };

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

    void Update()
    {
        // 前回のフレームと現在のフレームで画面番号が異なったら
        if (screenNum != oldFrameScreenNum)
        {
            // 前回の画面番号を保存
            oldScreenNum = oldFrameScreenNum;

            // 1フレーム前の画面番号に現在の画面番号を代入
            oldFrameScreenNum = screenNum;

            // 画面遷移したときの処理
            if(changeScreen !=  null)
                changeScreen();
        }

        // 前回のフレームと現在のフレームで階層が異なったら
        if (ScreenLoot != oldFrameScreenLoot)
        {
            // 前回の階層を保存
            oldScreenLoot = oldFrameScreenLoot;

            // 1フレーム前の階層に現在の階層を代入
            oldFrameScreenLoot = ScreenLoot;

            // 階層が遷移したときの処理
            if (changeLoot != null)
                changeLoot();
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

    // ポーズ画面に遷移
    void OpenPause(float value)
    {
        if (value > 0)
            screenNum = 6;
    }

    // フォーカスするボタンを変える
    void ChangeBtnFocus(Vector2 mVec)
    {
        float minInput = 0.5f; // 入力を受け付ける最低値

        if(focusBtn != null)
        {
            if ((mVec.x > minInput) && (focusBtn.buttonRight != null))
            {
                SetFocusBtn(focusBtn.buttonRight);
            }
            else if ((mVec.x < -minInput) && (focusBtn.buttonLeft != null))
            {
                SetFocusBtn(focusBtn.buttonLeft);
            }
            else if ((mVec.y < -minInput) && (focusBtn.buttonDown != null))
            {
                SetFocusBtn(focusBtn.buttonDown);
            }
            else if ((mVec.y > minInput) && (focusBtn.buttonUp != null))
            {
                SetFocusBtn(focusBtn.buttonUp);
            }
        }
    }

    // スライダーを動かす(ボタンで)
    void MoveSlider(Vector2 mVec)
    {
        // フォーカスされているボタンからOptionsSliderが取得出来たら
        var sliderBtn = focusBtn.gameObject.GetComponent<OptionsSlider>();
        if (sliderBtn != null)
            sliderBtn.MoveSlider(mVec.x);
    }

    // フォーカスするボタンを設定
    public void SetFocusBtn(Button btn)
    {
        // 前回フォーカスされていたボタンと異なればセットする
        if(btn != focusBtn)
        {
            oldfocusBtn = focusBtn;
            focusBtn = btn;

            // フォーカスされたときの処理
            if(focusBtn != null)
                focusBtn.FocusProcess(true);

            // フォーカスが外れたときの処理
            if(oldfocusBtn != null)
                oldfocusBtn.FocusProcess(false);
        }

        // スクロールバーのスクロール処理
        // スクロールが必要な座標を計算
        if(focusScrollbar != null)
        {
            float pos;
            var instance = ScrollBarController.instance;
            int num = instance.num;
            pos = instance.scrollBarStruct[num].rTransform.sizeDelta.y / 2;
            pos += instance.scrollBarStruct[num].rTransform.localPosition.y;

            // フォーカスしたボタンが見切れそうな座標ならスクロール
            if (focusBtn.gameObject.transform.localPosition.y > pos)
                instance.Scroll(focusScrollbar, true);

            pos = -(instance.scrollBarStruct[num].rTransform.sizeDelta.y / 2);
            pos += instance.scrollBarStruct[num].rTransform.localPosition.y;
            if (focusBtn.gameObject.transform.localPosition.y < pos)
                instance.Scroll(focusScrollbar, false);
        }
    }
}
