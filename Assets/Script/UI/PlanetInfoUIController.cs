using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// 惑星情報UIを管理
public class PlanetInfoUIController : MonoBehaviour
{
    [SerializeField] private Image targetRing;
    [SerializeField] private Text planetName;

    private UIController uICon;
    private Converter converter;
    private SphereRay sphereRay;
    private ScreenController scrCon;

    private void Start()
    {
        uICon = UIController.instance;
        converter = Converter.instance;
        sphereRay = SphereRay.instance;
        scrCon = ScreenController.instance;
    }

    // 惑星情報UIを描画
    void Draw(Vector3 position, string name)
    {
        targetRing.enabled = true;
        planetName.enabled = true;

        // 惑星情報UIの円のスクリーン座標を変更
        //uICon.planetInfoUI.targetRing.rectTransform.localPosition = converter.WSVConvert(position);
        targetRing.rectTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        // 惑星の名前UIのテキストを設定
        planetName.text = name;

        // 惑星の名前UIの位置を設定
        //uICon.planetInfoUI.planetName.rectTransform.localPosition = converter.WSVConvert(position);
        planetName.rectTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        // ゲーム中かつ対象が惑星なら描画
        if ((scrCon.Screen == ScreenController.ScreenType.InGame) && (sphereRay.hitObjectTag == "Planet"))
        {
            // 惑星情報UIを描画
            Draw(sphereRay.hitObjectPosition, sphereRay.hitObjectName);

            // 視点移動速度を遅くする
            TPSCamera.instance.rate = AppConst.CAMERA_SLOW_SPEED_RATE;
        }
        // 対象が惑星以外なら
        else
        {
            targetRing.enabled = false;
            planetName.enabled = false;

            // 視点移動速度を元に戻す
            TPSCamera.instance.rate = AppConst.CAMERA_DEFAULT_SPEED_RATE;
        }
    }
}
