using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �Q�[������UI���Ǘ�
[DefaultExecutionOrder(-100)]
public class UIController : Singleton<UIController>
{
    // Inspector��UI�̔z����w��
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
        public Text planetName;              // �f���̖��O
    }

    // �~�b�V������UI
    [System.Serializable]
    public class MissionUI
    {
        public Text missionText;             // �~�b�V�����̃e�L�X�g
        public GameObject icon;              // �~�b�V�����̃A�C�R��
    }

    // �������Ԃ�UI
    [System.Serializable]
    public class TimeLimitUI
    {
        public GameObject allTimeLimitUI;       // �S�Ă̐�������UI
        public Text value;                      // �������Ԃ̐��l��\���e�L�X�g
        public Image gauge;                     // �������Ԃ̃Q�[�W
        public delegate void RenderTimeLimit(); // ��������UI��`�悷��f���Q�[�g���`
        public RenderTimeLimit renderTimeLimit; // ��������UI��`�悷��f���Q�[�g��錾
    }

    // �|�[�Y��ʂ�UI
    [System.Serializable]
    public class PauseUI
    {
        public GameObject allPauseUI;             // �|�[�Y��ʑS�̂�UI
    }

    // �X�e�[�W�N���A��ʂ�UI
    [System.Serializable]
    public class StageClearUI
    {
        public GameObject allStageClearUI;        // �X�e�[�W�N���A��ʑS�̂�UI
        public Text stageClearText;               // �X�e�[�W�N���A��ʂ̃e�L�X�g
        public GameObject[] button;               // �X�e�[�W�N���A��ʂ̃{�^��
        public delegate void RenderStageClear();  // �X�e�[�W�N���A��ʂ�`�悷��f���Q�[�g���`
        public RenderStageClear renderStageClear; // �X�e�[�W�N���A��ʂ�`�悷��f���Q�[�g��錾
    }

    // �Q�[���I�[�o�[��ʂ�UI
    [System.Serializable]
    public class GameOverUI
    {
        public GameObject allGameOverUI;     // �Q�[���I�[�o�[��ʑS�̂�UI
        public Text GameOverText;            // �Q�[���I�[�o�[��ʂ̃e�L�X�g
        public GameObject[] button;          // �Q�[���I�[�o�[��ʂ̃{�^��
    }

    // ���C�����j���[��UI
    [System.Serializable]
    public class MainMenuUI
    {
        public GameObject allMainMenuUI;     // ���C�����j���[�S�̂�UI
        public Text titleText;               // ���C�����j���[�̃^�C�g��
        public GameObject[] button;          // ���C�����j���[�̃{�^��
    }

    // �X�e�[�W�I����ʂ�UI
    [System.Serializable]
    public class StageSelectUI
    {
        public GameObject allStageSelectUI;  // �X�e�[�W�I����ʑS�̂�UI
        public GameObject stageInfoUI;       // �X�e�[�W���UI
        public GameObject buttons;           // �X�e�[�W�{�^�����܂Ƃ߂��I�u�W�F�N�g
        public Text name;                    // �X�e�[�W��
        public Text mission;                 // �~�b�V������
    }

    // �X�L���I����ʂ�UI
    [System.Serializable]
    public class SkillSelectUI
    {
        public GameObject allSkillSelectUI;  // �X�L���I����ʑS�̂�UI
        public Text name;                    // �X�L���̖��O��\������e�L�X�g
        public Image icon;                   // �X�L���̃A�C�R���̉摜
        public Text cost;                    // �X�L���̃R�X�g��\������e�L�X�g
        public Text effectTime;              // �X�L���̌��ʎ��Ԃ�\������e�L�X�g
        public Text coolDown;                // �X�L���̃N�[���_�E����\������e�L�X�g
        public Text effectDetails;           // �X�L���̌��ʂ������ꂽ�e�L�X�g
    }

    // �ݒ��ʂ�UI
    [System.Serializable]
    public class SettingUI
    {
        public GameObject allSettingUI;      // �ݒ��ʑS�̂�UI
    }

    // �^�C�g����ʂ�UI
    [System.Serializable]
    public class TitleUI
    {
        public GameObject allTitleUI;        // �^�C�g����ʑS�̂�UI
    }

    // ���̑�UI
    [System.Serializable]
    public class OtherUI
    {
        public Image reticle;                   // ���e�B�N��
        public Text speedValue;                 // �ړ����x��UI
        public Text justShotText;               // �W���X�g�V���b�g���̃e�L�X�g
    }

    private SkillController skillCon;
    private EnergyUIController eneUICon;
    private ChargeUIController chargeUICon;
    private MissionUIController missionUICon;
    private SpeedUIController speedUICon;

    void Start()
    {
        skillCon = SkillController.instance;
        eneUICon = EnergyUIController.instance;
        chargeUICon = ChargeUIController.instance;
        missionUICon = MissionUIController.instance;
        speedUICon = SpeedUIController.instance;
    }

    void Update()
    {
        // �Q�[����ʂ��\������Ă���Ȃ�e��UI���X�V
        if (inGameUI.allInGameUI.activeSelf)
        {
            // �G�l���M�[��UI���X�V
            eneUICon.DrawEnergyUI(
                energyUI.EnergyGaugeOutline,
                energyUI.EnergyValue,
                messageUI.NoEnergy);

            // �`���[�W��UI���X�V
            chargeUICon.DrawChargeUI(
                chargeUI.allChargeUI,
                chargeUI.chargeValue,
                chargeUI.chargeCircle);

            // �~�b�V������UI���X�V
            missionUICon.DrawMissionUI();

            // �X�L����UI���X�V
            skillCon.CallSetSkillUI();

            // �ړ����x�̐��lUI���X�V
            speedUICon.DrawSpeedValue(otherUI.speedValue);

            // �������Ԃ�UI��`��
            timeLimitUI.renderTimeLimit();
        }
    }
}
