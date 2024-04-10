using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 値を保持するクラス(値を範囲内に収める機能付き)
public class ClampedValue<T> where T : struct, IComparable<T>
{
    private T defaultValue;
    private T defaultMax;
    private T defaultMin;

    // 値
    private T _value;
    public T Value
    {
        get { return _value; }
        set
        {
            _value = value;
            ClampValue();
        }
    }

    // 最大値
    private T _max;
    public T Max
    {
        get { return _max; }
        set
        {
            _max = value;
            ClampValue();
        }
    }

    // 最小値
    private T _min;
    public T Min
    {
        get { return _min; }
        set
        {
            _min = value;
            ClampValue();
        }
    }

    // コンストラクタ
    public ClampedValue(T value, T max, T min)
    {
        _value = value;
        _max = max;
        _min = min;

        defaultValue = value;
        defaultMax = max;
        defaultMin = min;

        ClampValue();
    }

    // 値を範囲内に収める
    private void ClampValue()
    {
        // 値が最小値以下なら
        if (_value.CompareTo(_min) < 0)
            _value = _min;
        // 値が最大値以上なら
        else if (_value.CompareTo(_max) > 0)
            _value = _max;
    }

    // 値・最大値・最小値を初期値に戻す
    public void Init()
    {
        _value = defaultValue;
        _max = defaultMax;
        _min = defaultMin;

        ClampValue();
    }
}