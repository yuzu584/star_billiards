using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ShaderGraph Ç≈ unscaledTime Çégópâ¬î\Ç…Ç∑ÇÈ
public class ShaderUnScaledTime : MonoBehaviour
{
    void Update()
    {
        // _UnScaleTime ÇÕ ShaderGraph Ç≈ê›íËÇµÇΩ Reference Ç…Ç∑ÇÈ
        Shader.SetGlobalFloat("_UnScaleTime", Time.unscaledTime);
    }
}
