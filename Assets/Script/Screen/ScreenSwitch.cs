using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ʑJ�ڎ��̏���
public class ScreenSwitch : Singleton<ScreenSwitch>
{
    void Update()
    {
        // �O��̃t���[���ƌ��݂̃t���[���ŉ�ʔԍ����قȂ�����
        if (ScreenController.instance.ScreenNum != ScreenController.instance.oldFrameScreenNum)
        {
            // �O��̉�ʔԍ���ۑ�
            ScreenController.instance.oldScreenNum = ScreenController.instance.oldFrameScreenNum;

            // 1�t���[���O�̉�ʔԍ��Ɍ��݂̉�ʔԍ�����
            ScreenController.instance.oldFrameScreenNum = ScreenController.instance.ScreenNum;

            // ��ʑJ�ڂ����Ƃ��̏���
            if (ScreenController.instance.changeScreen != null)
                ScreenController.instance.changeScreen();
        }

        // �O��̃t���[���ƌ��݂̃t���[���ŊK�w���قȂ�����
        if (ScreenController.instance.ScreenLoot != ScreenController.instance.oldFrameScreenLoot)
        {
            // �O��̊K�w��ۑ�
            ScreenController.instance.oldScreenLoot = ScreenController.instance.oldFrameScreenLoot;

            // 1�t���[���O�̊K�w�Ɍ��݂̊K�w����
            ScreenController.instance.oldFrameScreenLoot = ScreenController.instance.ScreenLoot;

            // �K�w���J�ڂ����Ƃ��̏���
            if (ScreenController.instance.changeLoot != null)
                ScreenController.instance.changeLoot();
        }
    }
}
