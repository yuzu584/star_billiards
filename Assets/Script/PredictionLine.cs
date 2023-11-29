using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// 反射するRayとLineを生成
public class PredictionLine : MonoBehaviour
{
    [SerializeField] private GameObject target;                 // Rayを出すオブジェクト
    [SerializeField] private GameObject directionTarget;        // Rayの向きを決めるオブジェクト
    [SerializeField] private LineRenderer lineRenderer;         // Inspectorでlinerendererを指定
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定
    [SerializeField] private EnergyController energyController; // InspectorでEnergyControllerを指定

    Rigidbody rb;                // InspectorでRigidbodyを指定
    Vector3 origin;              // 原点
    Vector3 direction;           // X軸方向を表すベクトル
    RaycastHit hit;              // Rayのhit
    Vector3 inDirection;         // 入射ベクトル（速度）
    Vector3 inNormal;            // 法線ベクトル
    Vector3 reflectionDirection; // 反射ベクトル

    void Start()
    {
        // 始点の太さを指定
        lineRenderer.startWidth = AppConst.PREDICTION_LINE_START_WIDTH;

        // 終点の太さを指定
        lineRenderer.endWidth = AppConst.PREDICTION_LINE_END_WIDTH;

        // lineRendererの線の数を指定
        lineRenderer.positionCount = 3;

        // rigidbodyを取得
        rb = target.GetComponent<Rigidbody>();
    }

    // RayとLineの向きを決める関数
    public Vector3 RayDirection()
    {
        // 原点をtargetの位置にする
        origin = target.transform.position;

        // ベクトルをdirectionTargetの向きにする
        direction = directionTarget.transform.forward;

        // Rayを生成
        Ray ray = new Ray(origin, direction);

        // プレイヤーと同じ幅のRayを生成
        if (Physics.SphereCast(ray, target.transform.localScale.x, out hit))
        {
            // Rayに沿ってLineを描画
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, hit.point);
        }

        // Rayが当たった面の法線ベクトルを代入
        inNormal = hit.normal;

        // プレイヤーの前向きのベクトルを代入
        direction = directionTarget.transform.forward;

        // 反射ベクトルを計算
        reflectionDirection = Vector3.Reflect(direction, inNormal);

        // Rayを生成
        ray = new Ray(hit.point, reflectionDirection);

        // プレイヤーと同じ幅のRayを生成
        if (Physics.SphereCast(ray, target.transform.localScale.x, out hit))
        {
            // 反射後のRayに沿ってLineを描画
            lineRenderer.SetPosition(2, hit.point);
        }

        // 反射方向を返す
        return reflectionDirection;
    }

    void Update()
    {
        // ゲーム画面なら
        if (screenController.screenNum == 0)
        {
            // エネルギーがある状態で発射ボタン1が押されていたら
            if ((Input.GetAxisRaw("Fire1") > 0) && (energyController.energy > 0))
            {
                // 線を表示
                lineRenderer.enabled = true;
            }
            // 線が表示されていたら
            else if (lineRenderer.enabled == true)
            {
                // 線を非表示
                lineRenderer.enabled = false;
            }
        }
        // ゲーム画面ではないなら
        else if(lineRenderer.enabled == true)
        {
            // 線を非表示
            lineRenderer.enabled = false;
        }
    }
}
