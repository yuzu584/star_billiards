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
        public GameObject chargeUI;         // チャージのUI
        public Image chargeCircle;          // チャージの円
        public Text chargeValue;            // チャージの数値
        public Text chargeName;             // チャージの文字
        public Image EnergyGauge;           // エネルギーゲージ
        public Image EnergyAfterImage;      // エネルギーゲージの減少量
        public Image EnergyGaugeOutline;    // エネルギーゲージの枠
        public Text EnergyValue;            // エネルギーの数値
        public Text NoEnergy;               // エネルギーがない旨を伝えるテキスト
        public Text skillName;              // スキル名
        public Image skillGauge;            // 効果時間とクールダウンのゲージ
        public Image planetInfoRing;        // 惑星情報UIの円
        public LineRenderer planetInfoLine; // 惑星情報UIの線
        public Text planetName;             // 惑星の名前
    }

    [SerializeField] Shot shot;                         // Shot型の変数
    [SerializeField] EnergyController energyController; // EnergyController型の変数

    RectTransform PIR = null; // 惑星情報UIの円のスクリーン座標
    Vector3 PIL1;             // 惑星情報UIの線の始点座標
    Vector3 PIL2;             // 惑星情報UIの線の中間座標
    Vector3 PIL3;             // 惑星情報UIの線の終点座標

    void Start()
    {
        // 惑星情報UIの円のRectTransformを取得
        PIR = UiList.planetInfoRing.GetComponent<RectTransform>();

        // 惑星情報UIの線の始点と終点の太さを指定
        UiList.planetInfoLine.startWidth = 0.01f;
        UiList.planetInfoLine.endWidth = 0.01f;

        // 惑星情報UIの線の数
        UiList.planetInfoLine.positionCount = 3;

        // エネルギーがない旨を伝えるテキストを非表示
        UiList.NoEnergy.enabled = false;
    }

    void Update()
    {
        // エネルギーのUIを描画
        DrawEnergyUI();

        // チャージのUIを描画
        DrawChargeUI();
    }

    // エネルギーのUIを描画
    void DrawEnergyUI()
    {
        // エネルギーゲージの増減を描画
        UiList.EnergyGauge.fillAmount = energyController.energy / energyController.maxEnergy;

        if (UiList.EnergyAfterImage.fillAmount > energyController.energy / energyController.maxEnergy)
        {
            // エネルギーゲージの減少量を少しずつ減らす
            UiList.EnergyAfterImage.fillAmount -=
                (UiList.EnergyAfterImage.fillAmount - energyController.energy / energyController.maxEnergy) * Time.deltaTime;
        }

        // エネルギーの数値を表示
        UiList.EnergyValue.text = energyController.energy.ToString("0");

        // エネルギーが0以下かつ非表示なら
        if ((energyController.energy <= 0) && (UiList.NoEnergy.enabled == false))
        {
            // エネルギーゲージの枠と数値を赤色にする
            UiList.EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);
            UiList.EnergyValue.color = new Color32(155, 0, 0, 100);

            // エネルギーがない旨を伝えるテキストを表示
            UiList.NoEnergy.enabled = true;
        }
        // エネルギーが0より上かつ表示されているなら
        else if ((energyController.energy > 0) && (UiList.NoEnergy.enabled == true))
        {
            // エネルギーゲージの枠と数値を白色にする
            UiList.EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);
            UiList.EnergyValue.color = new Color32(255, 255, 255, 255);

            // エネルギーがない旨を伝えるテキストを非表示
            UiList.NoEnergy.enabled = false;
        }
    }

    // チャージのUIを描画
    void DrawChargeUI()
    {
        // チャージされているなら
        if (shot.charge > 0)
        {
            // チャージのUIが無効化されていたら
            if (!(UiList.chargeUI.activeSelf))
            {
                // UIを有効化
                UiList.chargeUI.SetActive(true);
            }

            // チャージの数値をテキストで表示
            UiList.chargeValue.text = shot.charge.ToString("0") + "%";

            // チャージの円を描写
            UiList.chargeCircle.fillAmount = shot.charge / 100;
        }
        // チャージされていないかつ表示されているなら
        else if ((shot.charge == 0) && (UiList.chargeUI.activeSelf))
        {
            // UIを無効化
            UiList.chargeUI.SetActive(false);
        }
    }

    // スキルのUIを描画
    public void DrawSkillUI(string skillName, float coolDown, float effectTime, float nowCoolDown, float nowEffectTime)
    {
        // テキストを現在のスキル名に変更
        UiList.skillName.text = skillName;

        // 効果時間を描画
        if (nowEffectTime > 0)
            UiList.skillGauge.fillAmount = nowEffectTime / effectTime;
        // 効果時間が経過していたならクールダウンを描画
        else if (nowCoolDown > 0)
            UiList.skillGauge.fillAmount = (coolDown - nowCoolDown) / coolDown;
    }

    // 惑星情報UIを描画
    public void DrawPlanetInfoUI(Vector3 position, string planetName)
    {
        // 惑星情報UIの円のスクリーン座標を変更
        UiList.planetInfoRing.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, position);

        // 惑星情報UIの線のスクリーン座標をワールド座標に変換
        PIL1 = Camera.main.ScreenToWorldPoint(UiList.planetInfoRing.rectTransform.position + new Vector3(0, 0, 10));
        PIL2 = Camera.main.ScreenToWorldPoint(UiList.planetInfoRing.rectTransform.position + new Vector3(50, 50, 10));
        PIL3 = Camera.main.ScreenToWorldPoint(UiList.planetInfoRing.rectTransform.position + new Vector3(150, 50, 10));

        // 線を描画
        UiList.planetInfoLine.SetPosition(0, PIL1);
        UiList.planetInfoLine.SetPosition(1, PIL2);
        UiList.planetInfoLine.SetPosition(2, PIL3);

        // 惑星の名前をテキストに設定
        UiList.planetName.text = planetName;

        // 惑星の名前UIの位置を設定
        UiList.planetName.rectTransform.position = UiList.planetInfoRing.rectTransform.position + new Vector3(160, 80, 10);
    }
}
