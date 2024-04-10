using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム内で使用する変数を管理
public class AppParams : Singleton<AppParams>
{
    // ClampedValue クラスで管理している値をまとめる]
    public Dictionary<string, ClampedValue<int>> paramsDic_int = new Dictionary<string, ClampedValue<int>>();
    public Dictionary<string, ClampedValue<float>> paramsDic_float = new Dictionary<string, ClampedValue<float>>();
}
