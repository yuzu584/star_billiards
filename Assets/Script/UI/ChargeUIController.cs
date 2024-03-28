using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �`���[�W��UI���Ǘ�
public class ChargeUIController : MonoBehaviour
{
    [SerializeField] private Text chargeValue;
    [SerializeField] private Image chargeCircle;

    private Shot shot;

    private void Start()
    {
        shot = Shot.instance;
    }

    private void Update()
    {
        Draw();
    }

    // �`���[�W��UI��`��
    void Draw()
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
