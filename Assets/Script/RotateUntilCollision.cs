using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUntilCollision : MonoBehaviour
{
    // 回転速度
    public float speed = 1f;

    // マテリアルのオフセットのXの数値
    float x = 0f;

    // マテリアルのオフセットのXの最大値
    int maxX = 1;

    void OnCollisionExit(Collision collision)
    {
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

            GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(x, 0f));
        }
    }
}
