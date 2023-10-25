using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRay : MonoBehaviour
{
    [SerializeField] Canvas canvas;             // canvasをInspectorで指定
    [SerializeField] UIController uIController; // UIControllerをInspectorで指定

    RaycastHit hit;              // Rayのhit
    Camera camera = Camera.main; // メインカメラ
    Vector3 hitObjectPosition;   // hitしたオブジェクトの座標

    void Update()
    {
        // Rayを生成
        Ray ray = new Ray(canvas.transform.position,camera.transform.forward);

        // 球体のRayを生成
        if (Physics.SphereCast(ray, 10.0f, out hit))
        {
            // hitしたオブジェクトの座標を取得
            hitObjectPosition = hit.collider.gameObject.transform.position;

            // 惑星情報UIのリングを描画
            uIController.DrawPlanetInfoUI(hitObjectPosition);
        }
    }
}
