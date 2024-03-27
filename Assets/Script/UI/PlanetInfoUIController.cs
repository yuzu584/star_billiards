using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// 惑星情報UIを管理
public class PlanetInfoUIController : Singleton<PlanetInfoUIController>
{
    private UIController uICon;
    private Converter converter;

    private void Start()
    {
        uICon = UIController.instance;
        converter = Converter.instance;
    }

    // 惑星情報UIを描画
    public void DrawPlanetInfoUI(Vector3 position, string planetName)
    {
        // 惑星情報UIの円のスクリーン座標を変更
        //uICon.planetInfoUI.targetRing.rectTransform.localPosition = converter.WSVConvert(position);
        uICon.planetInfoUI.targetRing.rectTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        // 惑星の名前UIのテキストを設定
        uICon.planetInfoUI.planetName.text = planetName;

        // 惑星の名前UIの位置を設定
        //uICon.planetInfoUI.planetName.rectTransform.localPosition = converter.WSVConvert(position);
        uICon.planetInfoUI.planetName.rectTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
