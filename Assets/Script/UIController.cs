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
    public PauseUI pauseUI;
    public OtherUI otherUI;

    // �|�[�Y��ʂ�UI
    [System.Serializable]
    public class PauseUI
    {
        public GameObject allPauseUI;        // �|�[�Y��ʑS�̂�UI
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
    }

    // ���̑�UI
    [System.Serializable]
    public class OtherUI
    {
        public Image reticle;                // ���e�B�N��
    }

    [System.NonSerialized] public int popupAmount = 0;                // �|�b�v�A�b�v�̐�
    [System.NonSerialized] public bool[] drawingPopup = new bool[10]; // �|�b�v�A�b�v���`�悳��Ă��邩

    [SerializeField] private Shot shot;                                   // Inspector��Shot���w��
    [SerializeField] private EnergyController energyController;           // Inspector��EnergyController���w��
    [SerializeField] private ScreenController screenController;           // Inspector��ScreenController���w��
    [SerializeField] private PostProcessController postProcessController; // Inspector��PostProcessController���w��
    [SerializeField] private StageData stageData;                         // Inspector��StageData���w��
    [SerializeField] private StageController stageController;             // Inspector��StageController���w��
    [SerializeField] private DestroyPlanet destroyPlanet;                 // Inspector��DestroyPlanet���w��
    [SerializeField] private GameObject popUp;                            // �|�b�v�A�b�v�̃v���n�u

    RectTransform PIR = null; // �f�����UI�̉~�̃X�N���[�����W
    Vector3 PIL1;             // �f�����UI�̐��̎n�_���W
    Vector3 PIL2;             // �f�����UI�̐��̒��ԍ��W
    Vector3 PIL3;             // �f�����UI�̐��̏I�_���W

    // �G�l���M�[��UI��`��
    void DrawEnergyUI()
    {
        // �G�l���M�[�Q�[�W�̑�����`��
        energyUI.EnergyGauge.fillAmount = energyController.energy / energyController.maxEnergy;

        if (energyUI.EnergyAfterImage.fillAmount > energyController.energy / energyController.maxEnergy)
        {
            // �G�l���M�[�Q�[�W�̌����ʂ����������炷
            energyUI.EnergyAfterImage.fillAmount -=
                (energyUI.EnergyAfterImage.fillAmount - energyController.energy / energyController.maxEnergy) * Time.deltaTime;
        }

        // �G�l���M�[�̐��l��\��
        energyUI.EnergyValue.text = energyController.energy.ToString("0");

        // �G�l���M�[��0�ȉ�����\���Ȃ�
        if ((energyController.energy <= 0) && (messageUI.NoEnergy.enabled == false))
        {
            // �G�l���M�[�Q�[�W�̘g�Ɛ��l��ԐF�ɂ���
            energyUI.EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);
            energyUI.EnergyValue.color = new Color32(155, 0, 0, 100);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g��\��
            messageUI.NoEnergy.enabled = true;
        }
        // �G�l���M�[��0���ォ�\������Ă���Ȃ�
        else if ((energyController.energy > 0) && (messageUI.NoEnergy.enabled == true))
        {
            // �G�l���M�[�Q�[�W�̘g�Ɛ��l�𔒐F�ɂ���
            energyUI.EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);
            energyUI.EnergyValue.color = new Color32(255, 255, 255, 255);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
            messageUI.NoEnergy.enabled = false;
        }
    }

    // �`���[�W��UI��`��
    void DrawChargeUI()
    {
        // �`���[�W����Ă���Ȃ�
        if (shot.charge > 0)
        {
            // �`���[�W��UI������������Ă�����
            if (!(chargeUI.allChargeUI.activeSelf))
            {
                // UI��L����
                chargeUI.allChargeUI.SetActive(true);
            }

            // �`���[�W�̐��l���e�L�X�g�ŕ\��
            chargeUI.chargeValue.text = shot.charge.ToString("0") + "%";

            // �`���[�W�̉~��`��
            chargeUI.chargeCircle.fillAmount = shot.charge / 100;
        }
        // �`���[�W����Ă��Ȃ����\������Ă���Ȃ�
        else if ((shot.charge == 0) && (chargeUI.allChargeUI.activeSelf))
        {
            // UI�𖳌���
            chargeUI.allChargeUI.SetActive(false);
        }
    }

    // �X�L����UI��`��
    public void DrawSkillUI(string skillName, float coolDown, float effectTime, float nowCoolDown, float nowEffectTime)
    {
        // �e�L�X�g�����݂̃X�L�����ɕύX
        skillUI.skillName.text = skillName;

        // ���ʎ��Ԃ�`��
        if (nowEffectTime > 0)
            skillUI.skillGauge.fillAmount = nowEffectTime / effectTime;
        // ���ʎ��Ԃ��o�߂��Ă����Ȃ�N�[���_�E����`��
        else if (nowCoolDown > 0)
            skillUI.skillGauge.fillAmount = (coolDown - nowCoolDown) / coolDown;
    }

    // �f�����UI��`��
    public void DrawPlanetInfoUI(Vector3 position, string planetName)
    {
        // �Q�[����ʂȂ�UI��\��
        if(screenController.screenNum == 0)
        {
            // UI����\���Ȃ�\��
            if(!(planetInfoUI.allPlanetInfo.activeSelf))
            {
                planetInfoUI.allPlanetInfo.SetActive(true);
            }
            // �f�����UI�̉~�̃X�N���[�����W��ύX
            planetInfoUI.targetRing.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, position);

            // �f�����UI�̐��̃X�N���[�����W�����[���h���W�ɕϊ�
            PIL1 = Camera.main.ScreenToWorldPoint(planetInfoUI.targetRing.rectTransform.position + new Vector3(0, 0, 10));
            PIL2 = Camera.main.ScreenToWorldPoint(planetInfoUI.targetRing.rectTransform.position + new Vector3(50, 50, 10));
            PIL3 = Camera.main.ScreenToWorldPoint(planetInfoUI.targetRing.rectTransform.position + new Vector3(150, 50, 10));

            // ����`��
            planetInfoUI.planetInfoLine.SetPosition(0, PIL1);
            planetInfoUI.planetInfoLine.SetPosition(1, PIL2);
            planetInfoUI.planetInfoLine.SetPosition(2, PIL3);

            // �f���̖��O���e�L�X�g�ɐݒ�
            planetInfoUI.planetName.text = planetName;

            // �f���̖��OUI�̈ʒu��ݒ�
            planetInfoUI.planetName.rectTransform.position = planetInfoUI.targetRing.rectTransform.position + new Vector3(160, 80, 10);
        }
        // �|�[�Y��ʂ���UI���\������Ă���Ȃ��\���ɂ���
        else if((screenController.screenNum == 1) && (planetInfoUI.allPlanetInfo.activeSelf))
        {
            planetInfoUI.allPlanetInfo.SetActive(false);
        }
    }

    // �|�[�Y��ʂ�UI��\�����͔�\���ɂ���
    public void DrawPauseUI(bool draw)
    {
        // �|�[�Y��ʂ�\�����͔�\��
        pauseUI.allPauseUI.SetActive(draw);

        // ��ʊE�[�x��ONOFF�؂�ւ�
        postProcessController.DepthOfFieldOnOff(draw);

        // ���e�B�N����\�����͔�\��
        otherUI.reticle.enabled = !(draw);
    }

    // �f�����j�󂳂ꂽ�|��`����|�b�v�A�b�v��`��
    public IEnumerator DrawDestroyPlanetPopup(string name)
    {
        float destroyTime = 10.0f; // �f����j�󂷂�܂ł̎���
        int i = 0;                 // ���𐔂���ϐ�

        // false��������܂ŌJ��Ԃ�
        while ((drawingPopup[i]))
        {
            // �z��͈̔͊O�Ȃ�R���[�`���I��
            if (i > drawingPopup.Length)
                yield break;
            i++;
        }

        // �`��ς݂ɂ���
        drawingPopup[i] = true;

        // �|�b�v�A�b�v�̃C���X�^���X�𐶐�
        GameObject popup = Instantiate(popUp);

        // �e��ݒ�
        popup.transform.SetParent(messageUI.Message.transform, false);

        // �ʒu��ݒ�
        popup.transform.position += new Vector3(0, i * -40, 0);

        // �v���n�u�̃e�L�X�g���擾
        Text popupText = popup.transform.GetChild(1).GetComponent<Text>();

        // �v���n�u�̃e�L�X�g��ݒ�
        popupText.text = name + " was destroyed";

        // �|�b�v�A�b�v���폜
        Destroy(popup.gameObject, destroyTime);

        // �|�b�v�A�b�v��������܂ő҂�
        yield return new WaitForSeconds(destroyTime);

        // �|�b�v�A�b�v�J�E���g�����炷
        popupAmount--;

        // �`�悵�Ă��Ȃ���Ԃɂ���
        drawingPopup[i] = false;
    }

    // �~�b�V������UI��`��
    public void DrawMissionUI()
    {
        // �~�b�V�����ԍ�����
        int missionNum = stageData.stageList[stageController.stageNum].missionNum;

        // �X�e�[�W�̏����f��������
        int planetAmount = stageData.stageList[stageController.stageNum].planetAmount;

        // �~�b�V�����ԍ��ɂ���ĕ���
        switch (missionNum)
        {
            case 0: // �S�Ă̘f����j��

                // �~�b�V�����̃e�L�X�g��ݒ�
                missionUI.missionText.text = "Destroy all planets " + destroyPlanet.planetDestroyAmount + " / " + planetAmount;
                break;
            case 1: // ���ԓ��ɃS�[���ɂ��ǂ蒅��

                // �~�b�V�����̃e�L�X�g��ݒ�
                missionUI.missionText.text = "Reach the goal";
                break;
            default:
                break;
        }
    }

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
        DrawPauseUI(false);

        // �|�b�v�A�b�v���`�悳��Ă��邩���Ǘ�����ϐ���������
        for (int i = 0; i > drawingPopup.Length; i++)
        {
            drawingPopup[i] = false;
        }
    }

    void Update()
    {
        // �G�l���M�[��UI��`��
        DrawEnergyUI();

        // �`���[�W��UI��`��
        DrawChargeUI();

        // �~�b�V������UI��`��
        DrawMissionUI();
    }
}
