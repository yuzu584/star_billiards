using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// 惑星情報UIを管理
public class PlanetInfoUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController; // InspectorでUIControllerを指定

    Vector3 PILine1; // 惑星情報UIの線の始点座標
    Vector3 PILine2; // 惑星情報UIの線の中間座標
    Vector3 PILine3; // 惑星情報UIの線の終点座標

    // 惑星情報UIを描画
    public void DrawPlanetInfoUI(Vector3 position, string planetName)
    {
        // 惑星情報UIの円のスクリーン座標を変更
        uIController.planetInfoUI.targetRing.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, position);

        // 惑星情報UIの線のスクリーン座標をワールド座標に変換
        PILine1 = Camera.main.ScreenToWorldPoint(uIController.planetInfoUI.targetRing.rectTransform.position + new Vector3(0, 0, 10));
        PILine2 = Camera.main.ScreenToWorldPoint(uIController.planetInfoUI.targetRing.rectTransform.position + new Vector3(50, 50, 10));
        PILine3 = Camera.main.ScreenToWorldPoint(uIController.planetInfoUI.targetRing.rectTransform.position + new Vector3(150, 50, 10));

        // 線を描画
        uIController.planetInfoUI.planetInfoLine.SetPosition(0, PILine1);
        uIController.planetInfoUI.planetInfoLine.SetPosition(1, PILine2);
        uIController.planetInfoUI.planetInfoLine.SetPosition(2, PILine3);

        // 惑星の名前をテキストに設定
        uIController.planetInfoUI.planetName.text = planetName;

        // 惑星の名前UIの位置を設定
        uIController.planetInfoUI.planetName.rectTransform.position = uIController.planetInfoUI.targetRing.rectTransform.position + new Vector3(160, 80, 10);
    }
}
