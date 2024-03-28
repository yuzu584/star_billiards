using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ݒ��ʂ�UI���Ǘ�
public class OptionsUIController : MonoBehaviour
{
    private OptionsController optCon;

    private void Start()
    {
        optCon ??= OptionsController.instance;

        SwitchLoot();
    }

    // �\������K�w��؂�ւ�
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

        // �K�w����ԏ��
        optCon.loot = OptionsController.Loot.Top;
    }

    void Update()
    {
        // �K�w���ς�������ʂ�؂�ւ���
        if (optCon.oldLoot != (int)optCon.loot)
        {
            optCon.oldLoot = (int)optCon.loot;
            SwitchLoot();
        }
    }
}
