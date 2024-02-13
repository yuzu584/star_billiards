using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;

// �߂�{�^�����Ǘ�
public class BackButton : Button
{
    [System.Serializable]
    private struct ImageStruct // �摜�̍\����
    {
        public Image image;      // �摜
        public Color startColor; // �ω��O�̐F
        public Color endColor;   // �ω���̐F
        public float fadeTime;   // �t�F�[�h����
    }
    [SerializeField] private ImageStruct[] imageStructs;

    private int oldScreen = 0; // �O��̃X�N���[��(�߂��̉��)

    // �}�E�X�|�C���^�[����������̏���
    protected override void EnterProcess()
    {
        // �{�^���̃A�j���[�V����
        StopAllCoroutines();
        for(int i = 0; i < imageStructs.Length;  i++)
            StartCoroutine(lerp.Color_Image(imageStructs[i].image, imageStructs[i].startColor, imageStructs[i].endColor, imageStructs[i].fadeTime));
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    protected override void ExitProcess()
    {
        // �{�^���̃A�j���[�V����
        StopAllCoroutines();
        for (int i = 0; i < imageStructs.Length; i++)
            StartCoroutine(lerp.Color_Image(imageStructs[i].image, imageStructs[i].endColor, imageStructs[i].startColor, imageStructs[i].fadeTime));
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    protected override void ClickProcess()
    {
        // ��ʔԍ���O�̉�ʂɂ���
        screenController.screenNum = oldScreen;

        // �{�^���̐F�����Z�b�g
        for (int i = 0; i < imageStructs.Length; i++)
            imageStructs[i].image.color = imageStructs[i].startColor;
    }

    // �O��̃X�N���[���ԍ����Z�b�g
    void SetOldScreen()
    {
        oldScreen = screenController.oldScreenNum;
    }

    new void Start()
    {
        base.Start();

        // �f���Q�[�g��ǉ�
        screenController.changeScreen += SetOldScreen;
    }
}
