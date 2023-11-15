using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �ړ����x��UI���Ǘ�
public class SpeedUIController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��
    [SerializeField] private Rigidbody rb;                      // �v���C���[��Rigidbody

    // �ړ����x�̐��l��`��
    public void DrawSpeedValue(Text speedValue)
    {
        // �|�[�Y��ʂ���UI���\������Ă���Ȃ��\���ɂ���
        if ((screenController.screenNum == 1) && (speedValue.enabled == true))
        {
            speedValue.enabled = false;
        }
        // �Q�[����ʂȂ�
        else if (screenController.screenNum == 0)
        {
            // ��\���Ȃ�\��
            if (speedValue.enabled == false)
            {
                speedValue.enabled = true;
            }

            // ���x�̃e�L�X�g���X�V
            speedValue.text = rb.velocity.magnitude.ToString("0") + " km/s";
        }
    }
}
