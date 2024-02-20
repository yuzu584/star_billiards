using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 何かを変換する
public class Converter : MonoBehaviour
{
    [SerializeField]
    private RectTransform canvasRect; // キャンバスのRectTransform

    // ワールド座標→スクリーン座標→ビューポート座標に変換して返す
    // Screen Spaceが Cameraの時に使用する
    // WSV(Would Screen ViewPort)
    public Vector2 WSVConvert(Vector3 targetPos)
    {
        Vector2 pos = Vector2.zero;
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, Camera.main, out pos);
        return pos;
    }
}
