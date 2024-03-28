using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ボタンに関する情報を保存する
[DefaultExecutionOrder(-100)]
public class ButtonRecorder : Singleton<ButtonRecorder>
{
    // 保存するボタンの構造体
    [System.Serializable]
    public struct SaveButtonContent
    {
        public Button[] btn;
    }

    public SaveButtonContent[] savedBtn;

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
            if (savedBtn[(int)scrCon.Screen].btn[scrCon.ScreenLoot])
                focus.SetFocusBtn(savedBtn[(int)scrCon.Screen].btn[scrCon.ScreenLoot]);
            else
                savedBtn[(int)scrCon.Screen].btn[scrCon.ScreenLoot] = null;
        };

        // 画面遷移時にもボタンをフォーカスする
        scrCon.changeScreen += () =>
        {
            if (savedBtn[(int)scrCon.Screen].btn[scrCon.ScreenLoot])
                focus.SetFocusBtn(savedBtn[(int)scrCon.Screen].btn[scrCon.ScreenLoot]);
            else
                savedBtn[(int)scrCon.Screen].btn[scrCon.ScreenLoot] = null;
        };
    }

    // フォーカスされていたボタンを保存
    public void SaveFocusedButton(Button btn)
    {
        scrCon ??= ScreenController.instance;

        InitLoot();

        savedBtn[(int)btn.scrAndLoot.scrType].btn[btn.scrAndLoot.scrLoot] = btn;
    }

    // 配列の長さを設定
    void InitLoot()
    {
        // 初回のみ実行
        if(!setLootLength) {

            setLootLength = true;

            // 配列の長さを設定
            savedBtn = new SaveButtonContent[scrData.screenList.Count];
            for (int i = 0; i < savedBtn.Length; i++)
            {
                savedBtn[i].btn = new Button[scrData.screenList[i].loot.Length];
            }
        }
    }
}
