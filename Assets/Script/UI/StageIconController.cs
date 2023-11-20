using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

// ステージ選択画面のアイコンを管理
public class StageIconController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private StageController stageController; // InspectorでStageControllerを指定
    [SerializeField] private enum ClickAction                 // アイコンが押されたときの挙動
    {
        solarSystem,
    }
    [SerializeField] private ClickAction clickAction; // アイコンが押されたときの挙動

    // マウスポインターがアイコンの上に乗ったら
    public void OnPointerEnter(PointerEventData pointerEventData)
    {

    }

    // マウスポインターがアイコンの上から離れたら
    public void OnPointerExit(PointerEventData pointerEventData)
    {

    }

    // アイコンがクリックされたら
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // アイコンの種類によって分岐
        switch (clickAction)
        {
            case ClickAction.solarSystem: // 太陽系

                // ステージ番号を太陽系に設定
                stageController.stageNum = 0;
                break;
        }
    }
}
