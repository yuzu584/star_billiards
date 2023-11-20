using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 惑星の円の線を管理
public class PlanetCircleLine : MonoBehaviour
{
    public int segments = 20;    // セグメント数
    public float width = 0.1f;   // 幅
    public GameObject centerObj; // 中心のオブジェクト
    public GameObject radiusObj; // 外周のオブジェクト
    public GameObject parentObj; // 親オブジェクト

    void Start()
    {
        // Linerendererコンポーネントをセット
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

        // マテリアルを設定
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // 線の太さを設定
        lineRenderer.widthMultiplier = width;

        // セグメント数を設定
        lineRenderer.positionCount = segments + 1;

        float deltaTheta = (2f * Mathf.PI) / segments; // セグメントごとの角度
        float theta = 0f;                              // シータ

        // 中心と外周のオブジェクトの座標を代入
        Vector3 centerPos = centerObj.transform.position;
        Vector3 radiusPos = radiusObj.transform.position;

        // 中心と外周のオブジェクトの距離を半径に代入
        float radius = Vector3.Distance(centerPos, radiusPos);

        // セグメントごとの設定
        for (int i = 0; i < segments + 1; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, 0f, z) + parentObj.transform.position;
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}
