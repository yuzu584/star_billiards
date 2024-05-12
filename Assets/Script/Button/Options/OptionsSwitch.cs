using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// 設定画面のスイッチボタン
public class OptionsSwitch : Button
{
    [SerializeField] private Text state;                           // ボタンの状態を表すテキスト
    [SerializeField] private string[] stateText;                   // ボタンの状態を表すテキストにセットする文字列

    private int nowState = 0;                                      // 現在のボタンの状態
    private OptionsController opCon;

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

        // ボタンがロックされていたら処理を行わず終了
        if (lockButton) return;

        ++nowState;
        if(nowState > (stateText.Length - 1))
            nowState = 0;
        SetStateText();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // ボタンのテキストを設定
        SetStateText();
    }

    protected override void Start()
    {
        base.Start();

        opCon = OptionsController.instance;
    }

    // ボタンの状態を表すテキストを設定
    private void SetStateText()
    {
        state.text = stateText[nowState];
    }
}
