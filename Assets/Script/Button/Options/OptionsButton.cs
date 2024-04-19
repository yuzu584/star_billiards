using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 設定画面の汎用ボタン
public class OptionsButton : Button
{
    [SerializeField] private int loot; // 遷移先の設定項目

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

        // 設定画面の階層を変更
        opCon.SwitchLoot(loot);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        opCon = OptionsController.instance;
    }
}
