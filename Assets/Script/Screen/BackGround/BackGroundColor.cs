using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ”wŒiF‚ğŠÇ—
public class BackGroundColor : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Material material;
    [SerializeField] private Transform parentT;
    private BackGroundEffect bgEffect;

    // F‚ğƒ‰ƒ“ƒ_ƒ€‚Åİ’è
    private void Start()
    {
        bgEffect = BackGroundEffect.instance;

        // ”wŒi‚ÌƒGƒtƒFƒNƒg‚ğ¶¬
        bgEffect.parentT = parentT;
        bgEffect.DrawEffect();

        // ”wŒi‚Ì ButtomColor ‚ğİ’è
        material.SetColor("_TopColor", bgEffect.mainColor);
        material.SetColor("_ButtomColor", bgEffect.buttomColor);
    }
}
