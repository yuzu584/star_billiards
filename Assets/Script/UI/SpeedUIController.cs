using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �ړ����x��UI���Ǘ�
public class SpeedUIController : Singleton<SpeedUIController>
{
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��
    [SerializeField] private Rigidbody rb;                      // �v���C���[��Rigidbody

    // �ړ����x�̐��l��`��
    public void DrawSpeedValue(Text speedValue)
    {
        // ���x�̃e�L�X�g���X�V
        speedValue.text = rb.velocity.magnitude.ToString("0") + " km/s";
    }
}
