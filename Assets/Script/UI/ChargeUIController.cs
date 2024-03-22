using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// チャージのUIを管理
public class ChargeUIController : Singleton<ChargeUIController>
{
    [SerializeField] private Shot shot; // InspectorでShotを指定

    // チャージのUIを描画
    public void DrawChargeUI(GameObject allChargeUI, Text chargeValue, Image chargeCircle)
    {
        // チャージされているなら
        if (shot.charge > 0)
        {
            // チャージの数値をテキストで表示
            chargeValue.text = shot.charge.ToString("0") + "%";

            // チャージの円を描写
            chargeCircle.fillAmount = shot.charge / 100;
        }
        // チャージされていないなら
        else
        {
            // チャージのUIをリセット
            chargeValue.text = "0";
            chargeCircle.fillAmount = 0;
        }
    }
}
