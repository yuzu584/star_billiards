using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// スクロールバーの挙動を管理
public class ScrollBarController : Singleton<ScrollBarController>
{
    private ScreenController scrCon;
    private Focus focus;

    [System.Serializable]
    public struct ScrollBarStruct
    {
        public Scrollbar scrollbar;                     // スクロールバー
        public RectTransform parentRect;                // 親オブジェクトのRectTransform
        public RectTransform contentParentRect;         // スクロールされるコンテンツの親オブジェクトのRectTransform
        public ScreenController.ScreenType focusScreen; // スクロールバーをフォーカスする画面
        public Button.Group group;                      // スクロールするボタンのグループ
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
            // 画面とスクロールバーをフォーカスする画面が一致したら
            if (scrollBarStruct[i].focusScreen == scrCon.Screen)
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
