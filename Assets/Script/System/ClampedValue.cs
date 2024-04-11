using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AppParams;

// 値を保持するクラス(値を範囲内に収める機能付き)
public class ClampedValue<T> : IClampedValue where T : struct, IComparable<T>
{
    private T _value;       // 値
    private T _max;         // 最大値
    private T _min;         // 最小値
    private string _name;   // 名前(DictionaryのKeyに使用)

    // コンストラクタ
    public ClampedValue(T value, T max, T min, string name)
    {
        _value = value;
        _max = max;
        _min = min;
        _name = name;
        AddDictionary();
        ClampValue();
    }

    // 値を範囲内に収める
    private void ClampValue()
    {
        if (_value.CompareTo(_min) < 0)
            _value = _min;
        else if (_value.CompareTo(_max) > 0)
            _value = _max;
    }

    // 値を取得
    public object GetValue() { return _value; }
    public int GetValue_Int() { return Convert.ToInt32(_value); }
    public float GetValue_Float() { return Convert.ToSingle(_value); }

    // 値を設定
    public void SetValue(object value)
    {
        if (value is T)
        {
            _value = (T)value;
            ClampValue();
        }
        else
        {
            throw new ArgumentException("Value is not of type " + typeof(T).Name);
        }
    }

    // 最大値を取得
    public object GetMax() { return _max; }
    public int GetMax_Int() { return Convert.ToInt32(_max); }
    public float GetMax_Float() { return Convert.ToSingle(_max); }

    // 最大値を設定
    public void SetMax(object max)
    {
        if (max is T)
        {
            _max = (T)max;
            ClampValue();
        }
        else
        {
            throw new ArgumentException("Value is not of type " + typeof(T).Name);
        }
    }

    // 最小値を取得
    public object GetMin() {  return _min; }
    public int GetMin_Int() { return Convert.ToInt32(_min); }
    public float GetMin_Float() { return Convert.ToSingle(_min); }

    // 最小値を設定
    public void SetMin(object min)
    {
        if (min is T)
        {
            _min = (T)min;
            ClampValue();
        }
        else
        {
            throw new ArgumentException("Value is not of type " + typeof(T).Name);
        }
    }

    // 名前を取得
    public string GetName()
    {
        return _name;
    }

    // 名前を設定
    public void SetName(string name)
    {
        AddDictionary();
        _name = name;
    }

    public Type GetThisType() { return typeof(T); }

    // Dictionary に追加
    private void AddDictionary()
    {
        // 自分が既に登録済みなら削除
        if (AppParams.instance.paramsDic.ContainsKey(_name))
            AppParams.instance.paramsDic.Remove(_name);

        // 自分を登録
        AppParams.instance.paramsDic.Add(_name, this);
    }
}