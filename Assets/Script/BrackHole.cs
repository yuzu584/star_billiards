using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ブラックホールを管理
public class BrackHole : MonoBehaviour
{
    // Findで探すGameObject
    private GameObject stageController;

    // Findで探したGameObjectのコンポーネント
    private DestroyPlanet _destroyPlanet;

    // 周囲のオブジェクトに重力の影響を与える
    void Gravity()
    {
        // 指定した半径の当たり判定を生成
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            1000.0f,
            Vector3.forward);

        // 当たり判定に触れたオブジェクトの数繰り返す
        foreach (var hit in hits)
        {
            // 当たったオブジェクトのRigidBodyを取得
            Rigidbody hitObj = hit.collider.gameObject.GetComponent<Rigidbody>();

            // RigidBodyが取得できたなら
            if (hitObj != null)
            {
                // 力を加えるベクトルを設定(スケールによって力が変わる)
                Vector3 direction = (this.gameObject.transform.position - hitObj.position) * this.transform.localScale.x / 2;

                // オブジェクトとの距離が近いほど強い力を加える
                float distance = Vector3.Distance(this.gameObject.transform.position, hitObj.position);
                hitObj.AddForce(direction / distance);
            }
        }
    }

    // もし何かと衝突したら
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが惑星なら破壊
        _destroyPlanet.DestroyPlanetPrpcess(collision.gameObject);
    }

    void Start()
    {
        // GameObjectを探す
        stageController = GameObject.Find("StageController");

        // 探したGameObjectのコンポーネントを取得
        _destroyPlanet = stageController.gameObject.GetComponent<DestroyPlanet>();
    }

    void Update()
    {
        // 周囲のオブジェクトに重力の影響を与える
        Gravity();
    }
}
