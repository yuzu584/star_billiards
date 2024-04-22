using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Q[ÌUIðÇ
public class InGameUI : MonoBehaviour
{
    public KeyGuide.KeyGuideIconAndTextType[] keyGuideTypes;

    private KeyGuideUI keyGuideUI;

    private void Start()
    {
        keyGuideUI = KeyGuideUI.instance;

        // L[ìKChUIð`æ
        keyGuideUI.DrawGuide(keyGuideTypes);
    }
}
