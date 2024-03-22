using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ˜f¯‚Ì—Ê‚ğŠÇ—
public class PlanetAmount : Singleton<PlanetAmount>
{
    private Initialize init;

    public int planetDestroyAmount = 0; // ˜f¯‚ğ”j‰ó‚µ‚½”

    // ‰Šú‰»ˆ—
    void Init()
    {
        planetDestroyAmount = 0;
    }

    void Start()
    {
        init = Initialize.instance;

        // ƒfƒŠƒQ[ƒg‚É‰Šú‰»ŠÖ”‚ğ“o˜^
        init.init_Stage += Init;
    }
}
