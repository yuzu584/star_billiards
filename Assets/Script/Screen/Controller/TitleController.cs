using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �^�C�g����ʂ��Ǘ�
public class TitleController : MonoBehaviour
{
    private ScreenController scrCon;
    private InputController input;
    private KeyGuideUI keyGuideUI;

    void Start()
    {
        scrCon = ScreenController.instance;
        input = InputController.instance;
        keyGuideUI = KeyGuideUI.instance;

        // �L�[����K�C�h���폜
        keyGuideUI.DestroyGuide();
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
