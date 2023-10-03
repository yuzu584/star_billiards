using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
    // プレイヤーのエネルギー
    public static float energy = 1000;

    // 最大エネルギー
    public static float maxEnergy = 1000;

    // リジッドボディ
    private Rigidbody rb;

    void Start()
    {
        // 速度を参照するオブジェクトのrigidbodyを取得
        rb = this.GetComponent<Rigidbody>();
    }

    // 何かと衝突したとき
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトのタグがPlanetなら
        if (collision.gameObject.tag == "Planet")
        {
            // エネルギーを衝突したときの速度に応じて回復させる
            energy += rb.velocity.magnitude / 10;
        }
    }

    void FixedUpdate()
    {
        // もしエネルギーが最大値を超えてたら
        if(energy > maxEnergy)
        {
            // 最大値に戻す
            energy = maxEnergy;
        }
        // もしエネルギーが0未満なら
        else if(energy < 0)
        {
            // エネルギーを0にする
            energy = 0;
        }
    }
}
