using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// İ’è‰æ–Ê‚ÌUI‚ğŠÇ—
public class OptionsUIController : MonoBehaviour
{
    private OptionsController optCon;

    private void Start()
    {
        optCon ??= OptionsController.instance;

        SwitchLoot();
    }

    // •\¦‚·‚éŠK‘w‚ğØ‚è‘Ö‚¦
    private void SwitchLoot()
    {
        for (int i = 0; i < optCon.lootObj.Length; ++i)
        {
            if (i == (int)optCon.loot)
                optCon.lootObj[i].SetActive(true);
            else
                optCon.lootObj[i].SetActive(false);
        }
    }

    void OnEnable()
    {
        optCon ??= OptionsController.instance;

        // ŠK‘w‚ğˆê”Ôã‚É
        optCon.loot = OptionsController.Loot.Top;
    }

    void Update()
    {
        // ŠK‘w‚ª•Ï‚í‚Á‚½‚ç‰æ–Ê‚ğØ‚è‘Ö‚¦‚é
        if (optCon.oldLoot != (int)optCon.loot)
        {
            optCon.oldLoot = (int)optCon.loot;
            SwitchLoot();
        }
    }
}
