using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 発射ボタンで弾を発射する
public class Shot : MonoBehaviour
{
    [SerializeField] CreateRay Cr;       // RayとLineを作る関数の型
    public float speed = 1.0f;           // 移動速度
    public static float charge = 0;      // 球のチャージ
    public float chargeSpeed = 10;       // 球のチャージ速度
    public int playerBouncePower = 100;  // 衝突したときのプレイヤーの反発力
    public int planetBouncePower = 100;  // 衝突したときの惑星の反発力

    Camera cam;         // カメラ
    Vector3 direction;  // 向き
    Rigidbody rb;       // プレイヤーのRigidbody
    Rigidbody cRb;      // 衝突したオブジェクトのRigidbody
    RaycastHit hit;     // Rayのhit

    void Start()
    {
        // rigidbodyを取得
        rb = GetComponent<Rigidbody>();

        // メインカメラを設定
        cam = Camera.main;
    }

    // 衝突したとき
    void OnCollisionEnter(Collision collision)
    {
        // 一瞬速度を0にする
        rb.velocity *= 0;

        // 力を加える
        rb.AddForce(direction * speed * playerBouncePower);

        // 衝突したオブジェクトのタグがPlanetなら
        if (collision.gameObject.tag == "Planet")
        {
            // 衝突したオブジェクトのrigidbodyを取得
            cRb = collision.gameObject.GetComponent<Rigidbody>();

            // 加速させる
            cRb.velocity *= planetBouncePower / 50;
        }
    }

    // 衝突後
    void OnCollisionExit()
    {
        // Rayを生成
        Ray ray = new Ray(transform.position, rb.velocity.normalized);

        // 球体のRayを生成
        if (Physics.SphereCast(ray, 0.5f, out hit))
        {
            // プレイヤーの移動方向とRayが当たった位置の法線ベクトルから角度を計算
            direction = Vector3.Reflect(rb.velocity.normalized, hit.normal);
        }
    }

    void FixedUpdate()
    {
        // エネルギーがある状態で発射ボタンが押されたら
        if ((Input.GetAxisRaw("Fire1") > 0) && (EnergyController.energy > 0))
        {
            // 減速させる
            rb.velocity *= 0.995f;
        }
    }

    void Update()
    {
        // エネルギーがある状態で発射ボタンが押されたら
        if((Input.GetAxisRaw("Fire1") > 0) && (EnergyController.energy > 0))
        {
            // 角度を設定
            direction = Cr.RayDirection();

            // チャージを貯める
            charge += (chargeSpeed * Time.deltaTime) * 50;

            // 減速させる
            rb.velocity *= 0.996f;
        }
        // 発射ボタンが押されてないなら
        else if ((Input.GetAxisRaw("Fire1") == 0) && (charge > 0))
        {
            // エネルギーを減少させる
            EnergyController.energy -= charge / 10;

            // ベクトルをカメラの向きにする
            Vector3 velocity = cam.transform.forward;

            // 力を加える
            rb.AddForce(velocity * speed * charge);

            // チャージをリセット
            charge = 0;
        }
    }
}
