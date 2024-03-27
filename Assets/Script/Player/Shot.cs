using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// ショットボタンで弾を発射する
public class Shot : Singleton<Shot>
{

    public float speed = AppConst.PLAYER_DEFAULT_SPEED;           // 移動速度
    public float charge = 0;                                      // 球のチャージ
    public float chargeSpeed = AppConst.DEFAULT_CHARGE_SPEED;     // 球のチャージ速度
    public int playerBouncePower = AppConst.DEFAULT_BOUNCE_POWER; // 衝突したときのプレイヤーの反発力
    public int planetBouncePower = AppConst.DEFAULT_BOUNCE_POWER; // 衝突したときの惑星の反発力

    private PredictionLine pLine;
    private EnergyController eneCon;
    private ScreenController scrCon;
    private JustShot jShot;
    private InputController input;

    private Vector3 direction;                       // 向き
    private Rigidbody rb;                            // プレイヤーのRigidbody
    private Rigidbody cRb;                           // 衝突したオブジェクトのRigidbody
    private RaycastHit hit;                          // Rayのhit
    private int power = 0;                           // 衝突時のパワー
    private Vector3 colObjVelocity;                  // 衝突したオブジェクトのvelocityを保存
    private Coroutine coroutine;                     // ジャストショットの猶予時間をカウントするコルーチン
    private float inputValue = 0;                    // 入力の数値
    private float oldInputValue = 0;                 // 1フレーム前の入力の数値
    private bool nowInput = false;                   // 入力が行われた瞬間かどうか

    // プレイヤーとオブジェクトに力を加える
    void AddPower()
    {
        if (cRb != null)
        {
            // 移動制限を解除
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            cRb.constraints = RigidbodyConstraints.None;

            // プレイヤーとオブジェクトに力を加える
            rb.AddForce(direction * speed * playerBouncePower / (2 * power));
            cRb.velocity = colObjVelocity;
            cRb.velocity *= planetBouncePower / (50 / power);
        }
    }

    void Start()
    {
        pLine = PredictionLine.instance;
        eneCon = EnergyController.instance;
        scrCon = ScreenController.instance;
        jShot = JustShot.instance;
        input = InputController.instance;

        // rigidbodyを取得
        rb = GetComponent<Rigidbody>();

        input.game_OnShotDele += ShotProcess;
    }

    void Update()
    {
        // ショット入力がされた瞬間なら
        if ((inputValue != oldInputValue) && (!nowInput))
        {
            oldInputValue = inputValue;
            nowInput = true;
        }
        // ショット入力がされた瞬間でないなら
        else if ((inputValue == oldInputValue) && (nowInput))
        {
            nowInput = false;
        }
    }

    void FixedUpdate()
    {
        // エネルギーがある状態でショットボタンが押されたら減速
        if ((inputValue > 0) && (eneCon.energy > 0))
            rb.velocity *= AppConst.SPEED_REDUCTION_RATE;
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
            if (jShot.time > 0.0f)
            {
                power = 3;
                StopAllCoroutines();
                StartCoroutine(jShot.UIAnimation());
            }

            // ジャストショットの猶予時間外なら通常の力で飛ばす
            else power = 1;

            // ヒットストップ処理(衝突したオブジェクトの速度に応じてヒットストップ時間が変化)
            rb.velocity *= 0;
            colObjVelocity = cRb.velocity;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            cRb.constraints = RigidbodyConstraints.FreezePosition;
            Invoke("AddPower", Mathf.Clamp(colObjVelocity.magnitude / (10000 / power), 0.1f, 0.5f));
        }

        // 衝突したオブジェクトのタグがFixedStarなら
        else if (collision.gameObject.tag == "FixedStar")
        {
            // エネルギーを0にする(ゲームオーバー)
            eneCon.energy = 0;
        }

        // タグがPlanetとFixedStar以外なら
        else
        {
            // プレイヤーを加速させる
            rb.velocity *= 0;
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

    // ショットの処理
    void ShotProcess(float value)
    {
        inputValue = value;

        // エネルギーがある状態でショットボタンが押されていたら(長押し可)
        if ((inputValue > 0) && (eneCon.energy > 0))
        {
            // 向きを設定してチャージする
            direction = pLine.RayDirection();
            charge += (chargeSpeed * Time.deltaTime) * 50;
        }
        // ショットボタンが押されてないかつチャージ済みなら
        else if ((inputValue == 0) && (charge > 0))
        {
            // エネルギーを消費して発射
            eneCon.energy -= charge / 10;
            Vector3 velocity = Camera.main.transform.forward;
            rb.AddForce(velocity * speed * charge);
            charge = 0;
        }

        // エネルギーがある状態でショットボタンが押されたら(押した瞬間だけ)
        if ((inputValue > 0) && (eneCon.energy > 0) && (nowInput))
        {
            // ジャストショットの猶予時間をカウント
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(jShot.JustShotCount());
        }
    }
}
