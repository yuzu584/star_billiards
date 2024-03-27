using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �{�^���Ɋւ������ۑ�����
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

    private bool setLootLength = false;     // loot �̔z��̒�����ݒ肵����

    private void Start()
    {
        scrCon ??= ScreenController.instance;

        InitLoot();
    }

    // �t�H�[�J�X����Ă����{�^����ۑ�
    public void SaveFocusedButton(Button btn)
    {
        scrCon ??= ScreenController.instance;

        InitLoot();

        loot[(int)scrCon.Screen].btn[scrCon.ScreenLoot] = btn;
    }

    // �z��̒�����ݒ�
    void InitLoot()
    {
        if(!setLootLength) {

            setLootLength = true;

            // �z��̒�����ݒ�
            loot = new Loot[scrData.screenList.Count];
            for (int i = 0; i < loot.Length; i++)
            {
                loot[i].btn = new Button[scrData.screenList[i].loot.Length];
            }
        }
    }
}
