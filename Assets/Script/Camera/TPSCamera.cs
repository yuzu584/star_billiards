using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �J�����̎��_�ړ�
public class TPSCamera : MonoBehaviour
{
    [SerializeField] private InputController input; // Inspector��InputController���w��
    [SerializeField] private GameObject player;     // �v���C���[
    [SerializeField] private float speed = 1.0f;    // ���_�ړ����x

    void Start()
    {
        input.game_OnLookDele += MoveCameraAngle;
    }

    // ���_�ړ�����
    void MoveCameraAngle(Vector2 vec)
    {
        // X�����Ɉ��ʈړ����Ă���Ή���]
        if (Mathf.Abs(vec.x) > 0.001f)
        {
            // ��]���̓��[���h���W��Y��
            transform.RotateAround(player.transform.position, transform.up, vec.x * speed);
        }

        // Y�����Ɉ��ʈړ����Ă���Ώc��]
        if (Mathf.Abs(vec.y) > 0.001f)
        {
            // ��]���̓J�������g��X��
            transform.RotateAround(player.transform.position, transform.right, -vec.y * speed);
        }
    }
}
