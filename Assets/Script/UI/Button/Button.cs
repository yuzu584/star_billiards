using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// ボタンの親クラス
public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Findで探すGameObject
    [System.NonSerialized] public GameObject ScreenController;
    [System.NonSerialized] public GameObject UIFunctionController;

    // Findで探したGameObjectのコンポーネント
    [System.NonSerialized] public ScreenController screenController;
    [System.NonSerialized] public Lerp lerp;

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
    public virtual void EnterProcess()
    {
        Debug.Log("ポインターが乗った時の処理が設定されていません。");
    }

    // マウスポインターが離れたときの処理
    public virtual void ExitProcess()
    {
        Debug.Log("ポインターが離れた時の処理が設定されていません。");
    }

    // クリックされたときの処理
    public virtual void ClickProcess()
    {
        Debug.Log("クリック時の処理が設定されていません。");
    }

    void Start()
    {
        // GameObjectを探す
        ScreenController = GameObject.Find("ScreenController");
        UIFunctionController = GameObject.Find("UIFunctionController");

        // 探したGameObjectのコンポーネントを取得
        screenController = ScreenController.gameObject.GetComponent<ScreenController>();
        lerp = UIFunctionController.GetComponent<Lerp>();
    }
}
