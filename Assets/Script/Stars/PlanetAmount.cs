using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ˜f¯‚Ì—Ê‚ğŠÇ—
public class PlanetAmount : MonoBehaviour
{
    [SerializeField] private Initialize initialize; // Inspector‚ÅInitialize‚ğw’è

    public int planetDestroyAmount = 0; // ˜f¯‚ğ”j‰ó‚µ‚½”

    // ‰Šú‰»ˆ—
    void Init()
    {
        planetDestroyAmount = 0;
    }

    void Start()
    {
        // ƒfƒŠƒQ[ƒg‚É‰Šú‰»ŠÖ”‚ğ“o˜^
        initialize.init_Stage += Init;
    }
}
