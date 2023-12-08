using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 指定のオブジェクトに向き続ける
public class LookAt : MonoBehaviour
{
    public GameObject target; // このオブジェクトの方を向く

    void Update()
    {
        // ターゲットが存在しないなら自分を削除/存在するならターゲットの方を向く
        if (target == null)
            Destroy(this.gameObject);
        else
            transform.LookAt(target.transform);
    }
}
