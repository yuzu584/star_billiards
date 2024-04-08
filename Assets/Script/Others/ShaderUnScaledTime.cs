using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ShaderGraph で unscaledTime を使用可能にする
public class ShaderUnScaledTime : MonoBehaviour
{
    void Update()
    {
        // _UnScaleTime は ShaderGraph で設定した Reference にする
        Shader.SetGlobalFloat("_UnScaleTime", Time.unscaledTime);
    }
}
