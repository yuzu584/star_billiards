using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 視野角を変更する
public class FOV : MonoBehaviour
{
    [SerializeField] private GameObject player;                 // 速度を参照するオブジェクト
    [SerializeField] private int Fov = 60;                      // 視野角

    private Camera cam;   // メインカメラ
    private Rigidbody rb; // リジッドボディ

    void Start()
    {
        // カメラのコンポーネントを取得
        cam = GetComponent<Camera>();

        // 速度を参照するオブジェクトのrigidbodyを取得
        rb = player.GetComponent<Rigidbody>();
    }

    // 視野角を移動速度に応じて変更
    public void ChangeFOV()
    {
        // 視野角を滑らかに変更
        cam.fieldOfView += (Fov + rb.velocity.magnitude - cam.fieldOfView) * Time.deltaTime;

        // 視野角がFov+30度以上なら
        if (cam.fieldOfView >= Fov + 30)
        {
            // 視野角をFov+30度にする
            cam.fieldOfView = Fov + 30;
        }

        // 視野角がFov-10度以下なら
        else if (cam.fieldOfView <= Fov - 10)
        {
            // 視野角をFov-10度にする
            cam.fieldOfView = Fov - 10;
        }
    }

    // 視野角を初期値にリセット
    public void ResetFOV()
    {
        cam.fieldOfView = Fov;
    }
}
