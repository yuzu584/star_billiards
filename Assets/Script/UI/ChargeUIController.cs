using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �`���[�W��UI���Ǘ�
public class ChargeUIController : MonoBehaviour
{
    [SerializeField] private Shot shot; // Inspector��Shot���w��

    // �`���[�W��UI��`��
    public void DrawChargeUI(bool draw, GameObject allChargeUI, Text chargeValue, Image chargeCircle)
    {
        // �`�悷��Ȃ�
        if (draw)
        {
            // �`���[�W����Ă���Ȃ�
            if (shot.charge > 0)
            {
                // �`���[�W�̐��l���e�L�X�g�ŕ\��
                chargeValue.text = shot.charge.ToString("0") + "%";

                // �`���[�W�̉~��`��
                chargeCircle.fillAmount = shot.charge / 100;
            }
        }

        // �\��/��\���؂�ւ�
        if (allChargeUI.activeSelf != draw)
        {
            allChargeUI.SetActive(draw);
            chargeValue.enabled = draw;
            chargeCircle.enabled = draw;
        }
    }
}
