using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム内で変更可能な値をまとめた名前空間
namespace AppParam
{
    public static class Param_Player
    {
        public static FunctionalValue<int> energy = new FunctionalValue<int>(1000, 1000, 0);                        // エネルギー
    }

    public static class Param_Camera
    {
        public static FunctionalValue<int> fov = new FunctionalValue<int>(60, 90, 60);                              // カメラの視野角

        public static FunctionalValue<float> angleMoveSpeed = new FunctionalValue<float>(1.0f, 100.0f, 0.01f);      // カメラの視点移動速度
    }
}
