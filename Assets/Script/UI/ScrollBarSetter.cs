using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ScrollBarController にスクロールバーをセットする
public class ScrollBarSetter : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;                       // スクロールバー
    [SerializeField] private RectTransform parentRect;                  // 親オブジェクトのRectTransform
    [SerializeField] private RectTransform contentParentRect;           // スクロールされるコンテンツの親オブジェクトのRectTransform
    public Button.Group group;                                          // スクロールするボタンのグループ

    private ScrollBarController scrollBarCon;

    private void Start()
    {
        scrollBarCon = ScrollBarController.instance;

        // 設定
        scrollBarCon.scrollbar = scrollbar;
        scrollBarCon.parentRect = parentRect;
        scrollBarCon.contentParentRect = contentParentRect;
        scrollBarCon.group = group;
    }

    private void OnDestroy()
    {
        // 削除
        scrollBarCon.scrollbar = null;
        scrollBarCon.parentRect = null;
        scrollBarCon.contentParentRect = null;
    }
}
