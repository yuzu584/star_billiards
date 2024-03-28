using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ボタンの要素を設定する
public class ButtonContentSetter : MonoBehaviour
{
    [System.Serializable]
    public struct Buttons
    {
        public Button[] btnNum;
    }
    
    [SerializeField] private Buttons[] btn;             // この配列内の Button の btnNum を連番で設定
    [SerializeField] private Buttons[] dontFocusBtn;    // この配列内の Button の btnNum を -1 に設定(フォーカスされなくする)

    private void Awake()
    {
        SetBtnNum();
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
            if (btn[i].btnNum.Length <= 0) continue;

            for (int j = 0; j < btn[i].btnNum.Length; j++)
            {
                btn[i].btnNum[j].btnNum = j;
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
            if (dontFocusBtn[i].btnNum.Length <= 0) continue;

            for (int j = 0; j < dontFocusBtn[i].btnNum.Length; j++)
            {
                dontFocusBtn[i].btnNum[j].btnNum = -1;
            }
        }
    }
}
