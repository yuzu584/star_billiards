using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �ݒ��ʂ̃X���C�_�[
public class OptionsSlider : Button
{
    [SerializeField] private OptionsController _optionsController;
    [SerializeField] private Text state;                           // �{�^���̏�Ԃ�\���e�L�X�g
    [SerializeField] private Slider slider;                        // �X���C�_�[
    [SerializeField] private float defaultvalue;                   // �����l
    [SerializeField] private float maxValue;                       // �ő�l
    [SerializeField] private float minValue;                       // �ŏ��l

    // �}�E�X�|�C���^�[����������̏���
    protected override void EnterProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, true);
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    protected override void ExitProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, false);
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    protected override void ClickProcess()
    {

    }

    void OnEnable()
    {
        // �{�^���̏���������
        BtnInit(imageStructs, textStructs);

        // �{�^���̏�Ԃ�\���e�L�X�g��ݒ�
        SetStateText();
    }

    new void Start()
    {
        base.Start();

        // �X���C�_�[�̌��ݒl�E�ő�l�E�ŏ��l��ݒ�
        slider.value = defaultvalue;
        slider.maxValue = maxValue;
        slider.minValue = minValue;

        // �{�^���̏�Ԃ�\���e�L�X�g��ݒ�
        SetStateText();
    }

    // �{�^���̏�Ԃ�\���e�L�X�g��ݒ�
    public void SetStateText()
    {
        state.text = slider.value.ToString("f1");
    }
}