using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �ړ����x��UI���Ǘ�
public class SpeedUIController : MonoBehaviour
{
    [SerializeField] private Text speedText;                    // ���x��\���e�L�X�g

    private Rigidbody rb;                                       // �v���C���[��Rigidbody

    private PlayerController playerCon;

    // �ړ����x�̐��l��`��
    void Draw()
    {
        // ���x�̃e�L�X�g���X�V
        speedText.text = rb.velocity.magnitude.ToString("0") + " km/s";
    }

    private void Start()
    {
        playerCon = PlayerController.instance;

        rb = playerCon.rb;

        // �ړ����x��UI��`�悷��f���Q�[�g�ɓo�^
        playerCon.speedUIDele += Draw;
    }

    private void OnDestroy()
    {
        playerCon.speedUIDele -= Draw;
    }
}
