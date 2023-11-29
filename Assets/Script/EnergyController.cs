using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// エネルギーの増減を管理
public class EnergyController : MonoBehaviour
{
    public float energy = 1000;     // プレイヤーのエネルギー
    public float maxEnergy = 1000;  // 最大エネルギー
    private Rigidbody rb;           // リジッドボディ

    void Start()
    {
        // 速度を参照するオブジェクトのrigidbodyを取得
        rb = this.GetComponent<Rigidbody>();
    }

    // 何かと衝突したとき
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトのタグがPlanetなら
        if (collision.gameObject.CompareTag("Planet"))
        {
            // エネルギーを衝突したときの速度に応じて回復させる
            energy += rb.velocity.magnitude / 10;
        }
    }

    void FixedUpdate()
    {
        // エネルギーの数値が範囲外なら範囲内に戻す
        if (energy > maxEnergy)
            energy = maxEnergy;
        else if (energy < 0)
            energy = 0;
    }
}
