using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

// �t�H�[�J�X�֌W�������Ǘ�����
[DefaultExecutionOrder(-100)]
public class Focus : Singleton<Focus>
{
    public Button focusBtn;                  // �t�H�[�J�X���Ă���{�^��
    public Button oldfocusBtn;               // �t�H�[�J�X����Ă����{�^��
    public Scrollbar focusScrollbar;         // �t�H�[�J�X���Ă���X�N���[���o�[

    private ScreenController scrCon;
    private InputController input;
    private Sound sound;

    void Start()
    {
        scrCon = ScreenController.instance;
        input = InputController.instance;
        sound = Sound.instance;

        input.ui_OnMoveDele += ChangeBtnFocus;
        input.ui_OnMoveDele += MoveSlider;

        // UI_Positive���͎��̃C�x���g��o�^
        input.ui_OnPositiveDele += (float value) =>
        {
            // �����Đ�
            StartCoroutine(sound.Play(focusBtn.ClickSound));

            // �{�^���N���b�N���̏���
            focusBtn.ClickProcess();
        };
    }

    // �t�H�[�J�X����{�^����ς���
    void ChangeBtnFocus(Vector2 mVec)
    {
        float minInput = 0.5f; // ���͂��󂯕t����Œ�l

        if (focusBtn != null)
        {
            if ((mVec.x > minInput) && (focusBtn.buttonRight != null))
            {
                SetFocusBtn(focusBtn.buttonRight);
            }
            else if ((mVec.x < -minInput) && (focusBtn.buttonLeft != null))
            {
                SetFocusBtn(focusBtn.buttonLeft);
            }
            else if ((mVec.y < -minInput) && (focusBtn.buttonDown != null))
            {
                SetFocusBtn(focusBtn.buttonDown);
            }
            else if ((mVec.y > minInput) && (focusBtn.buttonUp != null))
            {
                SetFocusBtn(focusBtn.buttonUp);
            }
        }
    }

    // �X���C�_�[�𓮂���(�{�^����)
    void MoveSlider(Vector2 mVec)
    {
        // �t�H�[�J�X����Ă���{�^������OptionsSlider���擾�o������
        var sliderBtn = focusBtn.gameObject.GetComponent<OptionsSlider>();
        if (sliderBtn != null)
            sliderBtn.MoveSlider(mVec.x);
    }

    // �t�H�[�J�X����{�^����ݒ�
    public void SetFocusBtn(Button btn)
    {
        // �O��t�H�[�J�X����Ă����{�^���ƈقȂ�΃Z�b�g����
        if (btn != focusBtn)
        {
            oldfocusBtn = focusBtn;
            focusBtn = btn;

            // �t�H�[�J�X���ꂽ�Ƃ��̏���
            if (focusBtn != null)
                focusBtn.FocusProcess(true);

            // �t�H�[�J�X���O�ꂽ�Ƃ��̏���
            if (oldfocusBtn != null)
                oldfocusBtn.FocusProcess(false);
        }

        // �X�N���[���o�[�̃X�N���[������
        // �X�N���[�����K�v�ȍ��W���v�Z
        if ((focusScrollbar != null) && (focusBtn.group == ScrollBarController.instance.scrollBarStruct[ScrollBarController.instance.num].group) && (!focusBtn.orPointer))
        {
            float posY;                                     // ��ƂȂ�Y���W
            float maxY;                                     // �X�N���[�����s����ԏ��Y���W
            float minY;                                     // �X�N���[�����s����ԉ���Y���W
            float value;                                    // �X�N���[����
            int num;                                        // ScrollBarController��num
            var instance = ScrollBarController.instance;    // ScrollBarController�̃C���X�^���X

            num = instance.num;

            // �t�H�[�J�X����Ă���{�^����Y���W + �X�N���[���o�[��Y���W
            posY = focusBtn.gameObject.transform.localPosition.y + instance.scrollBarStruct[num].contentParentRect.localPosition.y;

            // ��[���班���Ⴂ���W
            maxY = -30.0f;

            // ���[���班���������W
            minY = -(instance.scrollBarStruct[num].parentRect.sizeDelta.y) + 30.0f;

            // �t�H�[�J�X�����{�^�������ȏ�̍��W�Ȃ�X�N���[������
            if (posY > maxY)
            {
                // �t�H�[�J�X���ꂽ�{�^����Y���W�ƃX�N���[�����s��Y���W�̍�����
                value = (posY - maxY);

                // �e�I�u�W�F�N�g�̍����ɑ΂���Y���W�̍��̊��������߂đ��
                value = Mathf.Abs(value / instance.scrollBarStruct[num].contentParentRect.sizeDelta.y);

                // �X�N���[������
                instance.Scroll(focusScrollbar, true, value);
            }

            // �t�H�[�J�X�����{�^�������ȉ��̍��W�Ȃ�X�N���[������
            else if (posY < minY)
            {
                // �t�H�[�J�X���ꂽ�{�^����Y���W�ƃX�N���[�����s��Y���W�̍�����
                value = (posY - minY);

                // �e�I�u�W�F�N�g�̍����ɑ΂���Y���W�̍��̊��������߂đ��
                value = Mathf.Abs(value / instance.scrollBarStruct[num].contentParentRect.sizeDelta.y);

                // �X�N���[������
                instance.Scroll(focusScrollbar, false, value);
            }
        }
    }
}