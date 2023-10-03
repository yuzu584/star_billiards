using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUntilCollision : MonoBehaviour
{
    // transform型の変数を宣言
    Transform myTransform;

    // ワールド座標を基準に、回転を取得
    Vector3 worldAngle;

    public float speed = 1f;

    void Update()
    {
        myTransform = this.transform;
        worldAngle = myTransform.eulerAngles;

        worldAngle.y += speed * Time.deltaTime;

        myTransform.eulerAngles = worldAngle; // 回転角度を設定
    }
}
