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
        public RectTransform contentParenRect;  // �X�N���[�������R���e���c�̐e�I�u�W�F�N�g��RectTransform
        public int focusScreen;                 // �X�N���[���o�[���t�H�[�J�X�����ʔԍ�
        public GameObject contentObj;           // �X�N���[������v�f�̐e�I�u�W�F�N�g
        public int amount;                      // �X�N���[������v�f�̐�
    }

    public int num = 0;

    public ScrollBarStruct[] scrollBarStruct;

    void Start()
    {
        // ��ʑJ�ڎ��ɃX�N���[���o�[�̃t�H�[�J�X��ݒ肷��
        screenController.changeScreen += Focus;

        // �X�N���[������v�f�̐����擾���Đݒ�
        SetAmount();
    }

    // �X�N���[������v�f�̐����擾���Đݒ�
    void SetAmount()
    {
        for (int i = 0; i < scrollBarStruct.Length; ++i)
        {
            scrollBarStruct[i].amount = scrollBarStruct[i].contentObj.transform.childCount;
        }
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
        // �X�N���[������v�f������
        int amount = scrollBarStruct[num].amount;

        // ��ɃX�N���[������Ȃ�
        if(up)
        {
            // �o�[����ɓ�����
            sBar.value += (1.0f / amount / value);
        }

        // ���ɃX�N���[������Ȃ�
        else
        {
            // �o�[�����ɓ�����
            sBar.value -= (1.0f / amount / value);
        }
    }
}
