using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 恒星を管理
public class FixedStar : MonoBehaviour
{
    // Findで探すGameObject
    private GameObject stageController;

    // Findで探したGameObjectのコンポーネント
    private DestroyPlanet _destroyPlanet;

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
}
