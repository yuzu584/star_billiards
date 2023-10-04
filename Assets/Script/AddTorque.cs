using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTorque : MonoBehaviour
{
    public float speed = 1f;                       // 回転速度
    public Vector3 torque = new Vector3(0, 1, 0);  // 回転方向

    void Start()
    {
        // Rigidbodyを取得
        Rigidbody rb = GetComponent<Rigidbody>();

        // 回転方向に加える速度を計算
        torque *= speed;

        // Rigidbodyに力を加える
        rb.AddTorque(torque, ForceMode.Acceleration);
    }
}
