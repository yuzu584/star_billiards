using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// 惑星情報UIを管理
public class PlanetInfoUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController; // InspectorでUIControllerを指定
    [SerializeField] private Converter converter;       // InspectorでConverterを指定

    // 惑星情報UIを描画
    public void DrawPlanetInfoUI(Vector3 position, string planetName)
    {
        // 惑星情報UIの円のスクリーン座標を変更
        uIController.planetInfoUI.targetRing.rectTransform.localPosition = converter.WSVConvert(position);

        // 惑星の名前UIのテキストを設定
        uIController.planetInfoUI.planetName.text = planetName;

        // 惑星の名前UIの位置を設定
        uIController.planetInfoUI.planetName.rectTransform.localPosition = converter.WSVConvert(position);
    }
}
