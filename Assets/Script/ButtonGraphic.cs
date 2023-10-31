using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// ボタンの見た目を管理
public class ButtonGraphic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color color;        // ポインターが乗った時の色
    [SerializeField] private Color defaultColor; // デフォルトの色
    [SerializeField] private Image Btn;          // ボタン

    // マウスポインターがボタンの上に乗ったら
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ボタンの色を変更
        Btn.color = color;
    }

    // マウスポインターがボタンの上から離れたら
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ボタンの色を元に戻す
        Btn.color = defaultColor;
    }
}
