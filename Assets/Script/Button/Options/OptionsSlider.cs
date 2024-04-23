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

    [SerializeField] private string key;
    private AppParams.IClampedValue clampedValue;

    private OptionsController opCon;
    private AppParams appParams;

    // �}�E�X�|�C���^�[����������̏���
    public override void EnterProcess()
    {
        base.EnterProcess();
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    public override void ExitProcess()
    {
        base.ExitProcess();
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    public override void ClickProcess()
    {
        base.ClickProcess();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        opCon = OptionsController.instance;
        appParams = AppParams.instance;

        // �X���C�_�[�̐ݒ�
        SetSliderState();
    }

    // �{�^���̏�Ԃ�\���e�L�X�g��ݒ�
    void SetStateText(Type type)
    {
        if(type == typeof(int))
        {
            state.text = slider.value.ToString("f0");
        }
        else if (type == typeof(float))
        {
            state.text = slider.value.ToString("f2");
        }
    }

    // �X���C�_�[�𓮂���(�{�^����)
    public void MoveSlider(float value)
    {
        // ���͒l���ۂ߂�
        float roundValue = Mathf.Round(value);

        // �ω�������l���X���C�_�[�̍ő�l�̌����ɂ���ĕω�������
        float rate = 1;
        for (int i = 0; i < slider.maxValue.ToString().Length; ++i)
            rate *= 10;

        // �l��ω�������
        slider.value += roundValue * (rate / 1000);
    }

    // �X���C�_�[�̐ݒ�
    void SetSliderState()
    {
        // IClampedValue �^�̃C���^�[�t�F�[�X���擾
        clampedValue = appParams.GetClampedValue(key);

        if (clampedValue == null)
        {
            Debug.LogError("IClampedValue �^�̃C���^�[�t�F�[�X���擾�ł��܂���ł����B");
            return;
        }

        // int�^�Ȃ� Slider �̒l�𐮐��ɂ���
        if (clampedValue.GetThisType() == typeof(int))
            slider.wholeNumbers = true;

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

                // �{�^���̏�Ԃ�\���e�L�X�g��ݒ�
                SetStateText(clampedValueType);
            }
            // �擾���� Type �� float �^�Ȃ�
            else if (clampedValueType == typeof(float))
            {
                // �X���C�_�[�̒l�����̂܂ܑ��
                clampedValue.SetValue(slider.value);

                // �{�^���̏�Ԃ�\���e�L�X�g��ݒ�
                SetStateText(clampedValueType);
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

            Action action = () => { slider.maxValue = clampedValue.GetMax_Float(); };
            clampedValue.SetOnMaxChanged(() => { slider.maxValue = clampedValue.GetMax_Float(); });
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
