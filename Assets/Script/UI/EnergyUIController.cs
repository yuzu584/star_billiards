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
        energyGauge.fillAmount = eneCon.energy / eneCon.maxEnergy;

        if (energyAfterImage.fillAmount > eneCon.energy / eneCon.maxEnergy)
        {
            // �G�l���M�[�Q�[�W�̌����ʂ����������炷
            energyAfterImage.fillAmount -=
                (energyAfterImage.fillAmount - eneCon.energy / eneCon.maxEnergy) * Time.deltaTime;
        }

        // �G�l���M�[�̐��l��\��
        EnergyValue.text = eneCon.energy.ToString("0");
    }
}
