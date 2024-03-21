using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// スクロールバーの挙動を管理(主にコントローラー使用時の)
public class ScrollBarController : Singleton<ScrollBarController>
{
    private ScreenController scrCon;
    private Focus focus;

    [System.Serializable]
    public struct ScrollBarStruct
    {
        public Scrollbar scrollbar;             // スクロールバー
        public RectTransform parentRect;        // 親オブジェクトのRectTransform
        public RectTransform contentParentRect; // スクロールされるコンテンツの親オブジェクトのRectTransform
        public int focusScreen;                 // スクロールバーをフォーカスする画面番号
        public Button.Group group;              // スクロールするボタンのグループ
    }

    public int num = 0;

    public ScrollBarStruct[] scrollBarStruct;

    void Start()
    {
        scrCon = ScreenController.instance;
        focus = Focus.instance;

        // 画面遷移時にスクロールバーのフォーカスを設定する
        scrCon.changeScreen += ScrollBarFocusProcess;
    }

    // スクロールバーのフォーカス処理
    void ScrollBarFocusProcess()
    {
        // フォーカスするスクロールバーを探す
        for(int i = 0; i < scrollBarStruct.Length; ++i)
        {
            // 画面番号とスクロールバーをフォーカスする画面番号が一致したら
            if (scrollBarStruct[i].focusScreen == scrCon.ScreenNum)
            {
                // フォーカスするスクロールバーを設定
                num = i;
                focus.focusScrollbar = scrollBarStruct[i].scrollbar;
                return;
            }
        }

        // フォーカスするスクロールバーが無ければフォーカスを外す
        focus.focusScrollbar = null;
    }

    // スクロール処理
    public void Scroll(Scrollbar sBar, bool up, float value)
    {
        // 上にスクロールするなら
        if(up)
        {
            // バーを上に動かす
            sBar.value += value * 3.0f;
        }

        // 下にスクロールするなら
        else
        {
            // バーを下に動かす
            sBar.value -= value * 3.0f;
        }
    }
}
