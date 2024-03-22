using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �`���[�W��UI���Ǘ�
public class ChargeUIController : Singleton<ChargeUIController>
{
    [SerializeField] private Shot shot; // Inspector��Shot���w��

    // �`���[�W��UI��`��
    public void DrawChargeUI(GameObject allChargeUI, Text chargeValue, Image chargeCircle)
    {
        // �`���[�W����Ă���Ȃ�
        if (shot.charge > 0)
        {
            // �`���[�W�̐��l���e�L�X�g�ŕ\��
            chargeValue.text = shot.charge.ToString("0") + "%";

            // �`���[�W�̉~��`��
            chargeCircle.fillAmount = shot.charge / 100;
        }
        // �`���[�W����Ă��Ȃ��Ȃ�
        else
        {
            // �`���[�W��UI�����Z�b�g
            chargeValue.text = "0";
            chargeCircle.fillAmount = 0;
        }
    }
}
