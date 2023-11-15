using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �G�l���M�[��UI���Ǘ�
public class EnergyUIController : MonoBehaviour
{
    [SerializeField] private EnergyController energyController; // Inspector��EnergyController���w��

    // �G�l���M�[��UI��`��
    public void DrawEnergyUI(bool draw, Image EnergyGauge, Image EnergyAfterImage, Image EnergyGaugeOutline, Text EnergyValue, Text NoEnergy)
    {
        // �`�悷��Ȃ�
        if (draw)
        {
            // �G�l���M�[�Q�[�W�̑�����`��
            EnergyGauge.fillAmount = energyController.energy / energyController.maxEnergy;

            if (EnergyAfterImage.fillAmount > energyController.energy / energyController.maxEnergy)
            {
                // �G�l���M�[�Q�[�W�̌����ʂ����������炷
                EnergyAfterImage.fillAmount -=
                    (EnergyAfterImage.fillAmount - energyController.energy / energyController.maxEnergy) * Time.deltaTime;
            }

            // �G�l���M�[�̐��l��\��
            EnergyValue.text = energyController.energy.ToString("0");

            // �G�l���M�[��0�ȉ�����\���Ȃ�
            if ((energyController.energy <= 0) && (NoEnergy.enabled == false))
            {
                // �G�l���M�[�Q�[�W�̘g�Ɛ��l��ԐF�ɂ���
                EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);
                EnergyValue.color = new Color32(155, 0, 0, 100);

                // �G�l���M�[���Ȃ��|��`����e�L�X�g��\��
                NoEnergy.enabled = true;
            }
            // �G�l���M�[��0���ォ�\������Ă���Ȃ�
            else if ((energyController.energy > 0) && (NoEnergy.enabled == true))
            {
                // �G�l���M�[�Q�[�W�̘g�Ɛ��l�𔒐F�ɂ���
                EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);
                EnergyValue.color = new Color32(255, 255, 255, 255);

                // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
                NoEnergy.enabled = false;
            }
        }

        // �\��/��\���؂�ւ�
        if (EnergyGauge.enabled != draw)
        {
            EnergyGauge.enabled = draw;
            EnergyAfterImage.enabled = draw;
            EnergyGaugeOutline.enabled = draw;
            EnergyValue.enabled = draw;
            NoEnergy.enabled = draw;
        }
    }
}
