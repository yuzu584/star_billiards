using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTorque : MonoBehaviour
{
    // ��]���x
    public float speed = 1f;

    // ��]����
    public Vector3 torque = new Vector3(0, 1, 0);

    void Start()
    {
        // Rigidbody���擾
        Rigidbody rb = GetComponent<Rigidbody>();

        torque *= speed;

        // Rigidbody�ɗ͂�������
        rb.AddTorque(torque, ForceMode.Acceleration);
    }
}
