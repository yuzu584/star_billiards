using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �Q�[������UI���Ǘ�
public class UIController : MonoBehaviour
{
    // Inspector��UI�̔z����w��
    [SerializeField]
    UIList UiList;

    // UI�̔z��
    [System.Serializable]
    public class UIList
    {
        public GameObject chargeUI;        // �`���[�W��UI
        public Image chargeCircle;         // �`���[�W�̉~
        public Text chargeValue;           // �`���[�W�̐��l
        public Text chargeName;            // �`���[�W�̕���
        public Image EnergyGauge;          // �G�l���M�[�Q�[�W
        public Image EnergyGaugeDecrease;  // �G�l���M�[�Q�[�W�̌�����
        public Image EnergyGaugeOutline;   // �G�l���M�[�Q�[�W�̘g
        public Text EnergyValue;           // �G�l���M�[�̐��l
        public Text NoEnergy;              // �G�l���M�[���Ȃ��|��`����e�L�X�g
    }

    void Start()
    {
        // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
        UiList.NoEnergy.enabled = false;
    }

    void Update()
    {
        // �G�l���M�[�Q�[�W�̑�����`��
        UiList.EnergyGauge.fillAmount = EnergyController.energy / EnergyController.maxEnergy;

        if (UiList.EnergyGaugeDecrease.fillAmount > EnergyController.energy / EnergyController.maxEnergy)
        {
            // �G�l���M�[�Q�[�W�̌����ʂ����������炷
            UiList.EnergyGaugeDecrease.fillAmount -= 
                (UiList.EnergyGaugeDecrease.fillAmount - EnergyController.energy / EnergyController.maxEnergy) * Time.deltaTime;
        }

        // �G�l���M�[�̐��l��\��
        UiList.EnergyValue.text = EnergyController.energy.ToString("0");

        // �`���[�W����Ă���Ȃ�
        if (Shot.charge > 0)
        {
            // UI��L����
            UiList.chargeUI.SetActive(true);

            // �`���[�W�̐��l���e�L�X�g�ŕ\��
            UiList.chargeValue.text = Shot.charge.ToString("0") + "%";

            // �`���[�W�̉~��`��
            UiList.chargeCircle.fillAmount = Shot.charge / 100;
        }
        // �`���[�W����Ă��Ȃ��Ȃ�
        else if (Shot.charge == 0)
        {
            // UI�𖳌���
            UiList.chargeUI.SetActive(false);
        }

        // �G�l���M�[��0�ȉ��Ȃ�
        if(EnergyController.energy <= 0)
        {
            // �G�l���M�[�Q�[�W�̘g��ԐF�ɂ���
            UiList.EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);

            // �G�l���M�[�Q�[�W�̐��l��ԐF�ɂ���
            UiList.EnergyValue.color = new Color32(155, 0, 0, 100);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g��\��
            UiList.NoEnergy.enabled = true;
        }
        else
        {
            // �G�l���M�[�Q�[�W�̘g�𔒐F�ɂ���
            UiList.EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);

            // �G�l���M�[�Q�[�W�̐��l�𔒐F�ɂ���
            UiList.EnergyValue.color = new Color32(255, 255, 255, 255);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
            UiList.NoEnergy.enabled = false;
        }
    }
}
