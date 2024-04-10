using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �l��ێ�����N���X(�l��͈͓��Ɏ��߂�@�\�t��)
public class FunctionalValue<T> where T : struct, IComparable<T>
{
    // �l
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

    // �ő�l
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

    // �ŏ��l
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

    // �R���X�g���N�^
    public FunctionalValue(T value, T max, T min)
    {
        _value = value;
        _max = max;
        _min = min;
        ClampValue();
    }

    // �l��͈͓��Ɏ��߂�
    private void ClampValue()
    {
        // �l���ŏ��l�ȉ��Ȃ�
        if (_value.CompareTo(_min) < 0)
            _value = _min;
        // �l���ő�l�ȏ�Ȃ�
        else if (_value.CompareTo(_max) > 0)
            _value = _max;
    }
}