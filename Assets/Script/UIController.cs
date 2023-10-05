using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ゲーム中のUIを管理
public class UIController : MonoBehaviour
{
    // InspectorでUIの配列を指定
    [SerializeField]
    UIList UL;

    // UIの配列
    [System.Serializable]
    public class UIList
    {
        public GameObject chargeUI;       // チャージのUI
        public Image chargeCircle;        // チャージの円
        public Text chargeValue;          // チャージの数値
        public Text chargeName;           // チャージの文字
        public Image EnergyGauge;         // エネルギーゲージ
        public Image EnergyGaugeOutline;  // エネルギーゲージの枠
        public Text EnergyValue;          // エネルギーの数値
        public Text NoEnergy;             // エネルギーがない旨を伝えるテキスト
    }

    void Start()
    {
        // エネルギーがない旨を伝えるテキストを非表示
        UL.NoEnergy.enabled = false;
    }

    void Update()
    {
        // エネルギーゲージの増減を描画
        UL.EnergyGauge.fillAmount = EnergyController.energy / EnergyController.maxEnergy;

        // エネルギーの数値を表示
        UL.EnergyValue.text = EnergyController.energy.ToString("0");

        // チャージされているなら
        if (Shot.charge > 0)
        {
            // UIを有効化
            UL.chargeUI.SetActive(true);

            // チャージの数値をテキストで表示
            UL.chargeValue.text = Shot.charge.ToString("0") + "%";

            // チャージの円を描写
            UL.chargeCircle.fillAmount = Shot.charge / 100;
        }
        // チャージされていないなら
        else if (Shot.charge == 0)
        {
            // UIを無効化
            UL.chargeUI.SetActive(false);
        }

        // エネルギーが0以下なら
        if(EnergyController.energy <= 0)
        {
            // エネルギーゲージの枠を赤色にする
            UL.EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);

            // エネルギーゲージの数値を赤色にする
            UL.EnergyValue.color = new Color32(155, 0, 0, 100);

            // エネルギーがない旨を伝えるテキストを表示
            UL.NoEnergy.enabled = true;
        }
        else
        {
            // エネルギーゲージの枠を白色にする
            UL.EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);

            // エネルギーゲージの数値を白色にする
            UL.EnergyValue.color = new Color32(255, 255, 255, 100);

            // エネルギーがない旨を伝えるテキストを非表示
            UL.NoEnergy.enabled = false;
        }
    }
}
