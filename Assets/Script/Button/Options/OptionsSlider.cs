using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// 設定画面のスライダー
public class OptionsSlider : Button
{
    [SerializeField] private OptionsController _optionsController;
    [SerializeField] private Text state;                           // ボタンの状態を表すテキスト
    [SerializeField] private Slider slider;                        // スライダー
    [SerializeField] private float defaultvalue;                   // 初期値
    [SerializeField] private float maxValue;                       // 最大値
    [SerializeField] private float minValue;                       // 最小値

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

    new void OnEnable()
    {
        base.OnEnable();

        // ボタンの初期化処理
        BtnInit(imageStructs, textStructs);

        // ボタンの状態を表すテキストを設定
        SetStateText();
    }

    new void Start()
    {
        base.Start();

        // スライダーの現在値・最大値・最小値を設定
        slider.value = defaultvalue;
        slider.maxValue = maxValue;
        slider.minValue = minValue;

        // ボタンの状態を表すテキストを設定
        SetStateText();
    }

    // ボタンの状態を表すテキストを設定
    public void SetStateText()
    {
        state.text = slider.value.ToString("f1");
    }

    // スライダーを動かす(ボタンで)
    public void MoveSlider(float value)
    {
        slider.value += Mathf.Round(value) * 10;
    }
}
