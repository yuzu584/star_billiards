using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTorque : MonoBehaviour
{
    // 回転速度
    public float speed = 1f;

    // 回転方向
    public Vector3 torque = new Vector3(0, 1, 0);

    void Start()
    {
        // Rigidbodyを取得
        Rigidbody rb = GetComponent<Rigidbody>();

        torque *= speed;

        // Rigidbodyに力を加える
        rb.AddTorque(torque, ForceMode.Acceleration);
    }
}
