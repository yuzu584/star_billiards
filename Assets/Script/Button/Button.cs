using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// ボタンの親クラス
public class Button : Lerp, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // 線形補完で色を変更する時に使用する変数の構造体
    [System.Serializable]
    protected struct LerpColor
    {
        public bool useLerp;     // 線形補完で色を変更するか
        public Color startColor; // 変化前の色
        public Color endColor;   // 変化後の色
        public float fadeTime;   // フェード時間
    }

    // 線形補完で座標を変更する時に使用する変数の構造体
    [System.Serializable]
    protected struct LerpPosition
    {
        public bool useLerp;     // 線形補完で座標を変更するか
        public Vector3 startPos; // 変化前の座標
        public Vector3 endPos;   // 変化後の座標
        public float fadeTime;   // フェード時間
    }

    // 線形補完でスケールを変更する時に使用する変数の構造体
    [System.Serializable]
    protected struct LerpScale
    {
        public bool useLerp;       // 線形補完でスケールを変更するか
        public Vector2 startScale; // 変化前のスケール
        public Vector2 endScale;   // 変化後のスケール
        public float fadeTime;     // フェード時間
    }

    [System.Serializable]
    protected struct ImageStruct // 画像の構造体
    {
        public Image image;               // 画像
        public LerpColor lerpColor;       // 線形補完で色を変更する時に使用する変数の構造体
        public LerpPosition lerpPosition; // 線形補完で座標を変更する時に使用する変数の構造体
        public LerpScale lerpScale;       // 線形補完でスケールを変更する時に使用する変数の構造体
        public bool onPointerDraw;        // ポインターが乗っているときのみ描画するか
    }

    [System.Serializable]
    protected struct TextStruct  // テキストの構造体
    {
        public Text text;                 // テキスト
        public LerpColor lerpColor;       // 線形補完で色を変更する時に使用する変数の構造体
        public LerpPosition lerpPosition; // 線形補完で座標を変更する時に使用する変数の構造体
        public LerpScale lerpScale;       // 線形補完でスケールを変更する時に使用する変数の構造体
        public bool onPointerDraw;        // ポインターが乗っているときのみ描画するか
    }

    [SerializeField] protected ImageStruct[] imageStructs;
    [SerializeField] protected TextStruct[] textStructs;

    // ボタンが所属するグループ
    public enum Group
    {
        None,
        SkillList,
    }

    // Findで探すGameObject
    protected GameObject ScreenController;
    protected GameObject UIFunctionController;
    protected GameObject SoundController;
    protected GameObject InputObj;

    // Findで探したGameObjectのコンポーネント
    protected ScreenController screenController;
    protected Sound sound;
    protected InputController input;

    [SerializeField] protected int loot = 0;                // フォーカスされる階層
    [SerializeField] protected bool defaultFocus = false;   // 最初にフォーカスするボタンか
    public AudioClip EnterSound;                            // ポインターが乗った時に再生する音声ファイル
    public AudioClip ClickSound;                            // ボタンクリック時に再生する音声ファイル
    public Button buttonUp;                                 // 自分の上に位置するボタン
    public Button buttonDown;                               // 自分の下に位置するボタン
    public Button buttonLeft;                               // 自分の左に位置するボタン
    public Button buttonRight;                              // 自分の右に位置するボタン
    public Group group;                                     // このボタンが所属するグループ

    // ポインターによってフォーカスされたか
    // true  : ポインターによってフォーカスされた
    // false : ポインター以外(コントローラー入力など)でフォーカスされた
    public bool orPointer = false;

    // マウスポインターがボタンの上に乗ったら
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ポインターによってフォーカスされた
        orPointer = true;

        // 音を再生
        if (sound != null)
            StartCoroutine(sound.Play(EnterSound));

        // フォーカスされているボタンを設定
        screenController.SetFocusBtn(this);

        EnterProcess();

        orPointer = false;
    }

    // マウスポインターがボタンの上から離れたら
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        ExitProcess();
    }

    // ボタンがクリックされたら
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //音を再生
        if (sound != null)
            StartCoroutine(sound.Play(ClickSound));

        ClickProcess();
    }

    // マウスポインターが乗った時の処理
    public virtual void EnterProcess()
    {
        Debug.Log("ポインターが乗った時の処理が設定されていません。");
    }

    // マウスポインターが離れたときの処理
    public virtual void ExitProcess()
    {
        Debug.Log("ポインターが離れた時の処理が設定されていません。");
    }

    // クリックされたときの処理
    public virtual void ClickProcess()
    {
        Debug.Log("クリック時の処理が設定されていません。");
    }

    // ボタンのアニメーション処理
    protected void BtnAnimProcess(ImageStruct[] childrenImageStructs, TextStruct[] childrenTextStructs, bool enterOrExit)
    {
        Color color1;
        Color color2;
        Vector3 pos1;
        Vector3 pos2;
        Vector2 scale1;
        Vector2 scale2;

        StopAll();

        // ポインターが乗っているときのみ描画するなら描画
        for (int i = 0; i < childrenImageStructs.Length; i++)
        {
            if ((childrenImageStructs[i].onPointerDraw) && (childrenImageStructs[i].image.enabled != enterOrExit))
                childrenImageStructs[i].image.enabled = enterOrExit;
        }
        for (int i = 0; i < childrenTextStructs.Length; i++)
        {
            if ((childrenTextStructs[i].onPointerDraw) && (childrenTextStructs[i].text.enabled != enterOrExit))
                childrenTextStructs[i].text.enabled = enterOrExit;
        }

        // 画像のアニメーション
        for (int i = 0; i < childrenImageStructs.Length; i++)
        {
            // 色の線形補完を使用するなら
            if (childrenImageStructs[i].lerpColor.useLerp)
            {
                // ポインターが乗ったときか離れたときかで分岐
                if (enterOrExit)
                {
                    color1 = childrenImageStructs[i].lerpColor.startColor;
                    color2 = childrenImageStructs[i].lerpColor.endColor;
                }
                else
                {
                    color1 = childrenImageStructs[i].image.color;
                    color2 = childrenImageStructs[i].lerpColor.startColor;
                }

                // 線形補完
                StartCoroutine(Color_Image(childrenImageStructs[i].image, color1, color2, childrenImageStructs[i].lerpColor.fadeTime));
            }

            // 座標の線形補完を使用するなら
            if (childrenImageStructs[i].lerpPosition.useLerp)
            {
                // ポインターが乗ったときか離れたときかで分岐
                if (enterOrExit)
                {
                    pos1 = childrenImageStructs[i].lerpPosition.startPos;
                    pos2 = childrenImageStructs[i].lerpPosition.endPos;
                }
                else
                {
                    pos1 = childrenImageStructs[i].image.rectTransform.anchoredPosition;
                    pos2 = childrenImageStructs[i].lerpPosition.startPos;
                }

                // 線形補完
                StartCoroutine(Position_Image(childrenImageStructs[i].image, pos1, pos2, childrenImageStructs[i].lerpPosition.fadeTime));
            }

            // スケールの線形補完を使用するなら
            if (childrenImageStructs[i].lerpScale.useLerp)
            {
                // ポインターが乗ったときか離れたときかで分岐
                if (enterOrExit)
                {
                    scale1 = childrenImageStructs[i].lerpScale.startScale;
                    scale2 = childrenImageStructs[i].lerpScale.endScale;
                }
                else
                {
                    scale1 = childrenImageStructs[i].image.rectTransform.localScale;
                    scale2 = childrenImageStructs[i].lerpScale.startScale;
                }

                // 線形補完
                StartCoroutine(Scale_Image(childrenImageStructs[i].image, scale1, scale2, childrenImageStructs[i].lerpScale.fadeTime));
            }
        }

        // テキストのアニメーション
        for (int i = 0; i < childrenTextStructs.Length; i++)
        {
            // 色の線形補完を使用するなら
            if (childrenTextStructs[i].lerpColor.useLerp)
            {
                // ポインターが乗ったときか離れたときかで分岐
                if (enterOrExit)
                {
                    color1 = childrenTextStructs[i].lerpColor.startColor;
                    color2 = childrenTextStructs[i].lerpColor.endColor;
                }
                else
                {
                    color1 = childrenTextStructs[i].text.color;
                    color2 = childrenTextStructs[i].lerpColor.startColor;
                }

                // 線形補完
                StartCoroutine(Color_Text(childrenTextStructs[i].text, color1, color2, childrenTextStructs[i].lerpColor.fadeTime));
            }

            // 座標の線形補完を使用するなら
            if (childrenTextStructs[i].lerpPosition.useLerp)
            {
                // ポインターが乗ったときか離れたときかで分岐
                if (enterOrExit)
                {
                    pos1 = childrenTextStructs[i].lerpPosition.startPos;
                    pos2 = childrenTextStructs[i].lerpPosition.endPos;
                }
                else
                {
                    pos1 = childrenTextStructs[i].text.rectTransform.anchoredPosition;
                    pos2 = childrenTextStructs[i].lerpPosition.startPos;
                }

                // 線形補完
                StartCoroutine(Position_Text(childrenTextStructs[i].text, pos1, pos2, childrenTextStructs[i].lerpPosition.fadeTime));
            }

            // スケールの線形補完を使用するなら
            if (childrenTextStructs[i].lerpScale.useLerp)
            {
                // ポインターが乗ったときか離れたときかで分岐
                if (enterOrExit)
                {
                    scale1 = childrenTextStructs[i].lerpScale.startScale;
                    scale2 = childrenTextStructs[i].lerpScale.endScale;
                }
                else
                {
                    scale1 = childrenTextStructs[i].text.rectTransform.localScale;
                    scale2 = childrenTextStructs[i].lerpScale.startScale;
                }

                // 線形補完
                StartCoroutine(Scale_Text(childrenTextStructs[i].text, scale1, scale2, childrenTextStructs[i].lerpScale.fadeTime));
            }
        }
    }

    // ボタンの初期化処理
    protected void BtnInit(ImageStruct[] childrenImageStructs, TextStruct[] childrenTextStructs)
    {
        // ボタンの色をリセット
        for (int i = 0; i < childrenImageStructs.Length; i++)
        {
            if (childrenImageStructs[i].lerpColor.useLerp)
            {
                childrenImageStructs[i].image.color = childrenImageStructs[i].lerpColor.startColor;
            }
        }
        for (int i = 0; i < childrenTextStructs.Length; i++)
        {
            if (childrenTextStructs[i].lerpColor.useLerp)
                childrenTextStructs[i].text.color = childrenTextStructs[i].lerpColor.startColor;
        }

        // ボタンの座標をリセット
        for (int i = 0; i < childrenImageStructs.Length; i++)
        {
            if (childrenImageStructs[i].lerpPosition.useLerp)
                childrenImageStructs[i].image.rectTransform.position = childrenImageStructs[i].lerpPosition.startPos;
        }
        for (int i = 0; i < childrenTextStructs.Length; i++)
        {
            if (childrenTextStructs[i].lerpPosition.useLerp)
                childrenTextStructs[i].text.rectTransform.anchoredPosition = childrenTextStructs[i].lerpPosition.startPos;
        }

        // ボタンのスケールをリセット
        for (int i = 0; i < childrenImageStructs.Length; i++)
        {
            if (childrenImageStructs[i].lerpScale.useLerp)
                childrenImageStructs[i].image.rectTransform.localScale = childrenImageStructs[i].lerpScale.startScale;
        }
        for (int i = 0; i < childrenTextStructs.Length; i++)
        {
            if (childrenTextStructs[i].lerpScale.useLerp)
                childrenTextStructs[i].text.rectTransform.localScale = childrenTextStructs[i].lerpScale.startScale;
        }

        // ポインターが乗っているときのみ描画するなら非表示
        for (int i = 0; i < childrenImageStructs.Length; i++)
        {
            if ((childrenImageStructs[i].onPointerDraw) && (childrenImageStructs[i].image.enabled))
                childrenImageStructs[i].image.enabled = false;
        }
        for (int i = 0; i < childrenTextStructs.Length; i++)
        {
            if ((childrenTextStructs[i].onPointerDraw) && (childrenTextStructs[i].text.enabled))
                childrenTextStructs[i].text.enabled = false;
        }
    }

    // フォーカス関係の処理
    public void FocusProcess(bool isEnter)
    {
        if (this.gameObject.activeInHierarchy)
        {
            if (isEnter)
            {
                // 音を再生
                if(sound != null)
                    StartCoroutine(sound.Play(EnterSound));

                EnterProcess();
            }
            else
            {
                ExitProcess();
            }
        }
    }

    protected void Start()
    {
        // オブジェクトを探してコンポーネントを取得
        if (screenController == null)
        {
            ScreenController = GameObject.Find("ScreenController");
            screenController = ScreenController.gameObject.GetComponent<ScreenController>();
        }

        UIFunctionController = GameObject.Find("UIFunctionController");
        SoundController = GameObject.Find("SoundController");
        InputObj = GameObject.Find("Input");

        sound = SoundController.GetComponent<Sound>();
        input = InputObj.GetComponent<InputController>();

        screenController.changeLoot += () =>
        {
            // このボタンがフォーカスされる階層なら
            if ((loot == screenController.ScreenLoot) && (this.gameObject.activeInHierarchy))
            {
                if (defaultFocus)
                {
                    screenController.SetFocusBtn(this);

                    EnterProcess();
                }
            }
        };
    }

    protected void OnEnable()
    {
        // screenControllerが取得されていなければ取得
        if (screenController == null)
        {
            ScreenController = GameObject.Find("ScreenController");
            screenController = ScreenController.gameObject.GetComponent<ScreenController>();
        }

        // このボタンがフォーカスされる階層なら
        if (loot == screenController.ScreenLoot)
        {
            if (defaultFocus)
            {
                screenController.SetFocusBtn(this);

                EnterProcess();
            }
        }
    }
}
