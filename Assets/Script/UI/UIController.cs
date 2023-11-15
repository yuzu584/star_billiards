using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �Q�[������UI���Ǘ�
public class UIController : MonoBehaviour
{
    // Inspector��UI�̔z����w��
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
    }

    // �Q�[����ʂ�UI
    [System.Serializable]
    public class InGameUI
    {
        public GameObject allInGameUI;          // �Q�[����ʂ�UI
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
    [SerializeField] private DestroyPlanet destroyPlanet;                 // Inspector��DestroyPlanet���w��
    [SerializeField] private Rigidbody rb;                                // �v���C���[��Rigidbody

    // UI�`��֐�
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

    RectTransform PIR = null; // �f�����UI�̉~�̃X�N���[�����W

    public UIFunction uIFunction; // Inspector��UI�`��֐����w��

    private bool drawedStageClearUI = false; // �X�e�[�W�N���A��ʂ��`�悳�ꂽ��

    void Start()
    {
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
        uIFunction.pauseUIController.DrawPauseUI(false);

        // �X�e�[�W�N���A��ʂ�UI���\��
        uIFunction.stageClearUIController.DrawStageClearUI(
            false,
            stageClearUI.allStageClearUI,
            stageClearUI.button,
            stageClearUI.stageClearText);
    }

    void Update()
    {
        // �G�l���M�[��UI��`��
        uIFunction.energyUIController.DrawEnergyUI(
            energyUI.EnergyGauge,
            energyUI.EnergyAfterImage,
            energyUI.EnergyGaugeOutline,
            energyUI.EnergyValue,
            messageUI.NoEnergy);

        // �`���[�W��UI��`��
        uIFunction.chargeUIController.DrawChargeUI(
            chargeUI.allChargeUI,
            chargeUI.chargeValue,
            chargeUI.chargeCircle);

        // �~�b�V������UI��`��
        uIFunction.missionUIController.DrawMissionUI();

        // �ړ����x�̐��l��`��
        uIFunction.speedUIController.DrawSpeedValue(otherUI.speedValue);

        // �X�e�[�W���N���A��������UI���`�悳��Ă��Ȃ��Ȃ�
        if ((stageController.stageCrear) && (!(drawedStageClearUI)))
        {
            // �X�e�[�W�N���A��ʂ�`��
            drawedStageClearUI = true;
            uIFunction.stageClearUIController.DrawStageClearUI(
                true,
                stageClearUI.allStageClearUI,
                stageClearUI.button,
                stageClearUI.stageClearText);
        }
    }
}
