using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 視野角を変更する
public class FOV : Singleton<FOV>
{
    [SerializeField] private GameObject player;     // 速度を参照するオブジェクト

    private ClampedValue<int> fov;                  // カメラの視野角
    private Rigidbody rb;                           // リジッドボディ

    void Start()
    {
        fov = new ClampedValue<int>(60, 120, 10, nameof(fov));

        // 速度を参照するオブジェクトのrigidbodyを取得
        rb = player.GetComponent<Rigidbody>();
    }

    // 視野角を移動速度に応じて変更
    public void ChangeFOV()
    {
        // 視野角を滑らかに変更
        Camera.main.fieldOfView += (fov.GetValue_Float() + rb.velocity.magnitude - Camera.main.fieldOfView) * Time.deltaTime;

        // 視野角を正常な範囲に保つ
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, fov.GetValue_Float() - 10.0f, fov.GetValue_Float() + 30.0f);
    }

    // 視野角を初期値にリセット
    public void ResetFOV()
    {
        Camera.main.fieldOfView = 60;
    }
}
