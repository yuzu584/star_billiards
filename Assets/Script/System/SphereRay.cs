using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// 球体のRayを生成し、関数を呼び出す
public class SphereRay : MonoBehaviour
{
    [SerializeField] private PlanetInfoUIController planetInfoUIController; // PlanetInfoUIControllerをInspectorで指定
    [SerializeField] private ScreenController screenController;             // InspectorでScreenControllerを指定
    [SerializeField] private UIController uIController;                     // InspectorでUIControllerを指定

    [System.NonSerialized] public RaycastHit hit;            // Rayのhit
    [System.NonSerialized] public Vector3 hitObjectPosition; // hitしたオブジェクトの座標
    [System.NonSerialized] public string hitObjectName;      // hitしたオブジェクトの名前
    [System.NonSerialized] public string hitObjectTag;       // hitしたオブジェクトのタグ

    void Update()
    {
        // Rayを生成
        Ray ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);

        // 球体のRayを生成
        if (Physics.SphereCast(ray, AppConst.SPHERE_RAY_WIDTH, out hit))
        {
            // hitしたオブジェクトの座標と名前とタグを取得
            hitObjectPosition = hit.collider.gameObject.transform.position;
            hitObjectName = hit.collider.gameObject.name;
            hitObjectTag = hit.collider.gameObject.tag;
        }

        // ゲーム中なら描画
        if ((screenController.screenNum == 5) && (hitObjectName != "Sphere") && (hitObjectName != "Player"))
        {
            // 非表示なら表示
            if (!uIController.planetInfoUI.allPlanetInfo.activeSelf)
                uIController.planetInfoUI.allPlanetInfo.SetActive(true);

            // 惑星情報UIのリングを描画
            planetInfoUIController.DrawPlanetInfoUI(hitObjectPosition, hitObjectName);
        }
        // 表示されているなら非表示
        else if(uIController.planetInfoUI.allPlanetInfo.activeSelf)
            uIController.planetInfoUI.allPlanetInfo.SetActive(false);
    }
}
