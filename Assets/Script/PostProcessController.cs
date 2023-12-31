using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// |XgvZXπΗ
public class PostProcessController : MonoBehaviour
{
    [SerializeField] Volume volume; // |XgvZXΜ{[
    DepthOfField depthOfField;      // νΚE[x

    // νΚE[xπONΝOFFΙ·ι
    public void DepthOfFieldOnOff(bool onoff)
    {
        if (volume.profile.TryGet<DepthOfField>(out depthOfField))
        {
            depthOfField.active = onoff;
        }
    }
}
