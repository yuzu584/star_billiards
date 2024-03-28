using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �^�C�g����ʂ��Ǘ�
public class TitleController : MonoBehaviour
{
    private ScreenController scrCon;
    private InputController input;

    void Start()
    {
        scrCon = ScreenController.instance;
        input = InputController.instance;
    }

    void Update()
    {
        // �^�C�g����ʂŉ�������̓��͂��s��ꂽ�烁�C�����j���[�ɑJ��
        if ((Input.anyKey) && (scrCon.Screen == ScreenController.ScreenType.Title) && (input.canInput))
        {
            scrCon.Screen = ScreenController.ScreenType.MainMenu;
        }
    }
}
