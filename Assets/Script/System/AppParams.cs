using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Q�[�����Ŏg�p����ϐ����Ǘ�
public class AppParams : Singleton<AppParams>
{
    // ClampedValue �N���X�ŊǗ����Ă���l���܂Ƃ߂�]
    public Dictionary<string, ClampedValue<int>> paramsDic_int = new Dictionary<string, ClampedValue<int>>();
    public Dictionary<string, ClampedValue<float>> paramsDic_float = new Dictionary<string, ClampedValue<float>>();
}
