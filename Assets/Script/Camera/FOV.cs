using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AppParam;

// 視野角を変更する
public class FOV : Singleton<FOV>
{
    [SerializeField] private GameObject player; // 速度を参照するオブジェクト

    private Rigidbody rb; // リジッドボディ

    void Start()
    {
        // 速度を参照するオブジェクトのrigidbodyを取得
        rb = player.GetComponent<Rigidbody>();
    }

    // 視野角を移動速度に応じて変更
    public void ChangeFOV()
    {
        // 視野角を滑らかに変更
        Camera.main.fieldOfView += ((float)Param_Camera.fov.Value + rb.velocity.magnitude - Camera.main.fieldOfView) * Time.deltaTime;

        // 視野角を正常な範囲に保つ
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, Param_Camera.fov.Min, Param_Camera.fov.Max);
    }

    // 視野角を初期値にリセット
    public void ResetFOV()
    {
        Camera.main.fieldOfView = Param_Camera.fov.Value;
    }
}
