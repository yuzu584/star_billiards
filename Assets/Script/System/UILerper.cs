using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// UIの線形補間を行う
public class UILerper : MonoBehaviour
{
    private Lerp lerp;

    // 線形補完で "色" を変更する時に使用する構造体
    [System.Serializable]
    public struct LerpColor
    {
        public bool use;                                    // 線形補完で色を変更するか
        [System.NonSerialized]
        public Color defaultColor;                          // 初期色
        public Color changedColor;                          // 変化後の色
        public float fadeTime;                              // フェード時間
    }

    // 線形補完で "座標" を変更する時に使用する構造体
    [System.Serializable]
    public struct LerpPosition
    {
        public bool use;                                    // 線形補完で座標を変更するか
        [System.NonSerialized]
        public Vector3 defaultPos;                          // 初期座標
        public Vector3 posFluctuation;                      // 座標変化量
        public float fadeTime;                              // フェード時間
    }

    // 線形補完で "スケール" を変更する時に使用する構造体
    [System.Serializable]
    public struct LerpScale
    {
        public bool use;                                    // 線形補完でスケールを変更するか
        [System.NonSerialized] public Vector2 defaultScale; // 初期スケール
        public Vector2 scaleFluctuation;                    // スケール変化量
        public float fadeTime;                              // フェード時間
    }

    // Inspector に表示する線形補完用クラス
    [System.Serializable]
    public class LerpStruct
    {
        public UIBehaviour ui;                              // 線形補間を行うUI
        public LerpColor color;                             // 線形補完で "色" を変更する時に使用する構造体
        public LerpPosition position;                       // 線形補完で "座標" を変更する時に使用する構造体
        public LerpScale scale;                             // 線形補完で "スケール" を変更する時に使用する構造体
        public bool onPointerDraw;                          // ポインターが乗っているときのみ描画するか
        public Type uiType;                                 // ui の型

        // 初期化関数
        public void Init()
        {
            // UIの型を取得して代入
            uiType = ui.GetType();

            // 初期色・初期座標・初期スケールを取得
            if(uiType == typeof(Image))
            {
                Image image = (Image)ui;
                color.defaultColor = image.color;
                position.defaultPos = image.rectTransform.localPosition;
                scale.defaultScale = image.transform.localScale;
            }
            else if (uiType == typeof(Text))
            {
                Text text = (Text)ui;
                color.defaultColor = text.color;
                position.defaultPos = text.rectTransform.localPosition;
                scale.defaultScale = text.transform.localScale;
            }
        }
    }

    public LerpStruct[] lerpStructs;

    private void Awake()
    {
        lerp = gameObject.AddComponent<Lerp>();

        // クラス配列の初期化
        for (int i = 0; i < lerpStructs.Length; i++)
            lerpStructs[i].Init();
    }

    // アニメーション処理
    public void AnimProcess(bool orEnter)
    {
        // Lerp コンポーネントが null なら取得
        lerp ??= gameObject.AddComponent<Lerp>();

        lerp.StopAll();

        // ポインターが乗っているときのみ描画するなら描画
        for (int i = 0; i < lerpStructs.Length; i++)
        {
            if ((lerpStructs[i].onPointerDraw) && (lerpStructs[i].ui.enabled != orEnter))
                lerpStructs[i].ui.enabled = orEnter;
        }

        // アニメーション処理
        void Anim(LerpStruct lerpStructs)
        {
            Color c1, c2;                                                               // 開始時と終了時の色
            c1 = lerpStructs.color.defaultColor;                                        // 初期色
            c2 = lerpStructs.color.changedColor;                                        // 変化後の色

            Vector3 p1, p2;                                                             // 開始時と終了時の座標
            p1 = lerpStructs.position.defaultPos;                                       // 初期座標
            p2 = lerpStructs.position.defaultPos + lerpStructs.position.posFluctuation; // 初期座標 + 座標変化量

            Vector2 s1, s2;                                                             // 開始時と終了時のスケール
            s1 = lerpStructs.scale.defaultScale;                                        // 初期スケール
            s2 = lerpStructs.scale.defaultScale + lerpStructs.scale.scaleFluctuation;   // 初期スケール + スケール変化量

            // 型が Image なら Image 用の線形補間を行う
            if (lerpStructs.uiType == typeof(Image))
            {
                // "色" の線形補間を使用するかつポインターが "乗った時" なら、線形補間を行う
                if ((lerpStructs.color.use) && (orEnter))
                { StartCoroutine(lerp.Color_Image((Image)lerpStructs.ui, c1, c2, lerpStructs.color.fadeTime)); }

                // "色" の線形補間を使用するかつポインターが "離れた" 時なら、線形補間を行う
                else if ((lerpStructs.color.use) && (!orEnter))
                { StartCoroutine(lerp.Color_Image((Image)lerpStructs.ui, c2, c1, lerpStructs.color.fadeTime)); }

                // "座標" の線形補間を使用するかつポインターが "乗った時" なら、線形補間を行う
                if ((lerpStructs.position.use) && (orEnter))
                { StartCoroutine(lerp.Position_Image((Image)lerpStructs.ui, p1, p2, lerpStructs.position.fadeTime)); }

                // "座標" の線形補間を使用するかつポインターが "離れた時" なら、線形補間を行う
                else if ((lerpStructs.position.use) && (!orEnter))
                { StartCoroutine(lerp.Position_Image((Image)lerpStructs.ui, p2, p1, lerpStructs.position.fadeTime)); }

                // "スケール" の線形補間を使用するかつポインターが "乗った時" なら、線形補間を行う
                if ((lerpStructs.scale.use) && (orEnter))
                { StartCoroutine(lerp.Scale_Image((Image)lerpStructs.ui, s1, s2, lerpStructs.scale.fadeTime)); }

                // "スケール" の線形補間を使用するかつポインターが "離れた時" なら、線形補間を行う
                else if ((lerpStructs.scale.use) && (!orEnter))
                { StartCoroutine(lerp.Scale_Image((Image)lerpStructs.ui, s2, s1, lerpStructs.scale.fadeTime)); }
            }

            // 型が Text なら Text 用の線形補間を行う
            if (lerpStructs.uiType == typeof(Text))
            {
                // "色" の線形補間を使用するかつポインターが "乗った時" なら、線形補間を行う
                if ((lerpStructs.color.use) && (orEnter))
                { StartCoroutine(lerp.Color_Text((Text)lerpStructs.ui, c1, c2, lerpStructs.color.fadeTime)); }

                // "色" の線形補間を使用するかつポインターが "離れた" 時なら、線形補間を行う
                else if ((lerpStructs.color.use) && (!orEnter))
                { StartCoroutine(lerp.Color_Text((Text)lerpStructs.ui, c2, c1, lerpStructs.color.fadeTime)); }

                // "座標" の線形補間を使用するかつポインターが "乗った時" なら、線形補間を行う
                if ((lerpStructs.position.use) && (orEnter))
                { StartCoroutine(lerp.Position_Text((Text)lerpStructs.ui, p1, p2, lerpStructs.position.fadeTime)); }

                // "座標" の線形補間を使用するかつポインターが "離れた時" なら、線形補間を行う
                else if ((lerpStructs.position.use) && (!orEnter))
                { StartCoroutine(lerp.Position_Text((Text)lerpStructs.ui, p2, p1, lerpStructs.position.fadeTime)); }

                // "スケール" の線形補間を使用するかつポインターが "乗った時" なら、線形補間を行う
                if ((lerpStructs.scale.use) && (orEnter))
                { StartCoroutine(lerp.Scale_Text((Text)lerpStructs.ui, s1, s2, lerpStructs.scale.fadeTime)); }

                // "スケール" の線形補間を使用するかつポインターが "離れた時" なら、線形補間を行う
                else if ((lerpStructs.scale.use) && (!orEnter))
                { StartCoroutine(lerp.Scale_Text((Text)lerpStructs.ui, s2, s1, lerpStructs.scale.fadeTime)); }
            }
        }

        // UI要素のアニメーション
        for (int i = 0; i < lerpStructs.Length; i++)
        {
            Anim(lerpStructs[i]);
        }
    }

    // UI要素の初期化処理
    public void Init()
    {
        // UIの要素を初期化
        // 線形補完アニメーションを使用するUI要素の数繰り返す
        for (int i = 0; i < lerpStructs.Length; i++)
        {
            // UI要素の型が Image なら
            if (lerpStructs[i].uiType == typeof(Image))
            {
                // 値を変更するために型変換して代入しておく
                Image image = (Image)lerpStructs[i].ui;

                if (lerpStructs[i].color.use) image.color = lerpStructs[i].color.defaultColor;                                // 色を初期化
                if (lerpStructs[i].position.use) image.rectTransform.position = lerpStructs[i].position.defaultPos;           // 座標を初期化
                if (lerpStructs[i].scale.use) image.rectTransform.localScale = lerpStructs[i].scale.defaultScale;             // スケールを初期化

                if ((lerpStructs[i].onPointerDraw) && (lerpStructs[i].ui.enabled)) lerpStructs[i].ui.enabled = false;       // ポインターが乗っているときのみ描画するなら非表示
            }

            // UI要素の型が Text なら
            if (lerpStructs[i].uiType == typeof(Text))
            {
                // 値を変更するために型変換して代入しておく
                Text text = (Text)lerpStructs[i].ui;

                if (lerpStructs[i].color.use) text.color = lerpStructs[i].color.defaultColor;                                 // 色を初期化
                if (lerpStructs[i].position.use) text.rectTransform.position = lerpStructs[i].position.defaultPos;            // 座標を初期化
                if (lerpStructs[i].scale.use) text.rectTransform.localScale = lerpStructs[i].scale.defaultScale;              // スケールを初期化

                if ((lerpStructs[i].onPointerDraw) && (lerpStructs[i].ui.enabled)) lerpStructs[i].ui.enabled = false;       // ポインターが乗っているときのみ描画するなら非表示
            }
        }
    }
}
