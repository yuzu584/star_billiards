using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �^�C�g����ʂ��Ǘ�
public class TitleController : MonoBehaviour
{
    private InputController input;
    private KeyGuideUI keyGuideUI;

    private static bool isFirstDraw = true;

    void Start()
    {
        input = InputController.instance;
        keyGuideUI = KeyGuideUI.instance;

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
