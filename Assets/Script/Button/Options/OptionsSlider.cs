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

    [SerializeField] private string key;
    private AppParams.IClampedValue clampedValue;

    private OptionsController opCon;
    private AppParams appParams;

    // マウスポインターが乗った時の処理
    public override void EnterProcess()
    {
        base.EnterProcess();
    }

    // マウスポインターが離れたときの処理
    public override void ExitProcess()
    {
        base.ExitProcess();
    }

    // クリックされたときの処理
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

        // スライダーの設定
        SetSliderState();
    }

    // ボタンの状態を表すテキストを設定
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

    // スライダーを動かす(ボタンで)
    public void MoveSlider(float value)
    {
        // 入力値を丸める
        float roundValue = Mathf.Round(value);

        // 変化させる値をスライダーの最大値の桁数によって変化させる
        float rate = 1;
        for (int i = 0; i < slider.maxValue.ToString().Length; ++i)
            rate *= 10;

        // 値を変化させる
        slider.value += roundValue * (rate / 1000);
    }

    // スライダーの設定
    void SetSliderState()
    {
        // IClampedValue 型のインターフェースを取得
        clampedValue = appParams.GetClampedValue(key);

        if (clampedValue == null)
        {
            Debug.LogError("IClampedValue 型のインターフェースを取得できませんでした。");
            return;
        }

        // int型なら Slider の値を整数にする
        if (clampedValue.GetThisType() == typeof(int))
            slider.wholeNumbers = true;

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

                // ボタンの状態を表すテキストを設定
                SetStateText(clampedValueType);
            }
            // 取得した Type が float 型なら
            else if (clampedValueType == typeof(float))
            {
                // スライダーの値をそのまま代入
                clampedValue.SetValue(slider.value);

                // ボタンの状態を表すテキストを設定
                SetStateText(clampedValueType);
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

            Action action = () => { slider.maxValue = clampedValue.GetMax_Float(); };
            clampedValue.SetOnMaxChanged(() => { slider.maxValue = clampedValue.GetMax_Float(); });
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
