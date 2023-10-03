using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // チャージのUI
    public GameObject chargeUI;

    // チャージの数値
    public Text chargeValue;

    // チャージの円
    public Image chargeCircle;

    // チャージの文字
    public Text chargeName;

    // エネルギーゲージ
    public Image EnergyGauge;

    // エネルギーゲージの枠
    public Image EnergyGaugeOutline;

    // エネルギーがない旨を伝えるテキスト
    public Text NoEnergy;

    void Start()
    {
        // エネルギーがない旨を伝えるテキストを非表示
        NoEnergy.enabled = false;
    }

    void Update()
    {
        // エネルギーゲージの増減を描画
        EnergyGauge.fillAmount = EnergyController.energy / EnergyController.maxEnergy;

        // チャージされているなら
        if (Shot.charge > 0)
        {
            // UIを有効化
            chargeUI.SetActive(true);

            // チャージの数値をテキストで表示
            chargeValue.text = Shot.charge.ToString("0") + "%";

            // チャージの円を描写
            chargeCircle.fillAmount = Shot.charge / 100;
        }
        // チャージされていないなら
        else if (Shot.charge == 0)
        {
            // UIを無効化
            chargeUI.SetActive(false);
        }

        // エネルギーが0以下なら
        if(EnergyController.energy <= 0)
        {
            // エネルギーゲージの枠を赤色にする
            EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);

            // エネルギーがない旨を伝えるテキストを表示
            NoEnergy.enabled = true;
        }
        else
        {
            // エネルギーゲージの枠を白色にする
            EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);

            // エネルギーがない旨を伝えるテキストを非表示
            NoEnergy.enabled = false;
        }
    }
}
