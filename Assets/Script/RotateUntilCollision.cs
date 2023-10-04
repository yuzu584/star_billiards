using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 自転させる
public class RotateUntilCollision : MonoBehaviour
{
    public float speed = 1f;  // 回転速度
    float x = 0f;             // マテリアルのオフセットのXの数値
    int maxX = 1;             // マテリアルのオフセットのXの最大値

    // 衝突判定が終わったら
    void OnCollisionExit(Collision collision)
    {
        // 最大値を0にする
        maxX = 0;
    }

    void Update()
    {
        // まだ衝突していなければ
        if (maxX == 1)
        {
            // マテリアルのオフセットのXの数値を増加
            x += speed * Time.deltaTime * 0.01f;

            // xの数値が最大値を超えたら
            if (x > maxX)
            {
                // xの数値を0に戻す
                x = 0f;
            }

            // マテリアルのオフセットを更新
            GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(x, 0f));
        }
    }
}
