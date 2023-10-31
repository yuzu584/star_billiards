using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// ボタンの見た目を管理
public class ButtonGraphic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color color;                       // ポインターが乗った時の色
    [SerializeField] private Color defaultColor;                // デフォルトの色
    [SerializeField] private Image Btn;                         // ボタン
    [SerializeField] private ScreenController screenController; // ScreenController型
    [SerializeField] private enum ClickAction                   // ボタンが押されたときの効果
    {
        ReturnToGame,  // ゲームに戻る
        ConfigMenu,    // 設定画面を開く
        ReturnToTitle, // タイトル画面に戻る
    }

    [SerializeField] ClickAction clickAction; // ボタンを押したときの効果

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
    
    // ボタンがクリックされたら
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // ボタンごとの効果によって分岐
        switch (clickAction)
        {
            case ClickAction.ReturnToGame:  // ゲームに戻る
                ReturnToGame();
                break;
            case ClickAction.ConfigMenu:    // 設定画面を開く
                break;
            case ClickAction.ReturnToTitle: // タイトル画面に戻る
                break;
            default:
                break;
        }
    }

    // ゲームに戻る
    void ReturnToGame()
    {
        screenController.screenNum = 0;
    }
}
