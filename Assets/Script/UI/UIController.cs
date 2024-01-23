using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �Q�[������UI���Ǘ�
public class UIController : MonoBehaviour
{
    // Inspector��UI�̔z����w��
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
    public SkillSelectUI skillSelectUI;
    public SettingUI settingUI;
    public OtherUI otherUI;

    // �Q�[����ʂ�UI
    [System.Serializable]
    public class InGameUI
    {
        public GameObject allInGameUI;       // �Q�[����ʂ�UI
    }

    // �`���[�W��UI
    [System.Serializable]
    public class ChargeUI
    {
        public GameObject allChargeUI;       // �S�Ẵ`���[�W��UI
        public Image chargeCircle;           // �`���[�W�̉~
        public Text chargeValue;             // �`���[�W�̐��l
        public Text chargeName;              // �`���[�W�̕���
    }

    // �G�l���M�[��UI
    [System.Serializable]
    public class EnergyUI
    {
        public Image EnergyGauge;            // �G�l���M�[�Q�[�W
        public Image EnergyAfterImage;       // �G�l���M�[�Q�[�W�̌�����
        public Image EnergyGaugeOutline;     // �G�l���M�[�Q�[�W�̘g
        public Text EnergyValue;             // �G�l���M�[�̐��l
    }

    // ���b�Z�[�W��UI
    [System.Serializable]
    public class MessageUI
    {
        public GameObject Message;           // ���b�Z�[�W
        public Text NoEnergy;                // �G�l���M�[���Ȃ��|��`����e�L�X�g
    }

    // �X�L����UI
    [System.Serializable]
    public class SkillUI
    {
        public Text skillName;               // �X�L����
        public Image skillGauge;             // ���ʎ��ԂƃN�[���_�E���̃Q�[�W
    }

    // �f�����UI
    [System.Serializable]
    public class PlanetInfoUI
    {
        public GameObject allPlanetInfo;     // �S�Ă̘f�����UI
        public Image targetRing;             // �f�����UI�̉~
        public LineRenderer planetInfoLine;  // �f�����UI�̐�
        public Text planetName;              // �f���̖��O
    }

    // �~�b�V������UI
    [System.Serializable]
    public class MissionUI
    {
        public Text missionText;             // �~�b�V�����̃e�L�X�g
        public GameObject icon;              // �~�b�V�����̃A�C�R��
    }

    // �f�����X�gUI
    [System.Serializable]
    public class PlanetListUI
    {
        public GameObject allPlanetList;     // �S�Ă̘f�����X�gUI
    }

    // �|�[�Y��ʂ�UI
    [System.Serializable]
    public class PauseUI
    {
        public GameObject allPauseUI;        // �|�[�Y��ʑS�̂�UI
    }

    // �X�e�[�W�N���A��ʂ�UI
    [System.Serializable]
    public class StageClearUI
    {
        public GameObject allStageClearUI;   // �X�e�[�W�N���A��ʑS�̂�UI
        public Text stageClearText;          // �X�e�[�W�N���A��ʂ̃e�L�X�g
        public GameObject[] button;          // �X�e�[�W�N���A��ʂ̃{�^��
    }

    // ���C�����j���[��UI
    [System.Serializable]
    public class MainMenuUI
    {
        public GameObject allMainMenuUI;     // ���C�����j���[�S�̂�UI
        public Text titleText;               // ���C�����j���[�̃^�C�g��
        public GameObject[] button;          // ���C�����j���[�̃{�^��
        public Image backGround;             // ���C�����j���[�̔w�i�摜
    }

    // �X�e�[�W�I����ʂ�UI
    [System.Serializable]
    public class StageSelectUI
    {
        public GameObject allStageSelectUI;  // �X�e�[�W�I����ʑS�̂�UI
        public Text name;                    // �X�e�[�W��
        public Text mission;                 // �~�b�V������
    }

    // �X�L���I����ʂ�UI
    [System.Serializable]
    public class SkillSelectUI
    {
        public GameObject allSkillSelectUI; // �X�L���I����ʑS�̂�UI
    }

    // �ݒ��ʂ�UI
    [System.Serializable]
    public class SettingUI
    {
        public GameObject allSettingUI; // �ݒ��ʑS�̂�UI
    }

    // ���̑�UI
    [System.Serializable]
    public class OtherUI
    {
        public Image reticle;                // ���e�B�N��
        public Text speedValue;              // �ړ����x��UI
    }

    [SerializeField] private Shot shot;                                   // Inspector��Shot���w��
    [SerializeField] private EnergyController energyController;           // Inspector��EnergyController���w��
    [SerializeField] private ScreenController screenController;           // Inspector��ScreenController���w��
    [SerializeField] private PostProcessController postProcessController; // Inspector��PostProcessController���w��
    [SerializeField] private StageData stageData;                         // Inspector��StageData���w��
    [SerializeField] private StageController stageController;             // Inspector��StageController���w��
    [SerializeField] private PlanetAmount planetAmount;                   // Inspector��PlanetAmount���w��
    [SerializeField] private SkillController skillController;             // Inspector��SkillController���w��
    [SerializeField] private PlanetListController planetListController;   // Inspector��PlanetListController���w��
    [SerializeField] private Rigidbody rb;                                // �v���C���[��Rigidbody

    // Find�ŒT��GameObject
    private GameObject UIFunctionController;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    private EnergyUIController energyUIController;
    private ChargeUIController chargeUIController;
    private PauseUIController pauseUIController;
    private MissionUIController missionUIController;
    private SpeedUIController speedUIController;
    private StageClearUIController stageClearUIController;
    private MainMenuUIController mainMenuUIController;
    private StageSelectUIController stageSelectUIController;
    private PlanetListUIController planetListUIController;

    RectTransform PIR = null; // �f�����UI�̉~�̃X�N���[�����W

    private bool drawedStageClearUI = false; // �X�e�[�W�N���A��ʂ��`�悳�ꂽ��

    void Start()
    {
        // GameObject��T��
        UIFunctionController = GameObject.Find("UIFunctionController");

        // �T����GameObject�̃R���|�[�l���g���擾
        energyUIController = UIFunctionController.gameObject.GetComponent<EnergyUIController>();
        chargeUIController = UIFunctionController.gameObject.GetComponent<ChargeUIController>();
        pauseUIController = UIFunctionController.gameObject.GetComponent<PauseUIController>();
        missionUIController = UIFunctionController.gameObject.GetComponent<MissionUIController>();
        speedUIController = UIFunctionController.gameObject.GetComponent<SpeedUIController>();
        stageClearUIController = UIFunctionController.gameObject.GetComponent<StageClearUIController>();
        mainMenuUIController = UIFunctionController.gameObject.GetComponent<MainMenuUIController>();
        stageSelectUIController = UIFunctionController.gameObject.GetComponent<StageSelectUIController>();
        planetListUIController = UIFunctionController.gameObject.GetComponent<PlanetListUIController>();

        // �f�����UI�̉~��RectTransform���擾
        PIR = planetInfoUI.targetRing.GetComponent<RectTransform>();

        // �f�����UI�̐��̎n�_�ƏI�_�̑������w��
        planetInfoUI.planetInfoLine.startWidth = 0.01f;
        planetInfoUI.planetInfoLine.endWidth = 0.01f;

        // �f�����UI�̐��̐�
        planetInfoUI.planetInfoLine.positionCount = 3;

        // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
        messageUI.NoEnergy.enabled = false;

        // �|�[�Y��ʂ�UI���\��
        pauseUIController.DrawPauseUI(false);

        // �X�e�[�W�N���A��ʂ�UI���\��
        stageClearUIController.DrawStageClearUI(
            false,
            stageClearUI.allStageClearUI,
            stageClearUI.button,
            stageClearUI.stageClearText);
    }

    void Update()
    {
        // �Q�[����ʂ�\��/��\��
        if ((screenController.CanUIDrawing[5]) && (!inGameUI.allInGameUI.activeSelf))
            inGameUI.allInGameUI.SetActive(true);
        else if((!screenController.CanUIDrawing[5]) && (inGameUI.allInGameUI.activeSelf))
            inGameUI.allInGameUI.SetActive(false);

        // �Q�[����ʂ��\������Ă���Ȃ�e��UI���X�V
        if (inGameUI.allInGameUI.activeSelf)
        {
            // �G�l���M�[��UI���X�V
            energyUIController.DrawEnergyUI(
                energyUI.EnergyGaugeOutline,
                energyUI.EnergyValue,
                messageUI.NoEnergy);

            // �`���[�W��UI���X�V
            chargeUIController.DrawChargeUI(
                chargeUI.allChargeUI,
                chargeUI.chargeValue,
                chargeUI.chargeCircle);

            // �~�b�V������UI���X�V
            missionUIController.DrawMissionUI();

            // �X�L����UI���X�V
            skillController.CallSetSkillUI();

            // �ړ����x�̐��lUI���X�V
            speedUIController.DrawSpeedValue(otherUI.speedValue);
        }

        // �f�����X�gUI��\��/��\��
        planetListUI.allPlanetList.SetActive(planetListController.uiDrawing);

        // �X�e�[�W���N���A��������UI���`�悳��Ă��Ȃ��Ȃ�
        if ((stageController.stageCrear) && (!(drawedStageClearUI)))
        {
            // �X�e�[�W�N���A��ʂ�`��
            drawedStageClearUI = true;
            stageClearUIController.DrawStageClearUI(
                true,
                stageClearUI.allStageClearUI,
                stageClearUI.button,
                stageClearUI.stageClearText);
        }
        // �X�e�[�W�N���A�ς݂��X�e�[�W�N���A��ʂł͂Ȃ��Ȃ�
        else if((stageController.stageCrear) && (screenController.screenNum != 8))
        {
            // �X�e�[�W�N���A�t���O��������
            drawedStageClearUI = false;
            stageController.stageCrear = false;
            planetAmount.planetDestroyAmount = 0;

            // �X�e�[�W�N���A��ʂ��\��
            stageClearUIController.DrawStageClearUI(
                false,
                stageClearUI.allStageClearUI,
                stageClearUI.button,
                stageClearUI.stageClearText);
        }

        // �|�[�Y��ʂ�\��/��\��
        if((screenController.screenNum == 6) && (!pauseUI.allPauseUI.activeSelf))
            pauseUIController.DrawPauseUI(true);
        else if ((screenController.screenNum != 6) && (pauseUI.allPauseUI.activeSelf))
            pauseUIController.DrawPauseUI(false);

        // ���C�����j���[��\��/��\��
        if ((screenController.screenNum == 1) && (!mainMenuUI.allMainMenuUI.activeSelf))
            mainMenuUIController.DrawMainMenu(true, mainMenuUI.allMainMenuUI);
        else if(screenController.screenNum != 1)
            mainMenuUIController.DrawMainMenu(false, mainMenuUI.allMainMenuUI);

        // �X�e�[�W�I����ʂ�\��/��\��
        if((screenController.screenNum == 2) && (!stageSelectUI.allStageSelectUI.activeSelf))
            stageSelectUI.allStageSelectUI.SetActive(true);
        else if(screenController.screenNum != 2)
            stageSelectUI.allStageSelectUI.SetActive(false);

        // �X�e�[�W�I����ʂ��\������Ă���Ȃ�e��UI���X�V
        if (stageSelectUI.allStageSelectUI.activeSelf)
        {
            // �X�e�[�W���UI���X�V
            stageSelectUIController.DrawStageInfo(
                stageSelectUI.name,
                stageSelectUI.mission);
        }

        // �X�L���I����ʂ�\��/��\��
        if ((screenController.screenNum == 4) && (!skillSelectUI.allSkillSelectUI.activeSelf))
            skillSelectUI.allSkillSelectUI.SetActive(true);
        else if (screenController.screenNum != 4)
            skillSelectUI.allSkillSelectUI.SetActive(false);

        // �ݒ��ʂ�\��/��\��
        if ((screenController.screenNum == 3) && (!settingUI.allSettingUI.activeSelf))
            settingUI.allSettingUI.SetActive(true);
        else if (screenController.screenNum != 3)
            settingUI.allSettingUI.SetActive(false);

        // �X�e�[�W�̃A�C�R����\��/��\��
        stageSelectUIController.DrawStageIcon(stageSelectUI.allStageSelectUI.activeSelf);
    }
}
