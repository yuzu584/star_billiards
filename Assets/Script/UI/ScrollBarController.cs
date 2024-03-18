using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �X�N���[���o�[�̋������Ǘ�(��ɃR���g���[���[�g�p����)
public class ScrollBarController : Singleton<ScrollBarController>
{
    [SerializeField] ScreenController screenController;

    [System.Serializable]
    public struct ScrollBarStruct
    {
        public Scrollbar scrollbar;             // �X�N���[���o�[
        public RectTransform parentRect;        // �e�I�u�W�F�N�g��RectTransform
        public RectTransform contentParentRect; // �X�N���[�������R���e���c�̐e�I�u�W�F�N�g��RectTransform
        public int focusScreen;                 // �X�N���[���o�[���t�H�[�J�X�����ʔԍ�
        public Button.Group group;              // �X�N���[������{�^���̃O���[�v
    }

    public int num = 0;

    public ScrollBarStruct[] scrollBarStruct;

    void Start()
    {
        // ��ʑJ�ڎ��ɃX�N���[���o�[�̃t�H�[�J�X��ݒ肷��
        screenController.changeScreen += Focus;
    }

    // �X�N���[���o�[�̃t�H�[�J�X����
    void Focus()
    {
        // �t�H�[�J�X����X�N���[���o�[��T��
        for(int i = 0; i < scrollBarStruct.Length; ++i)
        {
            // ��ʔԍ��ƃX�N���[���o�[���t�H�[�J�X�����ʔԍ�����v������
            if (scrollBarStruct[i].focusScreen == screenController.ScreenNum)
            {
                // �t�H�[�J�X����X�N���[���o�[��ݒ�
                num = i;
                screenController.focusScrollbar = scrollBarStruct[i].scrollbar;
                return;
            }
        }

        // �t�H�[�J�X����X�N���[���o�[��������΃t�H�[�J�X���O��
        screenController.focusScrollbar = null;
    }

    // �X�N���[������
    public void Scroll(Scrollbar sBar, bool up, float value)
    {
        // ��ɃX�N���[������Ȃ�
        if(up)
        {
            // �o�[����ɓ�����
            sBar.value += value * 3.0f;
        }

        // ���ɃX�N���[������Ȃ�
        else
        {
            // �o�[�����ɓ�����
            sBar.value -= value * 3.0f;
        }
    }
}
