using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 何かと衝突するまで公転と自転させる
public class RotateUntilCollision : MonoBehaviour
{
    [SerializeField] GameObject target;     // 公転の中心とするオブジェクト
    [SerializeField] int speed = 1;         // 公転速度

    bool rotate = true;                     // 公転するか否か
    Vector3 targetPosition;                 // 公転の中心とするオブジェクトの座標

    void Update()
    {
        // 公転するなら
        if (rotate)
            // 公転させる
            Rotate();
        else
        {
            // コンポーネントを廃止
            Destroy(this);
        }
    }

    // 何かと衝突したら
    void OnCollisionEnter(Collision collision)
    {
        // アニメーションを停止
        GetComponent<Animation>().Stop();

        // 公転をさせなくする
        rotate = false;
    }

    // 公転させる
    void Rotate()
    {
        // ターゲットの座標取得
        targetPosition = target.transform.position;

        // 公転させる
        transform.RotateAround(targetPosition, Vector3.up, speed * Time.deltaTime);
    }
}
