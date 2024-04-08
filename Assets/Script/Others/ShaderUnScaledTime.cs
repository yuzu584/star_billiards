using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ShaderGraph �� unscaledTime ���g�p�\�ɂ���
public class ShaderUnScaledTime : MonoBehaviour
{
    void Update()
    {
        // _UnScaleTime �� ShaderGraph �Őݒ肵�� Reference �ɂ���
        Shader.SetGlobalFloat("_UnScaleTime", Time.unscaledTime);
    }
}
