using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// スクロールバーの挙動を管理
public class ScrollBarController : Singleton<ScrollBarController>
{
    public Scrollbar scrollbar;                     // スクロールバー
    public RectTransform parentRect;                // 親オブジェクトのRectTransform
    public RectTransform contentParentRect;         // スクロールされるコンテンツの親オブジェクトのRectTransform
    public Button.Group group;                      // スクロールするボタンのグループ

    public int num = 0;

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
