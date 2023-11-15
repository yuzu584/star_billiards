using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �`���[�W��UI���Ǘ�
public class ChargeUIController : MonoBehaviour
{
    [SerializeField] private Shot shot; // Inspector��Shot���w��

    // �`���[�W��UI��`��
    public void DrawChargeUI(GameObject allChargeUI, Text chargeValue, Image chargeCircle)
    {
        // �`���[�W����Ă���Ȃ�
        if (shot.charge > 0)
        {
            // �`���[�W��UI������������Ă�����
            if (!(allChargeUI.activeSelf))
            {
                // UI��L����
                allChargeUI.SetActive(true);
            }

            // �`���[�W�̐��l���e�L�X�g�ŕ\��
            chargeValue.text = shot.charge.ToString("0") + "%";

            // �`���[�W�̉~��`��
            chargeCircle.fillAmount = shot.charge / 100;
        }
        // �`���[�W����Ă��Ȃ����\������Ă���Ȃ�
        else if ((shot.charge == 0) && (allChargeUI.activeSelf))
        {
            // UI�𖳌���
            allChargeUI.SetActive(false);
        }
    }
}