using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ݒ��ʂ�UI���Ǘ�
public class OptionsUIController : MonoBehaviour
{
    public GameObject[] lootObj;    // �K�w���Ƃ̃Q�[���I�u�W�F�N�g

    private OptionsController opCon;

    private void Start()
    {
        opCon.opUICon = this;
    }

    private void OnDestroy()
    {
        opCon.opUICon = null;
    }

    // �\������K�w��؂�ւ�
    private void SwitchLoot()
    {
        // �K�w���Ƃ̃I�u�W�F�N�g�̐��J��Ԃ�
        for (int i = 0; i < lootObj.Length; ++i)
        {
            // �\������K�w�Ȃ�\��
            if (i == (int)opCon.loot)
                lootObj[i].SetActive(true);
            // ��\��
            else
                lootObj[i].SetActive(false);
        }
    }

    void OnEnable()
    {
        opCon = OptionsController.instance;

        // �K�w����ԏ��
        opCon.loot = OptionsController.Loot.Top;

        // �ŏ��� Top ��\��
        SwitchLoot();
    }

    void Update()
    {
        // �K�w���ς�������ʂ�؂�ւ���
        if (opCon.oldLoot != opCon.loot)
        {
            opCon.oldLoot = opCon.loot;
            SwitchLoot();
        }
    }
}
