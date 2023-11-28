using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ターゲットの座標に移動する
public class ChaseTarget : MonoBehaviour
{
    public GameObject target; // ターゲット

    // ターゲットの座標に移動
    public void Chase()
    {
        // ターゲットの座標に移動
        transform.position = target.transform.position;

        // 座標を少し上にずらす
        transform.position += new Vector3(0, 0.5f, 0);
    }
}
