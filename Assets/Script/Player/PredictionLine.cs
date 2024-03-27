using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// 反射するRayとLineを生成
public class PredictionLine : Singleton<PredictionLine>
{
    [SerializeField] private GameObject target;                 // Rayを出すオブジェクト
    [SerializeField] private GameObject directionTarget;        // Rayの向きを決めるオブジェクト
    [SerializeField] private LineRenderer lineRenderer;         // Inspectorでlinerendererを指定

    private EnergyController eneCon;
    private InputController input;

    private Rigidbody rb;                   // InspectorでRigidbodyを指定
    private Vector3 origin;                 // 原点
    private Vector3 direction;              // X軸方向を表すベクトル
    private Vector3 inDirection;            // 入射ベクトル（速度）
    private Vector3 inNormal;               // 法線ベクトル
    private Vector3 reflectionDirection;    // 反射ベクトル

    public RaycastHit hit1, hit2;           // Rayのhit

    void Start()
    {
        eneCon = EnergyController.instance;
        input = InputController.instance;

        // 始点の太さを指定
        lineRenderer.startWidth = AppConst.PREDICTION_LINE_START_WIDTH;

        // 終点の太さを指定
        lineRenderer.endWidth = AppConst.PREDICTION_LINE_END_WIDTH;

        // lineRendererの線の数を指定
        lineRenderer.positionCount = 2;

        // 線を非表示
        lineRenderer.enabled = false;

        // rigidbodyを取得
        rb = target.GetComponent<Rigidbody>();

        input.game_OnShotDele += RenderProcess;
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
        if (Physics.SphereCast(ray, target.transform.localScale.x, out hit1))
        {
            // Rayが当たった面の法線ベクトルを代入
            inNormal = hit1.normal;
        }

        // プレイヤーの前向きのベクトルを代入
        direction = directionTarget.transform.forward;

        // 反射ベクトルを計算
        reflectionDirection = Vector3.Reflect(direction, inNormal);

        // Rayを生成
        ray = new Ray(hit1.point, reflectionDirection);

        // プレイヤーと同じ幅のRayを生成
        if (Physics.SphereCast(ray, target.transform.localScale.x, out hit2))
        {
            // 反射後のRayに沿ってLineを描画
            lineRenderer.SetPosition(0, hit1.point);
            lineRenderer.SetPosition(1, hit2.point);
        }

        // 反射方向を返す
        return reflectionDirection;
    }

    // 線を表示/非表示にする処理
    void RenderProcess(float value)
    {
        // エネルギーがある状態でショットボタンが押されていたら
        if ((value > 0) && (eneCon.energy > 0))
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
}
