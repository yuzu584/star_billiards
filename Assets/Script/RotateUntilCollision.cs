using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 何かと衝突するまで公転と自転させる
public class RotateUntilCollision : MonoBehaviour
{
    [SerializeField] GameObject target; // 公転の中心とするオブジェクト
    [SerializeField] int speed = 1;     // 速度

    Vector3 targetPosition; // 公転の中心とするオブジェクトの座標

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        // ターゲットの座標取得
        targetPosition = target.transform.position;

        // 公転させる
        transform.RotateAround(targetPosition, Vector3.up, speed * Time.deltaTime);
    }
}
