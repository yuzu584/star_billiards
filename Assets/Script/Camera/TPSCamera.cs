using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �J�����̎��_�ړ�
public class TPSCamera : MonoBehaviour
{
    [SerializeField] private InputController input; // Inspector��InputController���w��
    [SerializeField] private GameObject player;     // �v���C���[
    [SerializeField] private float speed = 1.0f;    // ���_�ړ����x

    Vector2 angleMove; // ���_�ړ���

    public void MoveCameraAngle()
    {
        // �}�E�X�̈ړ��ʂ��擾
        angleMove = input.Game_Look;

        // X�����Ɉ��ʈړ����Ă���Ή���]
        if (Mathf.Abs(angleMove.x) > 0.001f)
        {
            // ��]���̓��[���h���W��Y��
            transform.RotateAround(player.transform.position, transform.up, angleMove.x * speed);
        }

        // Y�����Ɉ��ʈړ����Ă���Ώc��]
        if (Mathf.Abs(angleMove.y) > 0.001f)
        {
            // ��]���̓J�������g��X��
            transform.RotateAround(player.transform.position, transform.right, -angleMove.y * speed);
        }
    }
}
