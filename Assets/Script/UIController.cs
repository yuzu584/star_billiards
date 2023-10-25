using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �Q�[������UI���Ǘ�
public class UIController : MonoBehaviour
{
    // Inspector��UI�̔z����w��
    [SerializeField] UIList UiList;

    // UI�̔z��
    [System.Serializable]
    public class UIList
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
    }

    RectTransform PIR = null; // �f�����UI�̉~�̃X�N���[�����W
    Vector3 PIL1;             // �f�����UI�̐��̎n�_���W
    Vector3 PIL2;             // �f�����UI�̐��̒��ԍ��W
    Vector3 PIL3;             // �f�����UI�̐��̏I�_���W

    void Start()
    {
        // �f�����UI�̉~��RectTransform���擾
        PIR = UiList.planetInfoRing.GetComponent<RectTransform>();

        // �f�����UI�̐��̊J�n�_�̑������w��
        UiList.planetInfoLine.startWidth = 0.01f;

        // �f�����UI�̐��̏I���_�̑������w��
        UiList.planetInfoLine.endWidth = 0.01f;

        // �f�����UI�̐��̐�
        UiList.planetInfoLine.positionCount = 3;

        // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
        UiList.NoEnergy.enabled = false;
    }

    void Update()
    {
        // �G�l���M�[��UI��`��
        DrawEnergyUI();

        // �`���[�W��UI��`��
        DrawChargeUI();
    }

    // �G�l���M�[��UI��`��
    void DrawEnergyUI()
    {
        // �G�l���M�[�Q�[�W�̑�����`��
        UiList.EnergyGauge.fillAmount = EnergyController.energy / EnergyController.maxEnergy;

        if (UiList.EnergyAfterImage.fillAmount > EnergyController.energy / EnergyController.maxEnergy)
        {
            // �G�l���M�[�Q�[�W�̌����ʂ����������炷
            UiList.EnergyAfterImage.fillAmount -=
                (UiList.EnergyAfterImage.fillAmount - EnergyController.energy / EnergyController.maxEnergy) * Time.deltaTime;
        }

        // �G�l���M�[�̐��l��\��
        UiList.EnergyValue.text = EnergyController.energy.ToString("0");

        // �G�l���M�[��0�ȉ�����\���Ȃ�
        if ((EnergyController.energy <= 0) && (UiList.NoEnergy.enabled == false))
        {
            // �G�l���M�[�Q�[�W�̘g�Ɛ��l��ԐF�ɂ���
            UiList.EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);
            UiList.EnergyValue.color = new Color32(155, 0, 0, 100);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g��\��
            UiList.NoEnergy.enabled = true;
        }
        // �G�l���M�[��0���ォ�\������Ă���Ȃ�
        else if ((EnergyController.energy > 0) && (UiList.NoEnergy.enabled == true))
        {
            // �G�l���M�[�Q�[�W�̘g�Ɛ��l�𔒐F�ɂ���
            UiList.EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);
            UiList.EnergyValue.color = new Color32(255, 255, 255, 255);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
            UiList.NoEnergy.enabled = false;
        }
    }

    // �`���[�W��UI��`��
    void DrawChargeUI()
    {
        // �`���[�W����Ă���Ȃ�
        if (Shot.charge > 0)
        {
            // �`���[�W��UI������������Ă�����
            if (!(UiList.chargeUI.activeSelf))
            {
                // UI��L����
                UiList.chargeUI.SetActive(true);
            }

            // �`���[�W�̐��l���e�L�X�g�ŕ\��
            UiList.chargeValue.text = Shot.charge.ToString("0") + "%";

            // �`���[�W�̉~��`��
            UiList.chargeCircle.fillAmount = Shot.charge / 100;
        }
        // �`���[�W����Ă��Ȃ����\������Ă���Ȃ�
        else if ((Shot.charge == 0) && (UiList.chargeUI.activeSelf))
        {
            // UI�𖳌���
            UiList.chargeUI.SetActive(false);
        }
    }

    // �X�L����UI��`��
    public void DrawSkillUI(string skillName, float coolDown, float effectTime, float nowCoolDown, float nowEffectTime)
    {
        // �e�L�X�g�����݂̃X�L�����ɕύX
        UiList.skillName.text = skillName;

        // ���ʎ��Ԃ�`��
        if (nowEffectTime > 0)
            UiList.skillGauge.fillAmount = nowEffectTime / effectTime;
        // ���ʎ��Ԃ��o�߂��Ă����Ȃ�N�[���_�E����`��
        else if (nowCoolDown > 0)
            UiList.skillGauge.fillAmount = (coolDown - nowCoolDown) / coolDown;
    }

    // �f�����UI��`��
    public void DrawPlanetInfoUI(Vector3 position)
    {
        // �f�����UI�̉~�̃X�N���[�����W��ύX
        UiList.planetInfoRing.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, position);

        // �f�����UI�̐��̃X�N���[�����W�����[���h���W�ɕϊ�
        PIL1 = Camera.main.ScreenToWorldPoint(UiList.planetInfoRing.rectTransform.position + new Vector3(0, 0, 10));
        PIL2 = Camera.main.ScreenToWorldPoint(UiList.planetInfoRing.rectTransform.position + new Vector3(50, 50, 10));
        PIL3 = Camera.main.ScreenToWorldPoint(UiList.planetInfoRing.rectTransform.position + new Vector3(150, 50, 10));

        // ����`��
        UiList.planetInfoLine.SetPosition(0, PIL1);
        UiList.planetInfoLine.SetPosition(1, PIL2);
        UiList.planetInfoLine.SetPosition(2, PIL3);
    }
}
