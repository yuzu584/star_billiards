using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �G�l���M�[��UI���Ǘ�
public class EnergyUIController : Singleton<EnergyUIController>
{
    [SerializeField] private Image energyGauge;                 // �G�l���M�[�Q�[�W�̉摜
    [SerializeField] private Image energyAfterImage;            // �G�l���M�[�Q�[�W�̎c���̉摜

    private EnergyController eneCon;
    private Initialize init;

    void Start()
    {
        eneCon = EnergyController.instance;
        init = Initialize.instance;

        // �f���Q�[�g�ɏ������֐���o�^
        init.init_Stage += Init;
    }

    // �G�l���M�[��UI��`��
    public void DrawEnergyUI(Image energyGaugeOutline, Text EnergyValue, Text NoEnergy)
    {
        // �G�l���M�[�Q�[�W�̑�����`��
        energyGauge.fillAmount = eneCon.energy / eneCon.maxEnergy;

        if (energyAfterImage.fillAmount > eneCon.energy / eneCon.maxEnergy)
        {
            // �G�l���M�[�Q�[�W�̌����ʂ����������炷
            energyAfterImage.fillAmount -=
                (energyAfterImage.fillAmount - eneCon.energy / eneCon.maxEnergy) * Time.deltaTime;
        }

        // �G�l���M�[�̐��l��\��
        EnergyValue.text = eneCon.energy.ToString("0");

        // �G�l���M�[��0�ȉ�����\���Ȃ�
        if ((eneCon.energy <= 0) && (NoEnergy.enabled == false))
        {
            // �G�l���M�[�Q�[�W�̘g�Ɛ��l��ԐF�ɂ���
            energyGaugeOutline.color = new Color32(155, 0, 0, 100);
            EnergyValue.color = new Color32(155, 0, 0, 100);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g��\��
            NoEnergy.enabled = true;
        }
        // �G�l���M�[��0���ォ�\������Ă���Ȃ�
        else if ((eneCon.energy > 0) && (NoEnergy.enabled == true))
        {
            // �G�l���M�[�Q�[�W�̘g�Ɛ��l�𔒐F�ɂ���
            energyGaugeOutline.color = new Color32(255, 255, 255, 100);
            EnergyValue.color = new Color32(255, 255, 255, 255);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
            NoEnergy.enabled = false;
        }
    }

    // �G�l���M�[��UI��������
    void Init()
    {
        energyGauge.fillAmount = 1;
        energyAfterImage.fillAmount = 1;
    }
}
