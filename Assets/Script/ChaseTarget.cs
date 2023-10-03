using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : MonoBehaviour
{
    // 追いかける対象
    public GameObject target;

    void Update()
    {
        // 座標を対象の位置に移動
        transform.position = target.transform.position;

        // 座標を少し上にずらす
        transform.position += new Vector3(0, 0.5f, 0);
    }
}
