using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ˜f¯î•ñUI‚ğŠÇ—
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

    // ˜f¯î•ñUI‚ğ•`‰æ
    void Draw(Vector3 position, string name)
    {
        targetRing.enabled = true;
        planetName.enabled = true;

        // ˜f¯î•ñUI‚Ì‰~‚ÌƒXƒNƒŠ[ƒ“À•W‚ğ•ÏX
        //uICon.planetInfoUI.targetRing.rectTransform.localPosition = converter.WSVConvert(position);
        targetRing.rectTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        // ˜f¯‚Ì–¼‘OUI‚ÌƒeƒLƒXƒg‚ğİ’è
        planetName.text = name;

        // ˜f¯‚Ì–¼‘OUI‚ÌˆÊ’u‚ğİ’è
        //uICon.planetInfoUI.planetName.rectTransform.localPosition = converter.WSVConvert(position);
        planetName.rectTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        bool isPlanet = sphereRay.hitObjectTag == "Planet";         // ‘ÎÛ‚ª˜f¯‚©
        bool isFixedStar = sphereRay.hitObjectTag == "FixedStar";   // ‘ÎÛ‚ªP¯‚©

        // ƒQ[ƒ€’†‚©‚Â‘ÎÛ‚ª˜f¯‚©P¯‚È‚ç•`‰æ
        if ((scrCon.Screen == ScreenController.ScreenType.InGame) && ((isPlanet || isFixedStar)))
        {
            // ˜f¯î•ñUI‚ğ•`‰æ
            Draw(sphereRay.hitObjectPosition, sphereRay.hitObjectName);

            // ‹“_ˆÚ“®‘¬“x‚ğ’x‚­‚·‚é
            TPSCamera.instance.rate = AppConst.CAMERA_SLOW_SPEED_RATE;
        }
        // ‘ÎÛ‚ª˜f¯‚©P¯ˆÈŠO‚È‚ç
        else
        {
            targetRing.enabled = false;
            planetName.enabled = false;

            // ‹“_ˆÚ“®‘¬“x‚ğŒ³‚É–ß‚·
            TPSCamera.instance.rate = AppConst.CAMERA_DEFAULT_SPEED_RATE;
        }
    }
}
