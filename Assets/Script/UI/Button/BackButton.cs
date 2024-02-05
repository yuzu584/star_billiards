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

    private int oldScreen = 1;                          // �O��̃X�N���[��(�߂��̉��)
    private Color startColor = new Color(0, 0, 0, 0);   // �ω��O�̐F
    private Color endColor = new Color (0, 0, 0, 0.1f); // �ω���̐F

    // �}�E�X�|�C���^�[����������̏���
    public override void EnterProcess()
    {
        // �{�^���̃A�j���[�V����
        StopAllCoroutines();
        lerp.Color_Image(backGround, startColor, endColor, 0.2f);
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    public override void ExitProcess()
    {
        // �{�^���̃A�j���[�V����
        StopAllCoroutines();
        lerp.Color_Image(backGround, endColor, startColor, 0.2f);
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    public override void ClickProcess()
    {
        // ��ʔԍ���O�̉�ʂɂ���
        screenController.screenNum = oldScreen;
    }
}
