using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ブラックホールを管理
public class BrackHole : MonoBehaviour
{
    private ScreenController scrCont;
    private DestroyPlanet destroyPlanet;

    // 周囲のオブジェクトに重力の影響を与える
    void Gravity(float radius, string tag, float power)
    {
        // 指定した半径の当たり判定を生成
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            radius,
            Vector3.forward);

        // 当たり判定に触れたオブジェクトの数繰り返す
        foreach (var hit in hits)
        {
            // プレイヤーのRigidBodyを取得
            Rigidbody hitObj = null;
            if (hit.collider.gameObject.tag == tag)
                hitObj = hit.collider.gameObject.GetComponent<Rigidbody>();

            // RigidBodyが取得できたなら
            if (hitObj != null)
            {
                // 力を加えるベクトルを設定(スケールによって力が変わる)
                Vector3 direction = (gameObject.transform.position - hitObj.position) * transform.localScale.x * power;

                // オブジェクトとの距離が近いほど強い力を加える
                float distance = Vector3.Distance(gameObject.transform.position, hitObj.position);
                hitObj.AddForce(direction / distance);
            }
        }
    }

    // もし何かと衝突したら
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが惑星なら破壊
        destroyPlanet.DestroyPlanetProcess(collision.gameObject);
    }

    void Start()
    {
        scrCont = ScreenController.instance;
        destroyPlanet = DestroyPlanet.instance;
    }

    void FixedUpdate()
    {
        // ゲーム画面なら周囲のオブジェクトに重力の影響を与える
        if(scrCont.ScreenNum == 5)
        {
            Gravity(1000.0f, "Player", 0.5f);
            Gravity(100.0f, "Player", 1.0f);
            Gravity(100.0f, "Planet", 1.0f);
        }
    }
}
