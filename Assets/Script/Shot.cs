using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// 発射ボタンで弾を発射する
public class Shot : MonoBehaviour
{
    [SerializeField] private PredictionLine predictionLine;       // InspectorでPredictionLineを指定
    [SerializeField] private EnergyController energyController;   // InspectorでEnergyControllerを指定
    [SerializeField] private ScreenController screenController;   // InspectorでScreenControllerを指定
    [SerializeField] private JustShot justShot;                   // InspectorでJustShotを指定

    public float speed = AppConst.PLAYER_DEFAULT_SPEED;           // 移動速度
    public float charge = 0;                                      // 球のチャージ
    public float chargeSpeed = AppConst.DEFAULT_CHARGE_SPEED;     // 球のチャージ速度
    public int playerBouncePower = AppConst.DEFAULT_BOUNCE_POWER; // 衝突したときのプレイヤーの反発力
    public int planetBouncePower = AppConst.DEFAULT_BOUNCE_POWER; // 衝突したときの惑星の反発力

    private Vector3 direction;                       // 向き
    private Rigidbody rb;                            // プレイヤーのRigidbody
    private Rigidbody cRb;                           // 衝突したオブジェクトのRigidbody
    private RaycastHit hit;                          // Rayのhit
    private int power = 0;                           // 衝突時のパワー
    private Vector3 colObjVelocity;                  // 衝突したオブジェクトのvelocityを保存

    // プレイヤーとオブジェクトに力を加える
    void AddPower()
    {
        // 移動制限を解除
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        cRb.constraints = RigidbodyConstraints.None;

        // プレイヤーとオブジェクトに力を加える
        rb.AddForce(direction * speed * playerBouncePower / (2 * power));
        cRb.velocity = colObjVelocity;
        cRb.velocity *= planetBouncePower / (50 / power);
    }

    void Start()
    {
        // rigidbodyを取得
        rb = GetComponent<Rigidbody>();
    }

    // 衝突したとき
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトのタグがPlanetなら
        if (collision.gameObject.tag == "Planet")
        {
            // 衝突したオブジェクトのrigidbodyを取得
            cRb = collision.gameObject.GetComponent<Rigidbody>();

            // ジャストショットの猶予時間内なら強い力で飛ばす
            if (justShot.time > 0.0f)
            {
                power = 2;
                StopAllCoroutines();
                StartCoroutine(justShot.UIAnimation());
            }

            // ジャストショットの猶予時間外なら通常の力で飛ばす
            else power = 1;

            // ヒットストップ処理(衝突したオブジェクトの速度に応じてヒットストップ時間が変化)
            colObjVelocity = cRb.velocity;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            cRb.constraints = RigidbodyConstraints.FreezePosition;
            Invoke("AddPower", Mathf.Clamp(colObjVelocity.magnitude / 10000, 0.1f, 0.5f));
        }
        // タグがPlanet以外なら
        else
        {
            // プレイヤーを加速させる
            rb.AddForce(direction * speed * playerBouncePower);
        }
    }

    // 衝突後
    void OnCollisionExit()
    {
        // Rayを生成
        Ray ray = new Ray(transform.position, rb.velocity.normalized);

        // 球体のRayを生成
        if (Physics.SphereCast(ray, transform.localScale.x, out hit))
        {
            // プレイヤーの移動方向とRayが当たった位置の法線ベクトルから角度を計算
            direction = Vector3.Reflect(rb.velocity.normalized, hit.normal);
        }
    }

    void FixedUpdate()
    {
        // エネルギーがある状態で発射ボタンが押されたら減速
        if ((Input.GetAxisRaw("Fire1") > 0) && (energyController.energy > 0))
            rb.velocity *= AppConst.SPEED_REDUCTION_RATE;
    }

    void Update()
    {
        // ゲーム画面なら
        if (screenController.screenNum == 5)
        {
            // エネルギーがある状態で発射ボタンが押されていたら(長押し可)
            if ((Input.GetAxisRaw("Fire1") > 0) && (energyController.energy > 0))
            {
                // 向きを設定してチャージする
                direction = predictionLine.RayDirection();
                charge += (chargeSpeed * Time.deltaTime) * 50;
            }
            // 発射ボタンが押されてないかつチャージ済みなら
            else if ((Input.GetAxisRaw("Fire1") == 0) && (charge > 0))
            {
                // エネルギーを消費して発射
                energyController.energy -= charge / 10;
                Vector3 velocity = Camera.main.transform.forward;
                rb.AddForce(velocity * speed * charge);
                charge = 0;
            }

            // エネルギーがある状態で発射ボタンが押されたら(押した瞬間だけ)
            if ((Input.GetButtonDown("Fire1")) && (energyController.energy > 0))
            {
                // ジャストショットの猶予時間をカウント
                StartCoroutine(justShot.JustShotCount());
            }
        }

        // ゲーム画面以外かつチャージ中なら
        else if(charge > 0)
        {
            // チャージをリセット
            charge = 0;
        }
    }
}
