using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 指定のオブジェクトに向き続ける
public class LookAt : MonoBehaviour
{
    public GameObject target; // このオブジェクトの方を向く

    void Update()
    {
        // オブジェクトの方を向く
        transform.LookAt(target.transform);
    }
}
