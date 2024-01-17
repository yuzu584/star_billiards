using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// スキルスロットを管理
public class SkillSlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image image;          // スキルスロットの画像
    [SerializeField] private Image imageOutline;   // スキルスロットの枠画像
    [SerializeField] private Color OnPointerColor; // ポインターが乗った時の色
    [SerializeField] private Color defaultColor;   // デフォルトの色
    [SerializeField] private float fadeTime;       // フェード時間

    // Findで探すもの
    private GameObject UIFunctionController;

    // Findで探したGameObjectのコンポーネント
    private Lerp lerp;

    // マウスポインターがボタンの上に乗ったら
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ボタンのアニメーション
        StopAllCoroutines();
        StartCoroutine(lerp.ChangeColor(image, defaultColor, OnPointerColor, fadeTime));
    }

    // マウスポインターがボタンの上から離れたら
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ボタンのアニメーション
        StopAllCoroutines();
        StartCoroutine(lerp.ChangeColor(image, OnPointerColor, defaultColor, fadeTime));
    }

    // ボタンがクリックされたら
    public void OnPointerClick(PointerEventData pointerEventData)
    {

    }

    void Start()
    {
        // GameObjectを探す
        UIFunctionController = GameObject.Find("UIFunctionController");

        // 探したGameObjectのコンポーネントを取得
        lerp = UIFunctionController.GetComponent<Lerp>();
    }
}
