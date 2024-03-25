using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����GameObject�ɃA�^�b�`����Ǝw�肵����ʂ̎��̂ݕ\�������悤�ɂȂ�
public class Screen : MonoBehaviour
{
    [SerializeField] private ScreenController.ScreenType num = 0;   // ����GameObject��\��������
    [SerializeField] private int loot = 0;                          // ����GameObject��\�������ʂ̊K�w
    [SerializeField] private bool resetFov = false;                 // �X�N���[���A�N�e�B�u���Ɏ���p�������l�ɂ��邩

    private ScreenController scrCon;

    // �\�������ʂ�؂�ւ�
    public void SwitchScreen()
    {
        // ��ʂ�num�Ɠ�������ʂ̊K�w��loot�Ɠ����Ȃ�\��
        if((scrCon.Screen == num) && (scrCon.ScreenLoot == loot) && (!gameObject.activeSelf))
        {
            gameObject.SetActive(true);

            if(resetFov)
            {
                // ����p�������l�ɂ���
                FOV.instance.ResetFOV();
            }
        }
        // �Ⴄ�Ȃ��\��
        else if (((scrCon.Screen != num) || (scrCon.ScreenLoot != loot)) && (gameObject.activeSelf))
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        scrCon = ScreenController.instance;
        scrCon.changeScreen += SwitchScreen;
        SwitchScreen();
    }
}
