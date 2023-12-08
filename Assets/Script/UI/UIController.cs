using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ゲーム中のUIを管理
public class UIController : MonoBehaviour
{
    // InspectorでUIの配列を指定
    public InGameUI inGameUI;
    public ChargeUI chargeUI;
    public EnergyUI energyUI;
    public MessageUI messageUI;
    public SkillUI skillUI;
    public PlanetInfoUI planetInfoUI;
    public MissionUI missionUI;
    public PlanetListUI planetListUI;
    public PauseUI pauseUI;
    public StageClearUI stageClearUI;
    public MainMenuUI mainMenuUI;
    public StageSelectUI stageSelectUI;
    public OtherUI otherUI;

    // ゲーム画面のUI
    [System.Serializable]
    public class InGameUI
    {
        public GameObject allInGameUI;          // ゲーム画面のUI
    }

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
        public GameObject icon;              // ミッションのアイコン
    }

    // 惑星リストUI
    [System.Serializable]
    public class PlanetListUI
    {
        public GameObject allPlanetList;     // 全ての惑星リストUI
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

    // メインメニューのUI
    [System.Serializable]
    public class MainMenuUI
    {
        public GameObject allMainMenuUI;     // メインメニュー全体のUI
        public Text titleText;               // メインメニューのタイトル
        public GameObject[] button;          // メインメニューのボタン
        public Image backGround;             // メインメニューの背景画像
    }

    // ステージ選択画面のUI
    [System.Serializable]
    public class StageSelectUI
    {
        public GameObject allStageSelectUI;  // ステージ選択画面全体のUI
        public Text name;                    // ステージ名
        public Text mission;                 // ミッション名
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
    [SerializeField] private PlanetAmount planetAmount;                   // InspectorでPlanetAmountを指定
    [SerializeField] private SkillController skillController;             // InspectorでSkillControllerを指定
    [SerializeField] private PlanetListController planetListController;   // InspectorでPlanetListControllerを指定
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
        public MainMenuUIController mainMenuUIController;
        public StageSelectUIController StageSelectUIController;
        public PlanetListUIController planetListUIController;
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
        // ゲーム画面を表示/非表示
        if(((screenController.screenNum == 0) || (screenController.screenNum == 5)) && (!inGameUI.allInGameUI.activeSelf))
            inGameUI.allInGameUI.SetActive(true);
        else if(((screenController.screenNum != 0) && (screenController.screenNum != 5)) && (inGameUI.allInGameUI.activeSelf))
            inGameUI.allInGameUI.SetActive(false);

        // ゲーム画面が表示されているなら各種UIを更新
        if (inGameUI.allInGameUI.activeSelf)
        {
            // エネルギーのUIを更新
            uIFunction.energyUIController.DrawEnergyUI(
                energyUI.EnergyGaugeOutline,
                energyUI.EnergyValue,
                messageUI.NoEnergy);

            // チャージのUIを更新
            uIFunction.chargeUIController.DrawChargeUI(
                chargeUI.allChargeUI,
                chargeUI.chargeValue,
                chargeUI.chargeCircle);

            // ミッションのUIを更新
            uIFunction.missionUIController.DrawMissionUI();

            // スキルのUIを更新
            skillController.CallSetSkillUI();

            // 移動速度の数値を更新
            uIFunction.speedUIController.DrawSpeedValue(otherUI.speedValue);
        }

        // 惑星リストUIを表示/非表示
        planetListUI.allPlanetList.SetActive(planetListController.uiDrawing);

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
        // ステージクリア済みかつステージクリア画面ではないなら
        else if((stageController.stageCrear) && (screenController.screenNum != 2))
        {
            // ステージクリアフラグを初期化
            drawedStageClearUI = false;
            stageController.stageCrear = false;
            planetAmount.planetDestroyAmount = 0;

            // ステージクリア画面を非表示
            uIFunction.stageClearUIController.DrawStageClearUI(
                false,
                stageClearUI.allStageClearUI,
                stageClearUI.button,
                stageClearUI.stageClearText);
        }

        // ポーズ画面を表示/非表示
        if((screenController.screenNum == 1) && (!pauseUI.allPauseUI.activeSelf))
            uIFunction.pauseUIController.DrawPauseUI(true);
        else if ((screenController.screenNum != 1) && (pauseUI.allPauseUI.activeSelf))
            uIFunction.pauseUIController.DrawPauseUI(false);

        // メインメニューを表示/非表示
        if ((screenController.screenNum == 3) && (!mainMenuUI.allMainMenuUI.activeSelf))
            uIFunction.mainMenuUIController.DrawMainMenu(true, mainMenuUI.allMainMenuUI);
        else if(screenController.screenNum != 3)
            uIFunction.mainMenuUIController.DrawMainMenu(false, mainMenuUI.allMainMenuUI);

        // ステージ選択画面を表示/非表示
        if((screenController.screenNum == 4) && (!stageSelectUI.allStageSelectUI.activeSelf))
            stageSelectUI.allStageSelectUI.SetActive(true);
        else if(screenController.screenNum != 4)
            stageSelectUI.allStageSelectUI.SetActive(false);

        // ステージ選択画面が表示されているなら各種UIを更新
        if (stageSelectUI.allStageSelectUI.activeSelf)
        {
            // ステージ情報UIを更新
            uIFunction.StageSelectUIController.DrawStageInfo(
                stageSelectUI.name,
                stageSelectUI.mission);
        }

        // ステージのアイコンを表示/非表示
        uIFunction.StageSelectUIController.DrawStageIcon(stageSelectUI.allStageSelectUI.activeSelf);
    }
}
