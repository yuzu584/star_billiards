using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle; // 発生させるパーティクル

    // もし衝突したら
    private void OnCollisionEnter(Collision collision)
    {
        // パーティクルシステムのインスタンスを生成
        ParticleSystem newParticle = Instantiate(particle);

        // 生成したパーティクルをプレイヤーの座標に移動
        newParticle.transform.position = collision.contacts[0].point;

        // パーティクルの向きをプレイヤーに向ける
        newParticle.transform.LookAt(gameObject.transform);

        // パーティクルを発生
        newParticle.Play();

        // パーティクルを削除
        Destroy(newParticle.gameObject, 5.0f);
    }
}
