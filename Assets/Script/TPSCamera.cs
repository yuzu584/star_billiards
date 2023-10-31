using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �J�����̎��_�ړ�
public class TPSCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;                 // �v���C���[
    [SerializeField] private ScreenController screenController; // ScreenController�^�̕ϐ�

    public float speed = 1.0f;  // ���_�ړ����x
    float mx;                   // �}�E�X�̉��ړ���
    float my;                   // �}�E�X�̏c�ړ���

    void Update()
    {
        // �Q�[����ʂȂ�
        if (screenController.screenNum == 0)
        {
            // �}�E�X�̈ړ��ʂ��擾
            mx = Input.GetAxis("Mouse X");
            my = Input.GetAxis("Mouse Y");

            // X�����Ɉ��ʈړ����Ă���Ή���]
            if (Mathf.Abs(mx) > 0.001f)
            {
                // ��]���̓��[���h���W��Y��
                transform.RotateAround(player.transform.position, transform.up, mx * speed);
            }

            // Y�����Ɉ��ʈړ����Ă���Ώc��]
            if (Mathf.Abs(my) > 0.001f)
            {
                // ��]���̓J�������g��X��
                transform.RotateAround(player.transform.position, transform.right, -my * speed);
            }
        }
    }
}
