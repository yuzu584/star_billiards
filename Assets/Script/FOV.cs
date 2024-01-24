using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 視野角を変更する
public class FOV : MonoBehaviour
{
    [SerializeField] private GameObject player; // 速度を参照するオブジェクト
    [SerializeField] private int Fov = 60;      // 視野角
    [SerializeField] private int maxFov = 90;   // 最大視野角
    [SerializeField] private int minFov = 60;   // 最小視野角

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
        Camera.main.fieldOfView += (Fov + rb.velocity.magnitude - Camera.main.fieldOfView) * Time.deltaTime;

        // 視野角を正常な範囲に保つ
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minFov, maxFov);
    }

    // 視野角を初期値にリセット
    public void ResetFOV()
    {
        Camera.main.fieldOfView = Fov;
    }
}
