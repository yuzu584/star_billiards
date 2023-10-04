using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTorque : MonoBehaviour
{
    public float speed = 1f;                       // ��]���x
    public Vector3 torque = new Vector3(0, 1, 0);  // ��]����

    void Start()
    {
        // Rigidbody���擾
        Rigidbody rb = GetComponent<Rigidbody>();

        // ��]�����ɉ����鑬�x���v�Z
        torque *= speed;

        // Rigidbody�ɗ͂�������
        rb.AddTorque(torque, ForceMode.Acceleration);
    }
}
