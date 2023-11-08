using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 自分に衝突した惑星を削除
public class DestroyPlanet : MonoBehaviour
{
    [SerializeField] UIController uIController;

    // もし何かと衝突したら
    void OnCollisionEnter(Collision collision)
    {
        // 惑星と衝突したら
        if (collision.gameObject.CompareTag("Planet"))
        {
            // ポップアップの数をカウント
            uIController.popupAmount++;

            // 惑星が破壊された旨を伝えるポップアップを描画
            StartCoroutine(uIController.DrawDestroyPlanetPopup(collision.gameObject.name));

            // 惑星を削除
            Destroy(collision.gameObject);
        }
    }
}
