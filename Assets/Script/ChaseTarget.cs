using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 目標の座標に移動する
public class ChaseTarget : MonoBehaviour
{
    public GameObject target;  // 追いかける対象

    void Update()
    {
        // 座標を対象の位置に移動
        transform.position = target.transform.position;

        // 座標を少し上にずらす
        transform.position += new Vector3(0, 0.5f, 0);
    }
}
