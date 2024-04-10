using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AppConst;
using UnityEngine.UIElements;

// 球体のRayを生成し、関数を呼び出す
public class SphereRay : Singleton<SphereRay>
{
    [System.NonSerialized] public RaycastHit hit;            // Rayのhit
    [System.NonSerialized] public Vector3 hitObjectPosition; // hitしたオブジェクトの座標
    [System.NonSerialized] public string hitObjectName;      // hitしたオブジェクトの名前
    [System.NonSerialized] public string hitObjectTag;       // hitしたオブジェクトのタグ

    
    private UIController uICon;

    private void Start()
    {
        uICon = UIController.instance;
    }

    void Update()
    {
        // Rayを生成
        Ray ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);

        // 球体のRayを生成
        if (Physics.SphereCast(ray, Const_Player.SPHERE_RAY_WIDTH, out hit))
        {
            // hitしたオブジェクトの座標と名前とタグを取得
            hitObjectPosition = hit.collider.gameObject.transform.position;
            hitObjectName = hit.collider.gameObject.name;
            hitObjectTag = hit.collider.gameObject.tag;
        }
    }
}
