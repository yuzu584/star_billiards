using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �Q�[������UI���Ǘ�
public class UIController : MonoBehaviour
{
    // Inspector��UI�̔z����w��
    [SerializeField] InGameUI inGameUI;
    [SerializeField] PauseUI pauseUI;

    // UI�̔z��
    [System.Serializable]
    public class InGameUI
    {
        public GameObject chargeUI;         // �`���[�W��UI
        public Image chargeCircle;          // �`���[�W�̉~
        public Text chargeValue;            // �`���[�W�̐��l
        public Text chargeName;             // �`���[�W�̕���
        public Image EnergyGauge;           // �G�l���M�[�Q�[�W
        public Image EnergyAfterImage;      // �G�l���M�[�Q�[�W�̌�����
        public Image EnergyGaugeOutline;    // �G�l���M�[�Q�[�W�̘g
        public Text EnergyValue;            // �G�l���M�[�̐��l
        public Text NoEnergy;               // �G�l���M�[���Ȃ��|��`����e�L�X�g
        public Text skillName;              // �X�L����
        public Image skillGauge;            // ���ʎ��ԂƃN�[���_�E���̃Q�[�W
        public Image planetInfoRing;        // �f�����UI�̉~
        public LineRenderer planetInfoLine; // �f�����UI�̐�
        public Text planetName;             // �f���̖��O
    }

    [System.Serializable]
    public class PauseUI
    {
        public GameObject pauseUI; // �|�[�Y��ʂ�UI
    }

    [SerializeField] Shot shot;                         // Shot�^�̕ϐ�
    [SerializeField] EnergyController energyController; // EnergyController�^�̕ϐ�
    [SerializeField] ScreenController screenController; // ScreenController�^�̕ϐ�

    RectTransform PIR = null; // �f�����UI�̉~�̃X�N���[�����W
    Vector3 PIL1;             // �f�����UI�̐��̎n�_���W
    Vector3 PIL2;             // �f�����UI�̐��̒��ԍ��W
    Vector3 PIL3;             // �f�����UI�̐��̏I�_���W

    void Start()
    {
        // �f�����UI�̉~��RectTransform���擾
        PIR = inGameUI.planetInfoRing.GetComponent<RectTransform>();

        // �f�����UI�̐��̎n�_�ƏI�_�̑������w��
        inGameUI.planetInfoLine.startWidth = 0.01f;
        inGameUI.planetInfoLine.endWidth = 0.01f;

        // �f�����UI�̐��̐�
        inGameUI.planetInfoLine.positionCount = 3;

        // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
        inGameUI.NoEnergy.enabled = false;
    }

    void Update()
    {
        // �G�l���M�[��UI��`��
        DrawEnergyUI();

        // �`���[�W��UI��`��
        DrawChargeUI();

        if(screenController.screenNum == 1)
        {
            // �|�[�Y��ʂ�UI��`��
            DrawPauseUI(true);
        }
        else
        {
            // �|�[�Y��ʂ�UI���\��
            DrawPauseUI(false);
        }
    }

    // �G�l���M�[��UI��`��
    void DrawEnergyUI()
    {
        // �G�l���M�[�Q�[�W�̑�����`��
        inGameUI.EnergyGauge.fillAmount = energyController.energy / energyController.maxEnergy;

        if (inGameUI.EnergyAfterImage.fillAmount > energyController.energy / energyController.maxEnergy)
        {
            // �G�l���M�[�Q�[�W�̌����ʂ����������炷
            inGameUI.EnergyAfterImage.fillAmount -=
                (inGameUI.EnergyAfterImage.fillAmount - energyController.energy / energyController.maxEnergy) * Time.deltaTime;
        }

        // �G�l���M�[�̐��l��\��
        inGameUI.EnergyValue.text = energyController.energy.ToString("0");

        // �G�l���M�[��0�ȉ�����\���Ȃ�
        if ((energyController.energy <= 0) && (inGameUI.NoEnergy.enabled == false))
        {
            // �G�l���M�[�Q�[�W�̘g�Ɛ��l��ԐF�ɂ���
            inGameUI.EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);
            inGameUI.EnergyValue.color = new Color32(155, 0, 0, 100);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g��\��
            inGameUI.NoEnergy.enabled = true;
        }
        // �G�l���M�[��0���ォ�\������Ă���Ȃ�
        else if ((energyController.energy > 0) && (inGameUI.NoEnergy.enabled == true))
        {
            // �G�l���M�[�Q�[�W�̘g�Ɛ��l�𔒐F�ɂ���
            inGameUI.EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);
            inGameUI.EnergyValue.color = new Color32(255, 255, 255, 255);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
            inGameUI.NoEnergy.enabled = false;
        }
    }

    // �`���[�W��UI��`��
    void DrawChargeUI()
    {
        // �`���[�W����Ă���Ȃ�
        if (shot.charge > 0)
        {
            // �`���[�W��UI������������Ă�����
            if (!(inGameUI.chargeUI.activeSelf))
            {
                // UI��L����
                inGameUI.chargeUI.SetActive(true);
            }

            // �`���[�W�̐��l���e�L�X�g�ŕ\��
            inGameUI.chargeValue.text = shot.charge.ToString("0") + "%";

            // �`���[�W�̉~��`��
            inGameUI.chargeCircle.fillAmount = shot.charge / 100;
        }
        // �`���[�W����Ă��Ȃ����\������Ă���Ȃ�
        else if ((shot.charge == 0) && (inGameUI.chargeUI.activeSelf))
        {
            // UI�𖳌���
            inGameUI.chargeUI.SetActive(false);
        }
    }

    // �X�L����UI��`��
    public void DrawSkillUI(string skillName, float coolDown, float effectTime, float nowCoolDown, float nowEffectTime)
    {
        // �e�L�X�g�����݂̃X�L�����ɕύX
        inGameUI.skillName.text = skillName;

        // ���ʎ��Ԃ�`��
        if (nowEffectTime > 0)
            inGameUI.skillGauge.fillAmount = nowEffectTime / effectTime;
        // ���ʎ��Ԃ��o�߂��Ă����Ȃ�N�[���_�E����`��
        else if (nowCoolDown > 0)
            inGameUI.skillGauge.fillAmount = (coolDown - nowCoolDown) / coolDown;
    }

    // �f�����UI��`��
    public void DrawPlanetInfoUI(Vector3 position, string planetName)
    {
        // �f�����UI�̉~�̃X�N���[�����W��ύX
        inGameUI.planetInfoRing.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, position);

        // �f�����UI�̐��̃X�N���[�����W�����[���h���W�ɕϊ�
        PIL1 = Camera.main.ScreenToWorldPoint(inGameUI.planetInfoRing.rectTransform.position + new Vector3(0, 0, 10));
        PIL2 = Camera.main.ScreenToWorldPoint(inGameUI.planetInfoRing.rectTransform.position + new Vector3(50, 50, 10));
        PIL3 = Camera.main.ScreenToWorldPoint(inGameUI.planetInfoRing.rectTransform.position + new Vector3(150, 50, 10));

        // ����`��
        inGameUI.planetInfoLine.SetPosition(0, PIL1);
        inGameUI.planetInfoLine.SetPosition(1, PIL2);
        inGameUI.planetInfoLine.SetPosition(2, PIL3);

        // �f���̖��O���e�L�X�g�ɐݒ�
        inGameUI.planetName.text = planetName;

        // �f���̖��OUI�̈ʒu��ݒ�
        inGameUI.planetName.rectTransform.position = inGameUI.planetInfoRing.rectTransform.position + new Vector3(160, 80, 10);
    }

    // �|�[�Y��ʂ�UI��`��
    void DrawPauseUI(bool draw)
    {
        // �\�����͔�\��
        pauseUI.pauseUI.SetActive(draw);
    }
}
