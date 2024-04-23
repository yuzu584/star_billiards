using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Q�[�����Ŏg�p����ϐ����Ǘ�
public class AppParams : Singleton<AppParams>
{
    // ClampedValue �N���X�ŊǗ����Ă���l���܂Ƃ߂�W�F�l���b�N�� Dictionary
    public Dictionary<string, IClampedValue> paramsDic = new Dictionary<string, IClampedValue>();

    // ClampedValue<T> ��ێ�����C���^�[�t�F�[�X
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

    // �w��� key �� Dictionary �̗v�f���擾
    public IClampedValue GetClampedValue(string key)
    {
        // Key �� Dictionary ��������
        // �q�b�g����΃q�b�g���� IClampedValue ��Ԃ�
        if (instance.paramsDic.ContainsKey(key))
            return paramsDic[key];
        // �q�b�g���Ȃ���� null ��Ԃ�
        else return null;
    }
}
