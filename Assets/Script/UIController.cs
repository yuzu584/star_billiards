using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ゲーム中のUIを管理
public class UIController : MonoBehaviour
{
    // InspectorでUIの配列を指定
    [SerializeField] InGameUI inGameUI;
    [SerializeField] PauseUI pauseUI;

    // UIの配列
    [System.Serializable]
    public class InGameUI
    {
        public GameObject chargeUI;          // チャージのUI
        public Image chargeCircle;           // チャージの円
        public Text chargeValue;             // チャージの数値
        public Text chargeName;              // チャージの文字
        public Image EnergyGauge;            // エネルギーゲージ
        public Image EnergyAfterImage;       // エネルギーゲージの減少量
        public Image EnergyGaugeOutline;     // エネルギーゲージの枠
        public Text EnergyValue;             // エネルギーの数値
        public GameObject Message;           // メッセージ
        public Text NoEnergy;                // エネルギーがない旨を伝えるテキスト
        public Text skillName;               // スキル名
        public Image skillGauge;             // 効果時間とクールダウンのゲージ
        public GameObject planetInfo;        // 惑星情報UI
        public Image planetInfoRing;         // 惑星情報UIの円
        public LineRenderer planetInfoLine;  // 惑星情報UIの線
        public Text planetName;              // 惑星の名前
        public Image reticle;                // レティクル
    }

    [System.Serializable]
    public class PauseUI
    {
        public GameObject pauseUI; // ポーズ画面のUI
    }

    public int popupAmount = 0;                // ポップアップの数
    public bool[] drawingPopup = new bool[10]; // ポップアップが描画されているか

    [SerializeField] private Shot shot;                                   // Shot型の変数
    [SerializeField] private EnergyController energyController;           // EnergyController型の変数
    [SerializeField] private ScreenController screenController;           // ScreenController型の変数
    [SerializeField] private PostProcessController postProcessController; // PostProcessController型の変数
    [SerializeField] private GameObject popUp;                            // ポップアップのプレハブ

    RectTransform PIR = null; // 惑星情報UIの円のスクリーン座標
    Vector3 PIL1;             // 惑星情報UIの線の始点座標
    Vector3 PIL2;             // 惑星情報UIの線の中間座標
    Vector3 PIL3;             // 惑星情報UIの線の終点座標

    // エネルギーのUIを描画
    void DrawEnergyUI()
    {
        // エネルギーゲージの増減を描画
        inGameUI.EnergyGauge.fillAmount = energyController.energy / energyController.maxEnergy;

        if (inGameUI.EnergyAfterImage.fillAmount > energyController.energy / energyController.maxEnergy)
        {
            // エネルギーゲージの減少量を少しずつ減らす
            inGameUI.EnergyAfterImage.fillAmount -=
                (inGameUI.EnergyAfterImage.fillAmount - energyController.energy / energyController.maxEnergy) * Time.deltaTime;
        }

        // エネルギーの数値を表示
        inGameUI.EnergyValue.text = energyController.energy.ToString("0");

        // エネルギーが0以下かつ非表示なら
        if ((energyController.energy <= 0) && (inGameUI.NoEnergy.enabled == false))
        {
            // エネルギーゲージの枠と数値を赤色にする
            inGameUI.EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);
            inGameUI.EnergyValue.color = new Color32(155, 0, 0, 100);

            // エネルギーがない旨を伝えるテキストを表示
            inGameUI.NoEnergy.enabled = true;
        }
        // エネルギーが0より上かつ表示されているなら
        else if ((energyController.energy > 0) && (inGameUI.NoEnergy.enabled == true))
        {
            // エネルギーゲージの枠と数値を白色にする
            inGameUI.EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);
            inGameUI.EnergyValue.color = new Color32(255, 255, 255, 255);

            // エネルギーがない旨を伝えるテキストを非表示
            inGameUI.NoEnergy.enabled = false;
        }
    }

    // チャージのUIを描画
    void DrawChargeUI()
    {
        // チャージされているなら
        if (shot.charge > 0)
        {
            // チャージのUIが無効化されていたら
            if (!(inGameUI.chargeUI.activeSelf))
            {
                // UIを有効化
                inGameUI.chargeUI.SetActive(true);
            }

            // チャージの数値をテキストで表示
            inGameUI.chargeValue.text = shot.charge.ToString("0") + "%";

            // チャージの円を描写
            inGameUI.chargeCircle.fillAmount = shot.charge / 100;
        }
        // チャージされていないかつ表示されているなら
        else if ((shot.charge == 0) && (inGameUI.chargeUI.activeSelf))
        {
            // UIを無効化
            inGameUI.chargeUI.SetActive(false);
        }
    }

    // スキルのUIを描画
    public void DrawSkillUI(string skillName, float coolDown, float effectTime, float nowCoolDown, float nowEffectTime)
    {
        // テキストを現在のスキル名に変更
        inGameUI.skillName.text = skillName;

        // 効果時間を描画
        if (nowEffectTime > 0)
            inGameUI.skillGauge.fillAmount = nowEffectTime / effectTime;
        // 効果時間が経過していたならクールダウンを描画
        else if (nowCoolDown > 0)
            inGameUI.skillGauge.fillAmount = (coolDown - nowCoolDown) / coolDown;
    }

    // 惑星情報UIを描画
    public void DrawPlanetInfoUI(Vector3 position, string planetName)
    {
        // ゲーム画面ならUIを表示
        if(screenController.screenNum == 0)
        {
            // UIが非表示なら表示
            if(!(inGameUI.planetInfo.activeSelf))
            {
                inGameUI.planetInfo.SetActive(true);
            }
            // 惑星情報UIの円のスクリーン座標を変更
            inGameUI.planetInfoRing.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, position);

            // 惑星情報UIの線のスクリーン座標をワールド座標に変換
            PIL1 = Camera.main.ScreenToWorldPoint(inGameUI.planetInfoRing.rectTransform.position + new Vector3(0, 0, 10));
            PIL2 = Camera.main.ScreenToWorldPoint(inGameUI.planetInfoRing.rectTransform.position + new Vector3(50, 50, 10));
            PIL3 = Camera.main.ScreenToWorldPoint(inGameUI.planetInfoRing.rectTransform.position + new Vector3(150, 50, 10));

            // 線を描画
            inGameUI.planetInfoLine.SetPosition(0, PIL1);
            inGameUI.planetInfoLine.SetPosition(1, PIL2);
            inGameUI.planetInfoLine.SetPosition(2, PIL3);

            // 惑星の名前をテキストに設定
            inGameUI.planetName.text = planetName;

            // 惑星の名前UIの位置を設定
            inGameUI.planetName.rectTransform.position = inGameUI.planetInfoRing.rectTransform.position + new Vector3(160, 80, 10);
        }
        // ポーズ画面かつUIが表示されているなら非表示にする
        else if((screenController.screenNum == 1) && (inGameUI.planetInfo.activeSelf))
        {
            inGameUI.planetInfo.SetActive(false);
        }
    }

    // ポーズ画面のUIを表示又は非表示にする
    public void DrawPauseUI(bool draw)
    {
        // ポーズ画面を表示又は非表示
        pauseUI.pauseUI.SetActive(draw);

        // 被写界深度のONOFF切り替え
        postProcessController.DepthOfFieldOnOff(draw);

        // レティクルを表示又は非表示
        inGameUI.reticle.enabled = !(draw);
    }

    // 惑星が破壊された旨を伝えるポップアップを描画
    public IEnumerator DrawDestroyPlanetPopup(string name)
    {
        float destroyTime = 10.0f; // 惑星を破壊するまでの時間
        int i = 0;                 // 数を数える変数

        // falseが見つかるまで繰り返す
        while ((drawingPopup[i]))
        {
            // 配列の範囲外ならコルーチン終了
            if (i > drawingPopup.Length)
                yield break;
            i++;
        }

        // 描画済みにする
        drawingPopup[i] = true;

        // ポップアップのインスタンスを生成
        GameObject popup = Instantiate(popUp);

        // 親を設定
        popup.transform.SetParent(inGameUI.Message.transform, false);

        // 位置を設定
        popup.transform.position += new Vector3(0, i * -40, 0);

        // プレハブのテキストを取得
        Text popupText = popup.transform.GetChild(1).GetComponent<Text>();

        // プレハブのテキストを設定
        popupText.text = name + " was destroyed";

        // ポップアップを削除
        Destroy(popup.gameObject, destroyTime);

        // ポップアップが消えるまで待つ
        yield return new WaitForSeconds(destroyTime);

        // ポップアップカウントを減らす
        popupAmount--;

        // 描画していない状態にする
        drawingPopup[i] = false;
    }

    void Start()
    {
        // 惑星情報UIの円のRectTransformを取得
        PIR = inGameUI.planetInfoRing.GetComponent<RectTransform>();

        // 惑星情報UIの線の始点と終点の太さを指定
        inGameUI.planetInfoLine.startWidth = 0.01f;
        inGameUI.planetInfoLine.endWidth = 0.01f;

        // 惑星情報UIの線の数
        inGameUI.planetInfoLine.positionCount = 3;

        // エネルギーがない旨を伝えるテキストを非表示
        inGameUI.NoEnergy.enabled = false;

        // ポーズ画面のUIを非表示
        DrawPauseUI(false);

        // ポップアップが描画されているかを管理する変数を初期化
        for (int i = 0; i > drawingPopup.Length; i++)
        {
            drawingPopup[i] = false;
        }
    }

    void Update()
    {
        // エネルギーのUIを描画
        DrawEnergyUI();

        // チャージのUIを描画
        DrawChargeUI();
    }
}
