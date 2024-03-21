using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

// フォーカス関係処理を管理する
[DefaultExecutionOrder(-100)]
public class Focus : Singleton<Focus>
{
    public Button focusBtn;                  // フォーカスしているボタン
    public Button oldfocusBtn;               // フォーカスされていたボタン
    public Scrollbar focusScrollbar;         // フォーカスしているスクロールバー

    private ScreenController scrCon;
    private InputController input;
    private Sound sound;

    void Start()
    {
        scrCon = ScreenController.instance;
        input = InputController.instance;
        sound = Sound.instance;

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
    }

    // フォーカスするボタンを変える
    void ChangeBtnFocus(Vector2 mVec)
    {
        float minInput = 0.5f; // 入力を受け付ける最低値

        if (focusBtn != null)
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
        if (btn != focusBtn)
        {
            oldfocusBtn = focusBtn;
            focusBtn = btn;

            // フォーカスされたときの処理
            if (focusBtn != null)
                focusBtn.FocusProcess(true);

            // フォーカスが外れたときの処理
            if (oldfocusBtn != null)
                oldfocusBtn.FocusProcess(false);
        }

        // スクロールバーのスクロール処理
        // スクロールが必要な座標を計算
        if ((focusScrollbar != null) && (focusBtn.group == ScrollBarController.instance.scrollBarStruct[ScrollBarController.instance.num].group) && (!focusBtn.orPointer))
        {
            float posY;                                     // 基準となるY座標
            float maxY;                                     // スクロールを行う一番上のY座標
            float minY;                                     // スクロールを行う一番下のY座標
            float value;                                    // スクロール量
            int num;                                        // ScrollBarControllerのnum
            var instance = ScrollBarController.instance;    // ScrollBarControllerのインスタンス

            num = instance.num;

            // フォーカスされているボタンのY座標 + スクロールバーのY座標
            posY = focusBtn.gameObject.transform.localPosition.y + instance.scrollBarStruct[num].contentParentRect.localPosition.y;

            // 上端から少し低い座標
            maxY = -30.0f;

            // 下端から少し高い座標
            minY = -(instance.scrollBarStruct[num].parentRect.sizeDelta.y) + 30.0f;

            // フォーカスしたボタンが一定以上の座標ならスクロール処理
            if (posY > maxY)
            {
                // フォーカスされたボタンのY座標とスクロールを行うY座標の差を代入
                value = (posY - maxY);

                // 親オブジェクトの高さに対するY座標の差の割合を求めて代入
                value = Mathf.Abs(value / instance.scrollBarStruct[num].contentParentRect.sizeDelta.y);

                // スクロール処理
                instance.Scroll(focusScrollbar, true, value);
            }

            // フォーカスしたボタンが一定以下の座標ならスクロール処理
            else if (posY < minY)
            {
                // フォーカスされたボタンのY座標とスクロールを行うY座標の差を代入
                value = (posY - minY);

                // 親オブジェクトの高さに対するY座標の差の割合を求めて代入
                value = Mathf.Abs(value / instance.scrollBarStruct[num].contentParentRect.sizeDelta.y);

                // スクロール処理
                instance.Scroll(focusScrollbar, false, value);
            }
        }
    }
}