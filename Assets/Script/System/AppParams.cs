using System;
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
        int GetValue_Int();
        float GetValue_Float();
        void SetValue(object value);
        object GetMax();
        int GetMax_Int();
        float GetMax_Float();
        void SetMax(object max);
        object GetMin();
        int GetMin_Int();
        float GetMin_Float();
        void SetMin(object min);
        string GetName();
        void SetName(string name);
        Type GetThisType();
        void SetOnValueChanged(Action action);
        void SetOnMaxChanged(Action action);
        void SetOnMinChanged(Action action);
    }

    // 指定の key の Dictionary の要素を取得
    public IClampedValue GetClampedValue(string key)
    {
        // Key で Dictionary 内を検索
        // ヒットすればヒットした IClampedValue を返す
        if (instance.paramsDic.ContainsKey(key))
            return paramsDic[key];
        // ヒットしなければ null を返す
        else return null;
    }
}
