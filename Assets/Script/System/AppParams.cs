using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム内で使用する変数を管理
public class AppParams : Singleton<AppParams>
{
    // ClampedValue クラスで管理している値をまとめるジェネリックな Dictionary
    public Dictionary<string, IClampedValue> paramsDic = new Dictionary<string, IClampedValue>();

    // ClampedValue<T> を保持するインターフェース
    public interface IClampedValue
    {
        object GetValue();
        void SetValue(object value);
        object GetMax();
        void SetMax(object max);
        object GetMin();
        void SetMin(object min);
        string GetName();
        void SetName(string name);
    }
}
