using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 惑星の方向を示す矢印を管理
public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;  // 矢印のプレハブ
    [SerializeField] private GameObject player; // プレイヤー

    // 矢印を作成
    public void Create(GameObject target)
    {
        // 矢印のインスタンスを生成
        GameObject arrowObj = Instantiate(arrow);

        // 親を設定
        arrowObj.transform.SetParent(player.transform, false);

        // ターゲットを指定
        LookAt lookTarget = arrowObj.GetComponent<LookAt>();
        lookTarget.target = target;
    }
}
