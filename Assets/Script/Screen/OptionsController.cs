using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

// �ݒ��ʂ��Ǘ�
public class OptionsController : MonoBehaviour
{
    public enum Loot   // �K�w
    {
        Top = 0,       // �ŏ��̉��
        GamePlay = 1,  // �Q�[�����ݒ�
        Video = 2,     // �r�f�I�ݒ�
        Audio = 3,     // �I�[�f�B�I�ݒ�
        KeyConfig = 4, // �L�[�z�u�ݒ�
        Language = 5,  // ����ݒ�
    }
    public Loot loot = 0;

    private int oldLoot = 0;  // 1�t���[���O�̊K�w

    [SerializeField] private OptionsUIController _optionsUIController;
    [SerializeField] private FOV fov;
    [SerializeField] private GameObject[] lootObj; // �K�w���Ƃ̃Q�[���I�u�W�F�N�g


    // �\������K�w��؂�ւ�
    private void SwitchLoot()
    {
        for(int i = 0; i < lootObj.Length; ++i)
        {
            if(i == (int)loot)
                lootObj[i].SetActive(true);
            else
                lootObj[i].SetActive(false);
        }
    }

    void OnEnable()
    {
        loot = Loot.Top;
        fov.ResetFOV();
    }

    void Update()
    {
        // �K�w���ς�������ʂ�؂�ւ���
        if(oldLoot != (int)loot)
        {
            oldLoot = (int)loot;
            SwitchLoot();
        }
    }
}
