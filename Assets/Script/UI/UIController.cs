using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ゲーム中のUIを管理
public class UIController : MonoBehaviour
{
    // InspectorでUIの配列を指定
    public ChargeUI chargeUI;
    public EnergyUI energyUI;
    public MessageUI messageUI;
    public SkillUI skillUI;
    public PlanetInfoUI planetInfoUI;
    public MissionUI missionUI;
    public InGameUI inGameUI;
    public PauseUI pauseUI;
    public StageClearUI stageClearUI;
    public OtherUI otherUI;

    // チャージのUI
    [System.Serializable]
    public class ChargeUI
    {
        public GameObject allChargeUI;       // 全てのチャージのUI
        public Image chargeCircle;           // チャージの円
        public Text chargeValue;             // チャージの数値
        public Text chargeName;              // チャージの文字
    }

    // エネルギーのUI
    [System.Serializable]
    public class EnergyUI
    {
        public Image EnergyGauge;            // エネルギーゲージ
        public Image EnergyAfterImage;       // エネルギーゲージの減少量
        public Image EnergyGaugeOutline;     // エネルギーゲージの枠
        public Text EnergyValue;             // エネルギーの数値
    }

    // メッセージのUI
    [System.Serializable]
    public class MessageUI
    {
        public GameObject Message;           // メッセージ
        public Text NoEnergy;                // エネルギーがない旨を伝えるテキスト
    }

    // スキルのUI
    [System.Serializable]
    public class SkillUI
    {
        public Text skillName;               // スキル名
        public Image skillGauge;             // 効果時間とクールダウンのゲージ
    }

    // 惑星情報UI
    [System.Serializable]
    public class PlanetInfoUI
    {
        public GameObject allPlanetInfo;     // 全ての惑星情報UI
        public Image targetRing;             // 惑星情報UIの円
        public LineRenderer planetInfoLine;  // 惑星情報UIの線
        public Text planetName;              // 惑星の名前
    }

    // ミッションのUI
    [System.Serializable]
    public class MissionUI
    {
        public Text missionText;             // ミッションのテキスト
    }

    // ゲーム画面のUI
    [System.Serializable]
    public class InGameUI
    {
        public GameObject allInGameUI;          // ゲーム画面のUI
    }

    // ポーズ画面のUI
    [System.Serializable]
    public class PauseUI
    {
        public GameObject allPauseUI;        // ポーズ画面全体のUI
    }

    // ステージクリア画面のUI
    [System.Serializable]
    public class StageClearUI
    {
        public GameObject allStageClearUI;   // ステージクリア画面全体のUI
        public Text stageClearText;          // ステージクリア画面のテキスト
        public GameObject[] button;          // ステージクリア画面のボタン
    }

    // その他UI
    [System.Serializable]
    public class OtherUI
    {
        public Image reticle;                // レティクル
        public Text speedValue;              // 移動速度のUI
    }

    [SerializeField] private Shot shot;                                   // InspectorでShotを指定
    [SerializeField] private EnergyController energyController;           // InspectorでEnergyControllerを指定
    [SerializeField] private ScreenController screenController;           // InspectorでScreenControllerを指定
    [SerializeField] private PostProcessController postProcessController; // InspectorでPostProcessControllerを指定
    [SerializeField] private StageData stageData;                         // InspectorでStageDataを指定
    [SerializeField] private StageController stageController;             // InspectorでStageControllerを指定
    [SerializeField] private DestroyPlanet destroyPlanet;                 // InspectorでDestroyPlanetを指定
    [SerializeField] private Rigidbody rb;                                // プレイヤーのRigidbody

    // UI描画関数
    [System.Serializable]
    public class UIFunction
    {
        public EnergyUIController energyUIController;
        public ChargeUIController chargeUIController;
        public PauseUIController pauseUIController;
        public MissionUIController missionUIController;
        public SpeedUIController speedUIController;
        public StageClearUIController stageClearUIController;
    }

    RectTransform PIR = null; // 惑星情報UIの円のスクリーン座標

    public UIFunction uIFunction; // InspectorでUI描画関数を指定

    private bool drawedStageClearUI = false; // ステージクリア画面が描画されたか

    void Start()
    {
        // 惑星情報UIの円のRectTransformを取得
        PIR = planetInfoUI.targetRing.GetComponent<RectTransform>();

        // 惑星情報UIの線の始点と終点の太さを指定
        planetInfoUI.planetInfoLine.startWidth = 0.01f;
        planetInfoUI.planetInfoLine.endWidth = 0.01f;

        // 惑星情報UIの線の数
        planetInfoUI.planetInfoLine.positionCount = 3;

        // エネルギーがない旨を伝えるテキストを非表示
        messageUI.NoEnergy.enabled = false;

        // ポーズ画面のUIを非表示
        uIFunction.pauseUIController.DrawPauseUI(false);

        // ステージクリア画面のUIを非表示
        uIFunction.stageClearUIController.DrawStageClearUI(
            false,
            stageClearUI.allStageClearUI,
            stageClearUI.button,
            stageClearUI.stageClearText);
    }

    void Update()
    {
        // エネルギーのUIを描画
        uIFunction.energyUIController.DrawEnergyUI(
            energyUI.EnergyGauge,
            energyUI.EnergyAfterImage,
            energyUI.EnergyGaugeOutline,
            energyUI.EnergyValue,
            messageUI.NoEnergy);

        // チャージのUIを描画
        uIFunction.chargeUIController.DrawChargeUI(
            chargeUI.allChargeUI,
            chargeUI.chargeValue,
            chargeUI.chargeCircle);

        // ミッションのUIを描画
        uIFunction.missionUIController.DrawMissionUI();

        // 移動速度の数値を描画
        uIFunction.speedUIController.DrawSpeedValue(otherUI.speedValue);

        // ステージをクリアしたかつUIが描画されていないなら
        if ((stageController.stageCrear) && (!(drawedStageClearUI)))
        {
            // ステージクリア画面を描画
            drawedStageClearUI = true;
            uIFunction.stageClearUIController.DrawStageClearUI(
                true,
                stageClearUI.allStageClearUI,
                stageClearUI.button,
                stageClearUI.stageClearText);
        }
    }
}
