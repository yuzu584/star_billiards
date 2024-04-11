using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AppParams;

// �l��ێ�����N���X(�l��͈͓��Ɏ��߂�@�\�t��)
public class ClampedValue<T> : IClampedValue where T : struct, IComparable<T>
{
    private T _value;       // �l
    private T _max;         // �ő�l
    private T _min;         // �ŏ��l
    private string _name;   // ���O(Dictionary��Key�Ɏg�p)

    // �R���X�g���N�^
    public ClampedValue(T value, T max, T min, string name)
    {
        _value = value;
        _max = max;
        _min = min;
        _name = name;
        AddDictionary();
        ClampValue();
    }

    // �l��͈͓��Ɏ��߂�
    private void ClampValue()
    {
        if (_value.CompareTo(_min) < 0)
            _value = _min;
        else if (_value.CompareTo(_max) > 0)
            _value = _max;
    }

    // �l���擾
    public object GetValue() { return _value; }
    public int GetValue_Int() { return Convert.ToInt32(_value); }
    public float GetValue_Float() { return Convert.ToSingle(_value); }

    // �l��ݒ�
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

    // �ő�l���擾
    public object GetMax() { return _max; }
    public int GetMax_Int() { return Convert.ToInt32(_max); }
    public float GetMax_Float() { return Convert.ToSingle(_max); }

    // �ő�l��ݒ�
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

    // �ŏ��l���擾
    public object GetMin() {  return _min; }
    public int GetMin_Int() { return Convert.ToInt32(_min); }
    public float GetMin_Float() { return Convert.ToSingle(_min); }

    // �ŏ��l��ݒ�
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

    // ���O���擾
    public string GetName()
    {
        return _name;
    }

    // ���O��ݒ�
    public void SetName(string name)
    {
        AddDictionary();
        _name = name;
    }

    public Type GetThisType() { return typeof(T); }

    // Dictionary �ɒǉ�
    private void AddDictionary()
    {
        // ���������ɓo�^�ς݂Ȃ�폜
        if (AppParams.instance.paramsDic.ContainsKey(_name))
            AppParams.instance.paramsDic.Remove(_name);

        // ������o�^
        AppParams.instance.paramsDic.Add(_name, this);
    }
}