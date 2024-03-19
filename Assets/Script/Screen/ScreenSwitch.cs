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
        // �O��̃t���[���ƌ��݂̃t���[���ŉ�ʔԍ����قȂ�����
        if (scrCon.ScreenNum != scrCon.oldFrameScreenNum)
        {
            // �O��̉�ʔԍ���ۑ�
            scrCon.oldScreenNum = scrCon.oldFrameScreenNum;

            // 1�t���[���O�̉�ʔԍ��Ɍ��݂̉�ʔԍ�����
            scrCon.oldFrameScreenNum = scrCon.ScreenNum;

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
