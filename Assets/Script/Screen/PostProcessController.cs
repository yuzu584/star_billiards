using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// ポストプロセスを管理
public class PostProcessController : MonoBehaviour
{
    [SerializeField] Volume volume;    // ポストプロセスのボリューム
    private DepthOfField depthOfField; // 被写界深度

    // 被写界深度をON又はOFFにする
    public void DepthOfFieldOnOff(bool onoff)
    {
        if (volume.profile.TryGet<DepthOfField>(out depthOfField))
            depthOfField.active = onoff;
    }

    // 被写界深度のON/OFFを取得
    public bool GetDepthOfFieldOnOff()
    {
        if (volume.profile.TryGet<DepthOfField>(out depthOfField))
            return depthOfField.active;

        return false;
    }

    void Start()
    {
        // 被写界深度をOFF
        DepthOfFieldOnOff(false);
    }
}
