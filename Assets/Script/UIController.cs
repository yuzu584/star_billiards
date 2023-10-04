using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �Q�[������UI���Ǘ�
public class UIController : MonoBehaviour
{
    // Inspector��UI�̔z����w��
    [SerializeField]
    UIList UL;

    // UI�̔z��
    [System.Serializable]
    public class UIList
    {
        public GameObject chargeUI;       // �`���[�W��UI
        public Image chargeCircle;        // �`���[�W�̉~
        public Text chargeValue;          // �`���[�W�̐��l
        public Text chargeName;           // �`���[�W�̕���
        public Image EnergyGauge;         // �G�l���M�[�Q�[�W
        public Image EnergyGaugeOutline;  // �G�l���M�[�Q�[�W�̘g
        public Text EnergyValue;          // �G�l���M�[�̐��l
        public Text NoEnergy;             // �G�l���M�[���Ȃ��|��`����e�L�X�g
    }

    EnergyController EC = new EnergyController();  // �C���X�^���X�𐶐�
    Shot St = new Shot();                          // �C���X�^���X�𐶐�

    void Start()
    {
        // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
        UL.NoEnergy.enabled = false;
    }

    void Update()
    {
        // �G�l���M�[�Q�[�W�̑�����`��
        UL.EnergyGauge.fillAmount = EC.energy / EC.maxEnergy;

        // �G�l���M�[�̐��l��\��
        UL.EnergyValue.text = EC.energy.ToString("0");

        // �`���[�W����Ă���Ȃ�
        if (St.charge > 0)
        {
            // UI��L����
            UL.chargeUI.SetActive(true);

            // �`���[�W�̐��l���e�L�X�g�ŕ\��
            UL.chargeValue.text = St.charge.ToString("0") + "%";

            // �`���[�W�̉~��`��
            UL.chargeCircle.fillAmount = St.charge / 100;
        }
        // �`���[�W����Ă��Ȃ��Ȃ�
        else if (St.charge == 0)
        {
            // UI�𖳌���
            UL.chargeUI.SetActive(false);
        }

        // �G�l���M�[��0�ȉ��Ȃ�
        if(EC.energy <= 0)
        {
            // �G�l���M�[�Q�[�W�̘g��ԐF�ɂ���
            UL.EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g��\��
            UL.NoEnergy.enabled = true;
        }
        else
        {
            // �G�l���M�[�Q�[�W�̘g�𔒐F�ɂ���
            UL.EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);

            // �G�l���M�[���Ȃ��|��`����e�L�X�g���\��
            UL.NoEnergy.enabled = false;
        }
    }
}
