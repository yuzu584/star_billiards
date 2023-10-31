using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// �|�X�g�v���Z�X���Ǘ�
public class PostProcessController : MonoBehaviour
{
    [SerializeField] Volume volume; // �|�X�g�v���Z�X�̃{�����[��
    DepthOfField depthOfField;      // ��ʊE�[�x

    // ��ʊE�[�x��ON����OFF�ɂ���
    public void DepthOfFieldOnOff(bool onoff)
    {
        if (volume.profile.TryGet<DepthOfField>(out depthOfField))
        {
            depthOfField.active = onoff;
        }
    }
}
