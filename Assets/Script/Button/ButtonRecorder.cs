using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ボタンに関する情報を保存する
[DefaultExecutionOrder(-100)]
public class ButtonRecorder : Singleton<ButtonRecorder>
{
    [System.Serializable]
    public struct LootStr
    {
        public Button[] btn;
    }

    public LootStr[] lootStr;

    [SerializeField] private ScreenData scrData;

    private ScreenController scrCon;
    private Focus focus;

    private bool setLootLength = false;     // loot の配列の長さを設定したか

    protected override void Awake()
    {
        base.Awake();

        InitLoot();
    }

    private void Start()
    {
        scrCon ??= ScreenController.instance;
        focus = Focus.instance;

        // 階層遷移時にボタンをフォーカスする
        scrCon.changeLoot += () =>
        {
            if (lootStr[(int)scrCon.Screen].btn[scrCon.ScreenLoot])
                focus.SetFocusBtn(lootStr[(int)scrCon.Screen].btn[scrCon.ScreenLoot]);
            else
                lootStr[(int)scrCon.Screen].btn[scrCon.ScreenLoot] = null;
        };

        // 画面遷移時にもボタンをフォーカスする
        scrCon.changeScreen += () =>
        {
            if (lootStr[(int)scrCon.Screen].btn[scrCon.ScreenLoot])
                focus.SetFocusBtn(lootStr[(int)scrCon.Screen].btn[scrCon.ScreenLoot]);
            else
                lootStr[(int)scrCon.Screen].btn[scrCon.ScreenLoot] = null;
        };
    }

    // フォーカスされていたボタンを保存
    public void SaveFocusedButton(Button btn)
    {
        scrCon ??= ScreenController.instance;

        InitLoot();

        lootStr[(int)btn.scrAndLoot.scrType].btn[btn.scrAndLoot.scrLoot] = btn;
    }

    // 配列の長さを設定
    void InitLoot()
    {
        // 初回のみ実行
        if(!setLootLength) {

            setLootLength = true;

            // 配列の長さを設定
            lootStr = new LootStr[scrData.screenList.Count];
            for (int i = 0; i < lootStr.Length; i++)
            {
                lootStr[i].btn = new Button[scrData.screenList[i].loot.Length];
            }
        }
    }
}
