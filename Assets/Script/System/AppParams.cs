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
        void SetValue(object value);
        object GetMax();
        void SetMax(object max);
        object GetMin();
        void SetMin(object min);
        string GetName();
        void SetName(string name);
    }
}
