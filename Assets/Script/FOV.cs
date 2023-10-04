using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 視野角を変更する
public class FOV : MonoBehaviour
{
    private Camera cam;        // メインカメラ
    private Rigidbody rb;      // リジッドボディ
    public GameObject player;  // 速度を参照するオブジェクト
    public int Fov = 60;       // 視野角

    void Start()
    {
        // カメラのコンポーネントを取得
        cam = GetComponent<Camera>();

        // 速度を参照するオブジェクトのrigidbodyを取得
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 視野角を滑らかに変更
        cam.fieldOfView += (Fov + rb.velocity.magnitude - cam.fieldOfView) * Time.deltaTime;

        // 視野角がFov+30度以上なら
        if(cam.fieldOfView >= Fov + 30)
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
}
