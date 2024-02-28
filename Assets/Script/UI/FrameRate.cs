using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// フレームレートを管理
public class FrameRate : MonoBehaviour
{
    [SerializeField] private Text FPSText; // フレームレートを表すテキスト

    private int frameCount = 0;            // 経過したフレームを数える
    private float prevTime = 0.0f;         // 計測開始した時間を保存
    private float waitTime = 0.2f;         // フレームレートの描画間隔

    void Update()
    {
        // 経過したフレームを数える
        ++frameCount;

        // 経過時間を計算する
        float time = Time.realtimeSinceStartup - prevTime;

        // 経過時間が一定時間以上ならFPSの値を更新
        if (time >= waitTime)
        {
            FPSText.text = (frameCount / time).ToString("0") + " FPS";

            // 数値を初期化
            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }
}
