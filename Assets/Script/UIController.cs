using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ゲーム中のUIを管理
public class UIController : MonoBehaviour
{
    // InspectorでUIの配列を指定
    [SerializeField] UIList UiList;

    // UIの配列
    [System.Serializable]
    public class UIList
    {
        public GameObject chargeUI;        // チャージのUI
        public Image chargeCircle;         // チャージの円
        public Text chargeValue;           // チャージの数値
        public Text chargeName;            // チャージの文字
        public Image EnergyGauge;          // エネルギーゲージ
        public Image EnergyAfterImage;  // エネルギーゲージの減少量
        public Image EnergyGaugeOutline;   // エネルギーゲージの枠
        public Text EnergyValue;           // エネルギーの数値
        public Text NoEnergy;              // エネルギーがない旨を伝えるテキスト
    }

    void Start()
    {
        // エネルギーがない旨を伝えるテキストを非表示
        UiList.NoEnergy.enabled = false;
    }

    void Update()
    {
        // エネルギーゲージの増減を描画
        UiList.EnergyGauge.fillAmount = EnergyController.energy / EnergyController.maxEnergy;

        if (UiList.EnergyAfterImage.fillAmount > EnergyController.energy / EnergyController.maxEnergy)
        {
            // エネルギーゲージの減少量を少しずつ減らす
            UiList.EnergyAfterImage.fillAmount -= 
                (UiList.EnergyAfterImage.fillAmount - EnergyController.energy / EnergyController.maxEnergy) * Time.deltaTime;
        }

        // エネルギーの数値を表示
        UiList.EnergyValue.text = EnergyController.energy.ToString("0");

        // チャージされているなら
        if (Shot.charge > 0)
        {
            // チャージのUIが無効化されていたら
            if (!(UiList.chargeUI.activeSelf))
            {
                // UIを有効化
                UiList.chargeUI.SetActive(true);
            }

            // チャージの数値をテキストで表示
            UiList.chargeValue.text = Shot.charge.ToString("0") + "%";

            // チャージの円を描写
            UiList.chargeCircle.fillAmount = Shot.charge / 100;
        }
        // チャージされていないかつ表示されているなら
        else if ((Shot.charge == 0) && (UiList.chargeUI.activeSelf))
        {
            // UIを無効化
            UiList.chargeUI.SetActive(false);
        }

        // エネルギーが0以下かつ非表示なら
        if((EnergyController.energy <= 0) && (UiList.NoEnergy.enabled == false))
        {
            // エネルギーゲージの枠を赤色にする
            UiList.EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);

            // エネルギーゲージの数値を赤色にする
            UiList.EnergyValue.color = new Color32(155, 0, 0, 100);

            // エネルギーがない旨を伝えるテキストを表示
            UiList.NoEnergy.enabled = true;
        }
        // エネルギーが0より上かつ表示されているなら
        else if ((EnergyController.energy > 0) && (UiList.NoEnergy.enabled == true))
        {
            // エネルギーゲージの枠を白色にする
            UiList.EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);

            // エネルギーゲージの数値を白色にする
            UiList.EnergyValue.color = new Color32(255, 255, 255, 255);

            // エネルギーがない旨を伝えるテキストを非表示
            UiList.NoEnergy.enabled = false;
        }
    }
}
