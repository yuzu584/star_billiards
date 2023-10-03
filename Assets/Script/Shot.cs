using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    // カメラ
    public GameObject camera;

    // 移動速度
    public float speed = 1.0f;

    // 球のチャージ
    public static float charge = 0;

    // 球のチャージ速度
    public float chargeSpeed = 10;

    // RayとLineを作る関数の型
    public CreateRay createRay;

    public int bouncePower = 100;

    // 向き
    Vector3 direction;

    // プレイヤーのRigidbody
    Rigidbody rb;

    void Start()
    {
        // rigidbodyを取得
        rb = GetComponent<Rigidbody>();
    }

    // 衝突したらdirectionの向きに力を加える
    void OnCollisionEnter(Collision collision)
    {
        // 一瞬速度を0にする
        rb.velocity *= 0;

        // 力を加える
        rb.AddForce(direction * speed * bouncePower);
    }

    // 衝突後に次の角度を設定
    void OnCollisionExit()
    {
        direction = createRay.RayDirection();
    }

    void Update()
    {
        // エネルギーがある状態で発射ボタンが押されたら
        if(Input.GetAxisRaw("Fire1") > 0 && EnergyController.energy > 0)
        {
            // 角度を設定
            direction = createRay.RayDirection();

            // チャージを貯める
            charge += (chargeSpeed * Time.deltaTime) * 50;

            // 減速させる
            rb.velocity *= 0.996f;
        }
        // 発射ボタンが押されてないなら
        else if (Input.GetAxisRaw("Fire1") == 0 && charge > 0)
        {
            // エネルギーを減少させる
            EnergyController.energy -= charge / 10;

            // ベクトルをカメラの向きにする
            Vector3 velocity = camera.transform.forward;

            // 力を加える
            rb.AddForce(velocity * speed * charge);

            // チャージをリセット
            charge = 0;
        }
    }
}
