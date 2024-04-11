using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �ݒ��ʂ̃X���C�_�[
public class OptionsSlider : Button
{
    [SerializeField] private Text state;                           // �{�^���̏�Ԃ�\���e�L�X�g
    [SerializeField] private Slider slider;                        // �X���C�_�[

    [SerializeField] private AppParams.ParamsKey key;
    private AppParams.IClampedValue clampedValue;

    private OptionsController opCon;
    private AppParams appParams;

    // �}�E�X�|�C���^�[����������̏���
    public override void EnterProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, true);
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    public override void ExitProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, false);
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    public override void ClickProcess()
    {

    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // �{�^���̏�Ԃ�\���e�L�X�g��ݒ�
        SetStateText();
    }

    protected override void Start()
    {
        base.Start();

        opCon = OptionsController.instance;
        appParams = AppParams.instance;

        // �X���C�_�[�̐ݒ�
        SetSliderState();

        // �{�^���̏�Ԃ�\���e�L�X�g��ݒ�
        SetStateText();
    }

    // �{�^���̏�Ԃ�\���e�L�X�g��ݒ�
    void SetStateText()
    {
        state.text = slider.value.ToString("f1");
    }

    // �X���C�_�[�𓮂���(�{�^����)
    public void MoveSlider(float value)
    {
        slider.value += Mathf.Round(value) * 10;
    }

    // �X���C�_�[�̐ݒ�
    void SetSliderState()
    {
        // IClampedValue �^�̃C���^�[�t�F�[�X���擾
        clampedValue = appParams.GetClampedValue(key);

        // �X���C�_�[�̒l���ω������Ƃ��ɕϐ��̒l���ω�������
        slider.onValueChanged.AddListener(delegate
        {
            // ClampedValue �� Type ���擾
            Type clampedValueType = clampedValue.GetThisType();

            // �擾���� Type �� int �^�Ȃ�
            if (clampedValueType == typeof(int))
            {
                // �X���C�_�[�̒l�� int �^�ɕϊ����đ��
                clampedValue.SetValue(Mathf.RoundToInt(slider.value));
            }
            // �擾���� Type �� float �^�Ȃ�
            else if (clampedValueType == typeof(float))
            {
                // �X���C�_�[�̒l�����̂܂ܑ��
                clampedValue.SetValue(slider.value);
            }
        });

        // �擾�o������
        if (clampedValue != null)
        {
            // �X���C�_�[�̌��ݒl�E�ő�l�E�ŏ��l��ݒ�
            float max = clampedValue.GetMax_Float();
            float min = clampedValue.GetMin_Float();
            float value = clampedValue.GetValue_Float();
            slider.maxValue = max;
            slider.minValue = min;
            slider.value = value;
        }
        // �擾�ł��Ȃ�������
        else
        {
            slider.value = 80.0f;
            slider.maxValue = 100.0f;
            slider.minValue = 0.0f;
        }
    }
}
