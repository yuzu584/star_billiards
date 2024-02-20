using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 何かを変換する
public class Converter : MonoBehaviour
{
    [SerializeField]
    private RectTransform canvasRect; // キャンバスのRectTransform

    // 引数の座標をワールド座標→スクリーン座標→ビューポート座標に変換して返す
    // Screen Spaceが Cameraの時のみ使用可能
    // WSVは "Would" "Screen" "ViewPort" の頭文字を取ったもの
    public Vector2 WSVConvert(Vector3 targetPos)
    {
        Vector2 pos = Vector2.zero;
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, Camera.main, out pos);
        return pos;
    }
}
