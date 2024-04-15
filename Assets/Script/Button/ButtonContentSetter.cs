using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ボタンの要素を設定する
public class ButtonContentSetter : MonoBehaviour
{
    // 水平か垂直か
    public enum HorizontalOrVertical
    {
        Horizontal,
        Vertical,
    }

    [System.Serializable]
    public struct Buttons
    {
        public HorizontalOrVertical horizontalOrVertical;   // ボタンの並びが水平か垂直か
        public Button[] buttons;
    }
    
    [SerializeField] private Buttons[] btn;                 // この配列内の Button の btnNum を連番で設定
    [SerializeField] private Buttons[] dontFocusBtn;        // この配列内の Button の btnNum を -1 に設定(フォーカスされなくする)

    private void Awake()
    {
        SetBtnNum();
        SetFocusBtn();
        SetDontFocusBtnNum();
    }

    // btnNum を一括設定
    void SetBtnNum()
    {
        // 配列の長さが 0 なら終了
        if (btn.Length <= 0) return;

        // 全てのボタンに順番に btnNum を代入
        for (int i = 0; i < btn.Length; i++)
        {
            // 配列の長さが 0 ならスキップ
            if (btn[i].buttons.Length <= 0) continue;

            for (int j = 0; j < btn[i].buttons.Length; j++)
            {
                // btnNum を連番で設定
                btn[i].buttons[j].btnNum = j;
            }
        }
    }

    // フォーカス先のボタンを一括設定
    void SetFocusBtn()
    {
        // 配列の長さが 0 なら終了
        if (btn.Length <= 0) return;

        // 全てのボタンのフォーカス先のボタンを設定
        for (int i = 0; i < btn.Length; i++)
        {
            // 配列の長さが 0 ならスキップ
            if (btn[i].buttons.Length <= 0) continue;
            
            // ボタンの向きが水平なら
            if (btn[i].horizontalOrVertical == HorizontalOrVertical.Horizontal)
            {
                // 左右のフォーカス先ボタンを設定
                for (int j = 0; j < btn[i].buttons.Length; j++)
                {
                    if (j > 0) btn[i].buttons[j].buttonLeft = btn[i].buttons[j - 1];
                    if(j < btn[i].buttons.Length - 1) btn[i].buttons[j].buttonRight = btn[i].buttons[j + 1];
                }
            }
            // ボタンの向きが垂直なら
            else if (btn[i].horizontalOrVertical == HorizontalOrVertical.Vertical)
            {
                // 上下のフォーカス先ボタンを設定
                for (int j = 0; j < btn[i].buttons.Length; j++)
                {
                    if (j > 0) btn[i].buttons[j].buttonUp = btn[i].buttons[j - 1];
                    if (j < btn[i].buttons.Length - 1) btn[i].buttons[j].buttonDown = btn[i].buttons[j + 1];
                }
            }
        }
    }

    // フォーカスしないボタンの btnNum を一括設定
    void SetDontFocusBtnNum()
    {
        // 配列の長さが 0 なら終了
        if (dontFocusBtn.Length <= 0) return;

        // 全てのボタンの btnNum に -1 を代入
        for (int i = 0; i < dontFocusBtn.Length; i++)
        {
            // 配列の長さが 0 ならスキップ
            if (dontFocusBtn[i].buttons.Length <= 0) continue;

            for (int j = 0; j < dontFocusBtn[i].buttons.Length; j++)
            {
                dontFocusBtn[i].buttons[j].btnNum = -1;
            }
        }
    }
}
