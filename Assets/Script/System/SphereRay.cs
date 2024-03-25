using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// 球体のRayを生成し、関数を呼び出す
public class SphereRay : Singleton<SphereRay>
{
    [System.NonSerialized] public RaycastHit hit;            // Rayのhit
    [System.NonSerialized] public Vector3 hitObjectPosition; // hitしたオブジェクトの座標
    [System.NonSerialized] public string hitObjectName;      // hitしたオブジェクトの名前
    [System.NonSerialized] public string hitObjectTag;       // hitしたオブジェクトのタグ

    private PlanetInfoUIController planetInfoUICon;
    private ScreenController scrCon;
    private UIController uICon;

    private void Start()
    {
        planetInfoUICon = PlanetInfoUIController.instance;
        scrCon = ScreenController.instance;
        uICon = UIController.instance;
    }

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

        // ゲーム中かつ対象が惑星なら描画
        if ((scrCon.Screen == ScreenController.ScreenType.InGame) && (hitObjectTag == "Planet"))
        {
            // 非表示なら表示
            if (!uICon.planetInfoUI.allPlanetInfo.activeSelf)
                uICon.planetInfoUI.allPlanetInfo.SetActive(true);

            // 惑星情報UIのリングを描画
            planetInfoUICon.DrawPlanetInfoUI(hitObjectPosition, hitObjectName);

            // 視点移動速度を遅くする
            TPSCamera.instance.rate = AppConst.CAMERA_SLOW_SPEED_RATE;
        }
        // 表示されているなら
        else if (uICon.planetInfoUI.allPlanetInfo.activeSelf)
        {
            // 非表示
            uICon.planetInfoUI.allPlanetInfo.SetActive(false);

            // 視点移動速度を元に戻す
            TPSCamera.instance.rate = AppConst.CAMERA_DEFAULT_SPEED_RATE;
        }
    }
}
