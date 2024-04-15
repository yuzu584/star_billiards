using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �{�^���̗v�f��ݒ肷��
public class ButtonContentSetter : MonoBehaviour
{
    // ������������
    public enum HorizontalOrVertical
    {
        Horizontal,
        Vertical,
    }

    [System.Serializable]
    public struct Buttons
    {
        public HorizontalOrVertical horizontalOrVertical;   // �{�^���̕��т�������������
        public Button[] buttons;
    }
    
    [SerializeField] private Buttons[] btn;                 // ���̔z����� Button �� btnNum ��A�ԂŐݒ�
    [SerializeField] private Buttons[] dontFocusBtn;        // ���̔z����� Button �� btnNum �� -1 �ɐݒ�(�t�H�[�J�X����Ȃ�����)

    private void Awake()
    {
        SetBtnNum();
        SetFocusBtn();
        SetDontFocusBtnNum();
    }

    // btnNum ���ꊇ�ݒ�
    void SetBtnNum()
    {
        // �z��̒����� 0 �Ȃ�I��
        if (btn.Length <= 0) return;

        // �S�Ẵ{�^���ɏ��Ԃ� btnNum ����
        for (int i = 0; i < btn.Length; i++)
        {
            // �z��̒����� 0 �Ȃ�X�L�b�v
            if (btn[i].buttons.Length <= 0) continue;

            for (int j = 0; j < btn[i].buttons.Length; j++)
            {
                // btnNum ��A�ԂŐݒ�
                btn[i].buttons[j].btnNum = j;
            }
        }
    }

    // �t�H�[�J�X��̃{�^�����ꊇ�ݒ�
    void SetFocusBtn()
    {
        // �z��̒����� 0 �Ȃ�I��
        if (btn.Length <= 0) return;

        // �S�Ẵ{�^���̃t�H�[�J�X��̃{�^����ݒ�
        for (int i = 0; i < btn.Length; i++)
        {
            // �z��̒����� 0 �Ȃ�X�L�b�v
            if (btn[i].buttons.Length <= 0) continue;
            
            // �{�^���̌����������Ȃ�
            if (btn[i].horizontalOrVertical == HorizontalOrVertical.Horizontal)
            {
                // ���E�̃t�H�[�J�X��{�^����ݒ�
                for (int j = 0; j < btn[i].buttons.Length; j++)
                {
                    if (j > 0) btn[i].buttons[j].buttonLeft = btn[i].buttons[j - 1];
                    if(j < btn[i].buttons.Length - 1) btn[i].buttons[j].buttonRight = btn[i].buttons[j + 1];
                }
            }
            // �{�^���̌����������Ȃ�
            else if (btn[i].horizontalOrVertical == HorizontalOrVertical.Vertical)
            {
                // �㉺�̃t�H�[�J�X��{�^����ݒ�
                for (int j = 0; j < btn[i].buttons.Length; j++)
                {
                    if (j > 0) btn[i].buttons[j].buttonUp = btn[i].buttons[j - 1];
                    if (j < btn[i].buttons.Length - 1) btn[i].buttons[j].buttonDown = btn[i].buttons[j + 1];
                }
            }
        }
    }

    // �t�H�[�J�X���Ȃ��{�^���� btnNum ���ꊇ�ݒ�
    void SetDontFocusBtnNum()
    {
        // �z��̒����� 0 �Ȃ�I��
        if (dontFocusBtn.Length <= 0) return;

        // �S�Ẵ{�^���� btnNum �� -1 ����
        for (int i = 0; i < dontFocusBtn.Length; i++)
        {
            // �z��̒����� 0 �Ȃ�X�L�b�v
            if (dontFocusBtn[i].buttons.Length <= 0) continue;

            for (int j = 0; j < dontFocusBtn[i].buttons.Length; j++)
            {
                dontFocusBtn[i].buttons[j].btnNum = -1;
            }
        }
    }
}
