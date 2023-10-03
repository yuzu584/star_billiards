using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    // Inspector�Ńv���C���[���w��
    [SerializeField] GameObject player;

    // ���_�ړ����x
    public float speed = 1.0f;

    void Update()
    {
        // �}�E�X�̈ړ��ʂ��擾
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        // X�����Ɉ��ʈړ����Ă���Ή���]
        if (Mathf.Abs(mx) > 0.001f)
        {
            // ��]���̓��[���h���W��Y��
            transform.RotateAround(player.transform.position, Vector3.up, mx * speed);
        }

        // Y�����Ɉ��ʈړ����Ă���Ώc��]
        if (Mathf.Abs(my) > 0.001f)
        {
            // ��]���̓J�������g��X��
            transform.RotateAround(player.transform.position, transform.right, -my * speed);
        }
    }
}
