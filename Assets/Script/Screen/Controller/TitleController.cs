using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �^�C�g����ʂ��Ǘ�
public class TitleController : MonoBehaviour
{
    private ScreenController scrCon;
    private InputController input;
    private KeyGuideUI keyGuideUI;
    private Focus focus;

    private static bool isFirstDraw = true;

    void Start()
    {
        scrCon = ScreenController.instance;
        input = InputController.instance;
        keyGuideUI = KeyGuideUI.instance;
        focus = Focus.instance;

        // �ŏ��̃^�C�g���`�掞�̂ݓ��͂̐ݒ���s��
        if (isFirstDraw)
        {
            isFirstDraw = false;
            input.SetInputs();
        }

        // �L�[����K�C�h���폜
        keyGuideUI.DestroyGuide();
    }
}
