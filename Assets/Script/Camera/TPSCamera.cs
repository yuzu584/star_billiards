using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AppConst;

// �J�����̎��_�ړ�
public class TPSCamera : Singleton<TPSCamera>
{
    [SerializeField] private GameObject player;                     // �v���C���[
    [SerializeField] private float speed = 1.0f;                    // ���_�ړ����x(Inspector�Ŏw��)

    private ClampedValue<float> angleMoveSpeed;                     // ���_�ړ����x
    private InputController input;

    public float rate = Const_Camera.CAMERA_DEFAULT_SPEED_RATE;     // ���_�ړ����x�̔{��

    void Start()
    {
        angleMoveSpeed = new ClampedValue<float>(1.0f, 100.0f, 0.01f, nameof(angleMoveSpeed));

        input = InputController.instance;
        input.game_OnLookDele += MoveCameraAngle;

        angleMoveSpeed.SetValue(speed);
    }

    // ���_�ړ�����
    void MoveCameraAngle(Vector2 vec)
    {
        // X�����Ɉ��ʈړ����Ă���Ή���]
        if (Mathf.Abs(vec.x) > 0.001f)
        {
            // ��]���̓��[���h���W��Y��
            transform.RotateAround(player.transform.position, transform.up, (vec.x * angleMoveSpeed.GetValue_Float()) * rate);
        }

        // Y�����Ɉ��ʈړ����Ă���Ώc��]
        if (Mathf.Abs(vec.y) > 0.001f)
        {
            // ��]���̓J�������g��X��
            transform.RotateAround(player.transform.position, transform.right, (-vec.y * angleMoveSpeed.GetValue_Float()) * rate);
        }
    }
}
