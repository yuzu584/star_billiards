using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// チャージのUIを管理
public class ChargeUIController : MonoBehaviour
{
    [SerializeField] private Shot shot; // InspectorでShotを指定

    // チャージのUIを描画
    public void DrawChargeUI(bool draw, GameObject allChargeUI, Text chargeValue, Image chargeCircle)
    {
        // 描画するなら
        if (draw)
        {
            // チャージされているなら
            if (shot.charge > 0)
            {
                // チャージの数値をテキストで表示
                chargeValue.text = shot.charge.ToString("0") + "%";

                // チャージの円を描写
                chargeCircle.fillAmount = shot.charge / 100;
            }
        }

        // 表示/非表示切り替え
        if (allChargeUI.activeSelf != draw)
        {
            allChargeUI.SetActive(draw);
            chargeValue.enabled = draw;
            chargeCircle.enabled = draw;
        }
    }
}
