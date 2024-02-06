using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;

// �߂�{�^�����Ǘ�
public class BackButton : Button
{
    [SerializeField] private Image backGround;          // �w�i�摜
    [SerializeField] private Text text;                 // �e�L�X�g

    private int oldScreen = 0;                                    // �O��̃X�N���[��(�߂��̉��)
    private Color startColor = new Color(1.0f, 1.0f, 1.0f, 0.0f); // �ω��O�̐F
    private Color endColor = new Color (1.0f, 1.0f, 1.0f, 0.1f);  // �ω���̐F
    private Color nowColor;                                       // ���݂̐F
    private float fadeTime = 0.1f;                                // �t�F�[�h����

    // �}�E�X�|�C���^�[����������̏���
    protected override void EnterProcess()
    {
        // �{�^���̃A�j���[�V����
        nowColor = backGround.color;

        StopAllCoroutines();
        StartCoroutine(lerp.Color_Image(backGround, nowColor, endColor, fadeTime));
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    protected override void ExitProcess()
    {
        // �{�^���̃A�j���[�V����
        nowColor = backGround.color;

        StopAllCoroutines();
        StartCoroutine(lerp.Color_Image(backGround, nowColor, startColor, fadeTime));
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    protected override void ClickProcess()
    {
        // ��ʔԍ���O�̉�ʂɂ���
        screenController.screenNum = oldScreen;

        // �{�^���̐F�����Z�b�g
        backGround.color = startColor;
    }

    // �O��̃X�N���[���ԍ����Z�b�g
    void SetOldScreen()
    {
        oldScreen = screenController.oldScreenNum;
    }

    void Start()
    {
        base.Start();

        // �f���Q�[�g��ǉ�
        screenController.changeScreen += SetOldScreen;
    }
}
