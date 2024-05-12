using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

// ボタンの親クラス
public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private const float BUTTON_LOCK_ALPHA = 0.1f;           // ボタンロック時の透明度

    public KeyGuide.KeyGuideIconAndTextType[] keyGuideTypes;

    // ボタンが所属するグループ
    public enum Group
    {
        None,
        SkillList,
    }

    // ボタンの音声ファイルの列挙型
    public enum BtnSounds
    {
        EnterSound,
        ClickSound,
        LockedSound,
    }

    // instanceを代入する変数
    protected ScreenController scrCon;
    protected Sound sound;
    protected InputController input;
    protected Focus focus;
    protected ButtonRecorder btnRec;
    protected KeyGuideUI keyGuideUI;

    // スクリーンと階層をまとめた構造体
    [System.Serializable]
    public struct ScreenAndLoot
    {
        public ScreenController.ScreenType scrType;
        public int scrLoot;
    }

    [SerializeField] protected bool defaultFocus = false;   // 最初にフォーカスするボタンか
    [SerializeField] protected bool lockButton = false;     // ボタンがロックされているか(ロックされているとボタンを押せなくなる)
    [SerializeField] protected AudioClip EnterSound;        // ポインターが乗った時に再生する音声ファイル
    [SerializeField] protected AudioClip ClickSound;        // ボタンクリック時に再生する音声ファイル
    [SerializeField] protected AudioClip LockedSound;       // ボタンロック時にボタンを押したとき再生する音声ファイル

    private bool oldLockButton = false;                     // Inspector更新時に lockButton の値が変わったか判定する用の変数

    public ScreenAndLoot scrAndLoot;                        // スクリーンと階層をまとめた構造体
    public Button buttonUp;                                 // 自分の上に位置するボタン
    public Button buttonDown;                               // 自分の下に位置するボタン
    public Button buttonLeft;                               // 自分の左に位置するボタン
    public Button buttonRight;                              // 自分の右に位置するボタン
    public Group group;                                     // このボタンが所属するグループ
    public int btnNum;                                      // このボタンの番号(主にフォーカスしていたボタンを保存するために使用する)

    protected bool isStartFocus = false;                    // StartFocus が実行されたか
    protected bool animating = false;                       // ボタンのアニメーション中か

    protected UILerper uiLerper;

    // ポインターによってフォーカスされたか
    // true  : ポインターによってフォーカスされた
    // false : ポインター以外(コントローラー入力など)でフォーカスされた
    public bool orPointer = false;

    private bool updateInspector = false;                   // Inspectorの値が更新されたか

    // マウスポインターがボタンの上に乗ったら
    public virtual void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ポインターによってフォーカスされた
        orPointer = true;

        EnterProcess();

        orPointer = false;
    }

    // マウスポインターがボタンの上から離れたら
    public virtual void OnPointerExit(PointerEventData pointerEventData)
    {
        //ExitProcess();
    }

    // ボタンがクリックされたら
    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        ClickProcess();
    }

    // マウスポインターが乗った時の処理
    public virtual void EnterProcess()
    {
        // 音を再生
        PlayBtnSound(BtnSounds.EnterSound);

        // ボタンのアニメーション処理
        if((uiLerper != null) && (!animating)) uiLerper.AnimProcess(true);
        animating = true;

        // フォーカスされているボタンを設定
        focus.SetFocusBtn(this);

        DrawKeyGuide();
    }

    // マウスポインターが離れたときの処理
    public virtual void ExitProcess()
    {
        // ボタンのアニメーション処理
        if ((uiLerper != null) && (animating)) uiLerper.AnimProcess(false);
        animating = false;
    }

    // クリックされたときの処理
    public virtual void ClickProcess()
    {
        // ボタンがロックされているかどうかで分岐
        if (lockButton)
        {
            // ボタンロック時の音を再生
            PlayBtnSound(BtnSounds.LockedSound);

            // ボタンが押せない旨を伝えるポップアップを生成
            PopupManager.instance.DrawPopup(PopupManager.PopupType.InMenuPopup1, Localize.instance.GetString("system", "button_lock_text"));
        }
        else 
        {
            // クリック時の音を再生
            PlayBtnSound(BtnSounds.ClickSound);
        }
    }

    // ボタンの音を再生
    public void PlayBtnSound(BtnSounds btnSounds)
    {
        sound ??= Sound.instance;

        // 再生する音によって分岐
        switch (btnSounds)
        {
            case BtnSounds.EnterSound:
                sound.Play(EnterSound);
                break;
            case BtnSounds.ClickSound:
                sound.Play(ClickSound);
                break;
            case BtnSounds.LockedSound:
                sound.Play(LockedSound);
                break;
            default:
                break;
        }
    }

    // フォーカス関係の処理
    public void FocusProcess(bool isEnter)
    {
        if (gameObject.activeInHierarchy)
        {
            // ポインターが乗ってフォーカスされた際は EnterProcess 実行済みなので
            // ポインターが乗って無いときのみ EnterProcess を実行
            if ((isEnter) && (!orPointer))
            {
                EnterProcess();
            }
            else if(!orPointer)
            {
                ExitProcess();
            }
        }
    }

    // 最初のフォーカス処理
    void StartFocus()
    {
        // 最初のフォーカス処理が実行済みなら終了
        if (isStartFocus) return;
        
        isStartFocus = true;

        // このボタンが保存済みか
        bool thisSaved = (btnRec.savedBtn[(int)scrCon.Screen].num[scrCon.ScreenLoot] == btnNum);

        // 何のボタンも保存されていないか(保存されていれば -1 以外になっている)
        bool somethingSaved = (btnRec.savedBtn[(int)scrCon.Screen].num[scrCon.ScreenLoot] == -1);

        // ボタン番号がフォーカス可能な番号か( -1 が入っていたらフォーカス不可)
        bool canFocus = (btnNum != -1);

        // このボタンが最初のボタンか(btnNum は連番で設定されているので 0 が入っていれば最初のボタンということになる)
        bool orFirst = (btnNum == 0);

        // 最初にフォーカスされるボタンなら(フォーカスの優先度 : 高)
        if (defaultFocus)
        {
            // フォーカス
            focus.SetFocusBtn(this);

            // フォーカス時のデバッグ処理が有効なら文字列を出力
            if (focus.focusDebugs.buttonStartFocusLog)
                Debug.Log($"type[{GetType()}] name[{name}] をデフォルトでフォーカス");
        }
        // ボタンが保存済みかつ btnNum が設定済みならフォーカス(フォーカスの優先度 : 中)
        else if ((thisSaved) && (canFocus))
        {
            // 自分を記録
            focus.SetFocusBtn(this);

            // フォーカス時のデバッグ処理が有効なら文字列を出力
            if (focus.focusDebugs.buttonStartFocusLog)
                Debug.Log($"type[{GetType()}] name[{name}] を保存済みでフォーカス");
        }
        // 何のボタンも保存されていないかつ一番最初のボタンならフォーカス(フォーカスの優先度 : 低)
        else if ((somethingSaved) && (orFirst))
        {
            // フォーカス
            focus.SetFocusBtn(this);

            // フォーカス時のデバッグ処理が有効なら文字列を出力
            if (focus.focusDebugs.buttonStartFocusLog)
                Debug.Log($"type[{GetType()}] name[{name}] をボタン番号0でフォーカス");
        }
    }

    // キー操作ガイドのUIを描画
    void DrawKeyGuide()
    {
        keyGuideUI ??= KeyGuideUI.instance;
        keyGuideUI.DrawGuide(keyGuideTypes);
    }

    private void Lock()
    {
        // 値に変更がなければ終了
        if (lockButton == oldLockButton) return;

        // 変更後の値を保存
        oldLockButton = lockButton;

        // ボタンがロックされたなら
        if (lockButton)
        {
            // CanvasGroup コンポーネントを追加
            var component = gameObject.AddComponent<CanvasGroup>();

            // ボタンを透けさせる
            component.alpha = BUTTON_LOCK_ALPHA;
        }
        else
        {
            // コンポーネント削除
            Destroy(GetComponent<CanvasGroup>());
        }
    }

    protected virtual void Start()
    {
        scrCon ??= ScreenController.instance;
        focus ??= Focus.instance;
        sound ??= Sound.instance;
        input = InputController.instance;
        btnRec ??= ButtonRecorder.instance;
        keyGuideUI ??= KeyGuideUI.instance;

        // StartFocus を階層遷移時に一回だけ実行
        scrCon.changeLoot += StartFocus;

        // 最初のフォーカス処理
        StartFocus();

        // ボタンのロック処理
        Lock();
    }

    private void Update()
    {
        // Inspectorに更新がなければ終了
        if (!updateInspector) return;

        // ボタンのロック処理
        Lock();
    }

    void OnValidate()
    {
        updateInspector = true;
    }

    protected virtual void OnEnable()
    {
        uiLerper ??= GetComponent<UILerper>();

        // 取得されていなければ取得
        scrCon ??= ScreenController.instance;
        focus ??= Focus.instance;
        btnRec ??= ButtonRecorder.instance;

        // UI要素の初期化処理
        if (uiLerper != null) uiLerper.Init();
    }

    protected virtual void OnDestroy()
    {
        scrCon.changeLoot -= StartFocus;
    }
}
