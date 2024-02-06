using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �G�l���M�[��UI���Ǘ�
public class EnergyUIController : MonoBehaviour
{
    [SerializeField] private EnergyController energyController; // Inspector��EnergyController���w��
    [SerializeField] private Initialize initialize;             // Inspector��Initialize���w��
    [SerializeField] private Image energyGauge;                 // �G�l���M�[�Q�[�W�̉摜
    [SerializeField] private Image energyAfterImage;            // �G�l���M�[�Q�[�W�̎c���̉摜

    // �G�l���M�[��UI��`��
    public void DrawEnergyUI(Image energyGaugeOutline, Text EnergyValue, Text NoEnergy)
    {
        // �G�l���M�[�Q�[�W�̑�����`��
        energyGauge.fillAmount = energyController.energy / energyController.maxEnergy;

        if (energyAfterImage.fillAmount > energyController.energy / energyController.maxEnergy)
        {
            // �G�l���M�[�Q�[�W�̌����ʂ����������炷
            energyAfterImage.fillAmount -=
                (energyAfterImage.fillAmount - energyController.energy / energyController.maxEnergy) * Time.deltaTime;
        }

        // �G�l���M�[�̐��l��\��
        EnergyValue.text = energyController.energy.ToString("0");

        // �G�l���M�[��0�ȉ�����\���Ȃ�
        if ((energyController.energy <= 0) && (NoEnergy.enabled == false))
        {
            // �G�l���M�[�Q�[�W�̘g�Ɛ��l��ԐF�ɂ���
            energyGaugeOutline.color = new Color32(155, 0, 0, 100);
            EnergyValue.color = new Color32(155, 0, 0, 100);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g��\��
            NoEnergy.enabled = true;
        }
        // �G�l���M�[��0���ォ�\������Ă���Ȃ�
        else if ((energyController.energy > 0) && (NoEnergy.enabled == true))
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

    void Start()
    {
        // �f���Q�[�g�ɏ������֐���o�^
        initialize.init_Stage += Init;
    }
}
