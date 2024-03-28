using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �{�^���̗v�f��ݒ肷��
public class ButtonContentSetter : MonoBehaviour
{
    [System.Serializable]
    public struct Buttons
    {
        public Button[] btnNum;
    }
    
    [SerializeField] private Buttons[] btn;             // ���̔z����� Button �� btnNum ��A�ԂŐݒ�
    [SerializeField] private Buttons[] dontFocusBtn;    // ���̔z����� Button �� btnNum �� -1 �ɐݒ�(�t�H�[�J�X����Ȃ�����)

    private void Awake()
    {
        SetBtnNum();
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
            if (btn[i].btnNum.Length <= 0) continue;

            for (int j = 0; j < btn[i].btnNum.Length; j++)
            {
                btn[i].btnNum[j].btnNum = j;
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
            if (dontFocusBtn[i].btnNum.Length <= 0) continue;

            for (int j = 0; j < dontFocusBtn[i].btnNum.Length; j++)
            {
                dontFocusBtn[i].btnNum[j].btnNum = -1;
            }
        }
    }
}
