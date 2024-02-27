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
    public TimeLimitUI timeLimitUI;
    public PauseUI pauseUI;
    public StageClearUI stageClearUI;
    public GameOverUI gameOverUI;
    public MainMenuUI mainMenuUI;
    public StageSelectUI stageSelectUI;
    public SkillSelectUI skillSelectUI;
    public SettingUI settingUI;
    public TitleUI titleUI;
    public OtherUI otherUI;

    // ゲーム画面のUI
    [System.Serializable]
    public class InGameUI
    {
        public GameObject allInGameUI;       // ゲーム画面のUI
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
        public Text planetName;              // 惑星の名前
    }

    // ミッションのUI
    [System.Serializable]
    public class MissionUI
    {
        public Text missionText;             // ミッションのテキスト
        public GameObject icon;              // ミッションのアイコン
    }

    // 制限時間のUI
    [System.Serializable]
    public class TimeLimitUI
    {
        public GameObject allTimeLimitUI;       // 全ての制限時間UI
        public Text value;                      // 制限時間の数値を表すテキスト
        public Image gauge;                     // 制限時間のゲージ
        public delegate void RenderTimeLimit(); // 制限時間UIを描画するデリゲートを定義
        public RenderTimeLimit renderTimeLimit; // 制限時間UIを描画するデリゲートを宣言
    }

    // ポーズ画面のUI
    [System.Serializable]
    public class PauseUI
    {
        public GameObject allPauseUI;             // ポーズ画面全体のUI
    }

    // ステージクリア画面のUI
    [System.Serializable]
    public class StageClearUI
    {
        public GameObject allStageClearUI;        // ステージクリア画面全体のUI
        public Text stageClearText;               // ステージクリア画面のテキスト
        public GameObject[] button;               // ステージクリア画面のボタン
        public delegate void RenderStageClear();  // ステージクリア画面を描画するデリゲートを定義
        public RenderStageClear renderStageClear; // ステージクリア画面を描画するデリゲートを宣言
    }

    // ゲームオーバー画面のUI
    [System.Serializable]
    public class GameOverUI
    {
        public GameObject allGameOverUI;     // ゲームオーバー画面全体のUI
        public Text GameOverText;            // ゲームオーバー画面のテキスト
        public GameObject[] button;          // ゲームオーバー画面のボタン
    }

    // メインメニューのUI
    [System.Serializable]
    public class MainMenuUI
    {
        public GameObject allMainMenuUI;     // メインメニュー全体のUI
        public Text titleText;               // メインメニューのタイトル
        public GameObject[] button;          // メインメニューのボタン
    }

    // ステージ選択画面のUI
    [System.Serializable]
    public class StageSelectUI
    {
        public GameObject allStageSelectUI;  // ステージ選択画面全体のUI
        public GameObject stageInfoUI;       // ステージ情報UI
        public GameObject buttons;           // ステージボタンをまとめたオブジェクト
        public Text name;                    // ステージ名
        public Text mission;                 // ミッション名
    }

    // スキル選択画面のUI
    [System.Serializable]
    public class SkillSelectUI
    {
        public GameObject allSkillSelectUI;  // スキル選択画面全体のUI
        public Text name;                    // スキルの名前を表示するテキスト
        public Image icon;                   // スキルのアイコンの画像
        public Text cost;                    // スキルのコストを表示するテキスト
        public Text effectTime;              // スキルの効果時間を表示するテキスト
        public Text coolDown;                // スキルのクールダウンを表示するテキスト
        public Text effectDetails;           // スキルの効果が書かれたテキスト
    }

    // 設定画面のUI
    [System.Serializable]
    public class SettingUI
    {
        public GameObject allSettingUI;      // 設定画面全体のUI
    }

    // タイトル画面のUI
    [System.Serializable]
    public class TitleUI
    {
        public GameObject allTitleUI;        // タイトル画面全体のUI
    }

    // その他UI
    [System.Serializable]
    public class OtherUI
    {
        public Image reticle;                // レティクル
        public Text speedValue;              // 移動速度のUI
        public Text justShotText;            // ジャストショット時のテキスト
    }

    [SerializeField] private SkillController skillController;             // InspectorでSkillControllerを指定

    // Findで探すGameObject
    private GameObject UIFunctionController;

    // Findで探したGameObjectのコンポーネント
    private EnergyUIController energyUIController;
    private ChargeUIController chargeUIController;
    private MissionUIController missionUIController;
    private SpeedUIController speedUIController;
    private StageClearUIController stageClearUIController;

    private bool drawedStageCrearUI = false; // ステージクリア画面を描画済みか

    void Start()
    {
        // GameObjectを探す
        UIFunctionController = GameObject.Find("UIFunctionController");

        // 探したGameObjectのコンポーネントを取得
        energyUIController = UIFunctionController.gameObject.GetComponent<EnergyUIController>();
        chargeUIController = UIFunctionController.gameObject.GetComponent<ChargeUIController>();
        missionUIController = UIFunctionController.gameObject.GetComponent<MissionUIController>();
        speedUIController = UIFunctionController.gameObject.GetComponent<SpeedUIController>();
        stageClearUIController = UIFunctionController.gameObject.GetComponent<StageClearUIController>();
    }

    void Update()
    {
        // ゲーム画面が表示されているなら各種UIを更新
        if (inGameUI.allInGameUI.activeSelf)
        {
            // エネルギーのUIを更新
            energyUIController.DrawEnergyUI(
                energyUI.EnergyGaugeOutline,
                energyUI.EnergyValue,
                messageUI.NoEnergy);

            // チャージのUIを更新
            chargeUIController.DrawChargeUI(
                chargeUI.allChargeUI,
                chargeUI.chargeValue,
                chargeUI.chargeCircle);

            // ミッションのUIを更新
            missionUIController.DrawMissionUI();

            // スキルのUIを更新
            skillController.CallSetSkillUI();

            // 移動速度の数値UIを更新
            speedUIController.DrawSpeedValue(otherUI.speedValue);

            // 制限時間のUIを描画
            timeLimitUI.renderTimeLimit();
        }

        // ステージクリア画面が表示されているなら処理を行う
        if ((stageClearUI.allStageClearUI.activeSelf) && (!drawedStageCrearUI))
        {
            drawedStageCrearUI = true;
            stageClearUIController.DrawStageClearUI();
        }
        else if ((!stageClearUI.allStageClearUI.activeSelf) && (drawedStageCrearUI))
        {
            drawedStageCrearUI = false;
        }
    }
}
