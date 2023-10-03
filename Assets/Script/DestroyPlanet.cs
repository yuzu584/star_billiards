using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlanet : MonoBehaviour
{
    // もし何かと衝突したら
    void OnCollisionEnter(Collision collision)
    {
        // 惑星と衝突したら
        if (collision.gameObject.CompareTag("Planet"))
        {
            // 惑星を削除
            Destroy(collision.gameObject);
        }
    }
}
