using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// チャージのUIを管理
public class ChargeUIController : MonoBehaviour
{
    [SerializeField] private Text chargeValue;
    [SerializeField] private Image chargeCircle;

    private Shot shot;

    private void Start()
    {
        shot = Shot.instance;
    }

    private void Update()
    {
        Draw();
    }

    // チャージのUIを描画
    void Draw()
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
