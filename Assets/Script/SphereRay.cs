using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 球体のRayを生成し、関数を呼び出す
public class SphereRay : MonoBehaviour
{
    [SerializeField] UIController uIController; // UIControllerをInspectorで指定

    RaycastHit hit;              // Rayのhit
    Vector3 hitObjectPosition;   // hitしたオブジェクトの座標

    void Update()
    {
        // Rayを生成
        Ray ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);

        // 球体のRayを生成
        if (Physics.SphereCast(ray, 20.0f, out hit))
        {
            // hitしたオブジェクトの座標を取得
            hitObjectPosition = hit.collider.gameObject.transform.position;

            // 惑星情報UIのリングを描画
            uIController.DrawPlanetInfoUI(hitObjectPosition);
        }
    }
}
