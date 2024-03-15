using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// スクロールバーの挙動を管理(主にコントローラー使用時の)
public class ScrollBarController : Singleton<ScrollBarController>
{
    [SerializeField] ScreenController screenController;

    [System.Serializable]
    public struct ScrollBarStruct
    {
        public Scrollbar scrollbar;             // スクロールバー
        public RectTransform parentRect;        // 親オブジェクトのRectTransform
        public RectTransform contentParenRect;  // スクロールされるコンテンツの親オブジェクトのRectTransform
        public int focusScreen;                 // スクロールバーをフォーカスする画面番号
        public GameObject contentObj;           // スクロールする要素の親オブジェクト
        public int amount;                      // スクロールする要素の数
    }

    public int num = 0;

    public ScrollBarStruct[] scrollBarStruct;

    void Start()
    {
        // 画面遷移時にスクロールバーのフォーカスを設定する
        screenController.changeScreen += Focus;

        // スクロールする要素の数を取得して設定
        SetAmount();
    }

    // スクロールする要素の数を取得して設定
    void SetAmount()
    {
        for (int i = 0; i < scrollBarStruct.Length; ++i)
        {
            scrollBarStruct[i].amount = scrollBarStruct[i].contentObj.transform.childCount;
        }
    }

    // スクロールバーのフォーカス処理
    void Focus()
    {
        // フォーカスするスクロールバーを探す
        for(int i = 0; i < scrollBarStruct.Length; ++i)
        {
            // 画面番号とスクロールバーをフォーカスする画面番号が一致したら
            if (scrollBarStruct[i].focusScreen == screenController.ScreenNum)
            {
                // フォーカスするスクロールバーを設定
                num = i;
                screenController.focusScrollbar = scrollBarStruct[i].scrollbar;
                return;
            }
        }

        // フォーカスするスクロールバーが無ければフォーカスを外す
        screenController.focusScrollbar = null;
    }

    // スクロール処理
    public void Scroll(Scrollbar sBar, bool up, float value)
    {
        // スクロールする要素数を代入
        int amount = scrollBarStruct[num].amount;

        // 上にスクロールするなら
        if(up)
        {
            // バーを上に動かす
            sBar.value += (1.0f / amount / value);
        }

        // 下にスクロールするなら
        else
        {
            // バーを下に動かす
            sBar.value -= (1.0f / amount / value);
        }
    }
}
