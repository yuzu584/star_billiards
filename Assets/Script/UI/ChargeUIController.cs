using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// チャージのUIを管理
public class ChargeUIController : MonoBehaviour
{
    [SerializeField] private Shot shot; // InspectorでShotを指定

    // チャージのUIを描画
    public void DrawChargeUI(GameObject allChargeUI, Text chargeValue, Image chargeCircle)
    {
        // チャージされているなら
        if (shot.charge > 0)
        {
            // チャージのUIが無効化されていたら
            if (!(allChargeUI.activeSelf))
            {
                // UIを有効化
                allChargeUI.SetActive(true);
            }

            // チャージの数値をテキストで表示
            chargeValue.text = shot.charge.ToString("0") + "%";

            // チャージの円を描写
            chargeCircle.fillAmount = shot.charge / 100;
        }
        // チャージされていないかつ表示されているなら
        else if ((shot.charge == 0) && (allChargeUI.activeSelf))
        {
            // UIを無効化
            allChargeUI.SetActive(false);
        }
    }
}
