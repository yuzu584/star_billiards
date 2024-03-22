using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 恒星を管理
public class FixedStar : MonoBehaviour
{
    private DestroyPlanet destroyPlanet;

    // もし何かと衝突したら
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが惑星なら破壊
        destroyPlanet.DestroyPlanetProcess(collision.gameObject);
    }

    void Start()
    {
        destroyPlanet = DestroyPlanet.instance;
    }
}
