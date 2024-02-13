using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// ボタンの親クラス
public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Findで探すGameObject
    protected GameObject ScreenController;
    protected GameObject UIFunctionController;

    // Findで探したGameObjectのコンポーネント
    protected ScreenController screenController;
    protected Lerp lerp;

    // マウスポインターがボタンの上に乗ったら
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        EnterProcess();
    }

    // マウスポインターがボタンの上から離れたら
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        ExitProcess();
    }

    // ボタンがクリックされたら
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        ClickProcess();
    }

    // マウスポインターが乗った時の処理
    protected virtual void EnterProcess()
    {
        Debug.Log("ポインターが乗った時の処理が設定されていません。");
    }

    // マウスポインターが離れたときの処理
    protected virtual void ExitProcess()
    {
        Debug.Log("ポインターが離れた時の処理が設定されていません。");
    }

    // クリックされたときの処理
    protected virtual void ClickProcess()
    {
        Debug.Log("クリック時の処理が設定されていません。");
    }

    protected void Start()
    {
        // オブジェクトを探してコンポーネントを取得
        ScreenController = GameObject.Find("ScreenController");
        UIFunctionController = GameObject.Find("UIFunctionController");
        screenController = ScreenController.gameObject.GetComponent<ScreenController>();
        lerp = UIFunctionController.GetComponent<Lerp>();
    }
}
