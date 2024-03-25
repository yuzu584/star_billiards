using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ʑJ�ڎ��̏���
public class ScreenSwitch : Singleton<ScreenSwitch>
{
    private ScreenController scrCon;

    void Start()
    {
        scrCon = ScreenController.instance;
    }

    void Update()
    {
        // �O��̃t���[���ƌ��݂̃t���[���ŉ�ʂ��قȂ�����
        if (scrCon.Screen != scrCon.oldFrameScreen)
        {
            // �O��̉�ʂ�ۑ�
            scrCon.oldScreen = scrCon.oldFrameScreen;

            // 1�t���[���O�̉�ʂɌ��݂̉�ʂ���
            scrCon.oldFrameScreen = scrCon.Screen;

            // ��ʑJ�ڂ����Ƃ��̏���
            if (scrCon.changeScreen != null)
                scrCon.changeScreen();
        }

        // �O��̃t���[���ƌ��݂̃t���[���ŊK�w���قȂ�����
        if (scrCon.ScreenLoot != scrCon.oldFrameScreenLoot)
        {
            // �O��̊K�w��ۑ�
            scrCon.oldScreenLoot = scrCon.oldFrameScreenLoot;

            // 1�t���[���O�̊K�w�Ɍ��݂̊K�w����
            scrCon.oldFrameScreenLoot = scrCon.ScreenLoot;

            // �K�w���J�ڂ����Ƃ��̏���
            if (scrCon.changeLoot != null)
                scrCon.changeLoot();
        }
    }
}
