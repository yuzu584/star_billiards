using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ボタンに関する情報を保存する
[DefaultExecutionOrder(-100)]
public class ButtonRecorder : Singleton<ButtonRecorder>
{
    [System.Serializable]
    public struct Loot
    {
        public Button[] btn;
    }

    public Loot[] loot;

    [SerializeField] private ScreenData scrData;

    private ScreenController scrCon;

    private bool setLootLength = false;     // loot の配列の長さを設定したか

    private void Start()
    {
        scrCon ??= ScreenController.instance;

        InitLoot();
    }

    // フォーカスされていたボタンを保存
    public void SaveFocusedButton(Button btn)
    {
        scrCon ??= ScreenController.instance;

        InitLoot();

        loot[(int)scrCon.Screen].btn[scrCon.ScreenLoot] = btn;
    }

    // 配列の長さを設定
    void InitLoot()
    {
        if(!setLootLength) {

            setLootLength = true;

            // 配列の長さを設定
            loot = new Loot[scrData.screenList.Count];
            for (int i = 0; i < loot.Length; i++)
            {
                loot[i].btn = new Button[scrData.screenList[i].loot.Length];
            }
        }
    }
}
