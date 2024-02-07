using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージと衝突したとき反発する
public class BounceByStage : MonoBehaviour
{
    public int bouncePower = 100;  // 衝突したときの反発力

    Rigidbody rb;  // 自分のRigidbody

    void Start()
    {
        // rigidbodyを取得
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // 反発力を加える
        rb.AddForce(collision.contacts[0].normal * bouncePower * 100);
    }
}
