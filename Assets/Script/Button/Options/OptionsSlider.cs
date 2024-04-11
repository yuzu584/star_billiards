using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// 設定画面のスライダー
public class OptionsSlider : Button
{
    [SerializeField] private Text state;                           // ボタンの状態を表すテキスト
    [SerializeField] private Slider slider;                        // スライダー

    [SerializeField] private AppParams.ParamsKey key;
    private AppParams.IClampedValue clampedValue;

    private OptionsController opCon;
    private AppParams appParams;

    // マウスポインターが乗った時の処理
    public override void EnterProcess()
    {
        // ボタンのアニメーション処理
        BtnAnimProcess(imageStructs, textStructs, true);
    }

    // マウスポインターが離れたときの処理
    public override void ExitProcess()
    {
        // ボタンのアニメーション処理
        BtnAnimProcess(imageStructs, textStructs, false);
    }

    // クリックされたときの処理
    public override void ClickProcess()
    {

    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // ボタンの状態を表すテキストを設定
        SetStateText();
    }

    protected override void Start()
    {
        base.Start();

        opCon = OptionsController.instance;
        appParams = AppParams.instance;

        // スライダーの設定
        SetSliderState();

        // ボタンの状態を表すテキストを設定
        SetStateText();
    }

    // ボタンの状態を表すテキストを設定
    void SetStateText()
    {
        state.text = slider.value.ToString("f1");
    }

    // スライダーを動かす(ボタンで)
    public void MoveSlider(float value)
    {
        slider.value += Mathf.Round(value) * 10;
    }

    // スライダーの設定
    void SetSliderState()
    {
        // IClampedValue 型のインターフェースを取得
        clampedValue = appParams.GetClampedValue(key);

        // スライダーの値が変化したときに変数の値も変化させる
        slider.onValueChanged.AddListener(delegate
        {
            // ClampedValue の Type を取得
            Type clampedValueType = clampedValue.GetThisType();

            // 取得した Type が int 型なら
            if (clampedValueType == typeof(int))
            {
                // スライダーの値を int 型に変換して代入
                clampedValue.SetValue(Mathf.RoundToInt(slider.value));
            }
            // 取得した Type が float 型なら
            else if (clampedValueType == typeof(float))
            {
                // スライダーの値をそのまま代入
                clampedValue.SetValue(slider.value);
            }
        });

        // 取得出来たら
        if (clampedValue != null)
        {
            // スライダーの現在値・最大値・最小値を設定
            float max = clampedValue.GetMax_Float();
            float min = clampedValue.GetMin_Float();
            float value = clampedValue.GetValue_Float();
            slider.maxValue = max;
            slider.minValue = min;
            slider.value = value;
        }
        // 取得できなかったら
        else
        {
            slider.value = 80.0f;
            slider.maxValue = 100.0f;
            slider.minValue = 0.0f;
        }
    }
}
