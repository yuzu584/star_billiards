using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject chargeUI;       // �`���[�W��UI
    public Text chargeValue;          // �`���[�W�̐��l
    public Image chargeCircle;        // �`���[�W�̉~
    public Text chargeName;           // �`���[�W�̕���
    public Image EnergyGauge;         // �G�l���M�[�Q�[�W
    public Image EnergyGaugeOutline;  // �G�l���M�[�Q�[�W�̘g
    public Text NoEnergy;             // �G�l���M�[���Ȃ��|��`����e�L�X�g
    public Text EnergyValue;             // �G�l���M�[�̐��l

    void Start()
    {
        // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
        NoEnergy.enabled = false;
    }

    void Update()
    {
        // �G�l���M�[�Q�[�W�̑�����`��
        EnergyGauge.fillAmount = EnergyController.energy / EnergyController.maxEnergy;

        // �G�l���M�[�̐��l��\��
        EnergyValue.text = EnergyController.energy.ToString("0");

        // �`���[�W����Ă���Ȃ�
        if (Shot.charge > 0)
        {
            // UI��L����
            chargeUI.SetActive(true);

            // �`���[�W�̐��l���e�L�X�g�ŕ\��
            chargeValue.text = Shot.charge.ToString("0") + "%";

            // �`���[�W�̉~��`��
            chargeCircle.fillAmount = Shot.charge / 100;
        }
        // �`���[�W����Ă��Ȃ��Ȃ�
        else if (Shot.charge == 0)
        {
            // UI�𖳌���
            chargeUI.SetActive(false);
        }

        // �G�l���M�[��0�ȉ��Ȃ�
        if(EnergyController.energy <= 0)
        {
            // �G�l���M�[�Q�[�W�̘g��ԐF�ɂ���
            EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g��\��
            NoEnergy.enabled = true;
        }
        else
        {
            // �G�l���M�[�Q�[�W�̘g�𔒐F�ɂ���
            EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
            NoEnergy.enabled = false;
        }
    }
}
