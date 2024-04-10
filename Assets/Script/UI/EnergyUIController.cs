using AppParam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �G�l���M�[��UI���Ǘ�
public class EnergyUIController : Singleton<EnergyUIController>
{
    [SerializeField] private Image energyGauge;                 // �G�l���M�[�Q�[�W�̉摜
    [SerializeField] private Image energyAfterImage;            // �G�l���M�[�Q�[�W�̎c���̉摜
    [SerializeField] private Text EnergyValue;                  // �G�l���M�[�Q�[�W�̎c�ʂ̃e�L�X�g

    private EnergyController eneCon;

    void Start()
    {
        eneCon = EnergyController.instance;
    }

    private void Update()
    {
        Draw();
    }

    // �G�l���M�[��UI��`��
    void Draw()
    {
        // �G�l���M�[�Q�[�W�̑�����`��
        energyGauge.fillAmount = (float)Param_Player.energy.Value / (float)Param_Player.energy.Max;

        if (energyAfterImage.fillAmount > (float)Param_Player.energy.Value / (float)Param_Player.energy.Max)
        {
            // �G�l���M�[�Q�[�W�̌����ʂ����������炷
            energyAfterImage.fillAmount -=
                (energyAfterImage.fillAmount - (float)Param_Player.energy.Value / (float)Param_Player.energy.Max) * Time.deltaTime;
        }

        // �G�l���M�[�̐��l��\��
        EnergyValue.text = Param_Player.energy.Value.ToString("0");
    }
}
