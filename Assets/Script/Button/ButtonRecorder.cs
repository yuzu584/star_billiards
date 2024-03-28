using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �{�^���Ɋւ������ۑ�����
[DefaultExecutionOrder(-100)]
public class ButtonRecorder : Singleton<ButtonRecorder>
{
    // �ۑ�����{�^���̍\����
    [System.Serializable]
    public struct SaveButtonContent
    {
        public Button[] btn;
    }

    public SaveButtonContent[] savedBtn;

    [SerializeField] private ScreenData scrData;

    private ScreenController scrCon;
    private Focus focus;

    private bool setLootLength = false;     // loot �̔z��̒�����ݒ肵����

    protected override void Awake()
    {
        base.Awake();

        InitLoot();
    }

    private void Start()
    {
        scrCon ??= ScreenController.instance;
        focus = Focus.instance;

        // �K�w�J�ڎ��Ƀ{�^�����t�H�[�J�X����
        scrCon.changeLoot += () =>
        {
            if (savedBtn[(int)scrCon.Screen].btn[scrCon.ScreenLoot])
                focus.SetFocusBtn(savedBtn[(int)scrCon.Screen].btn[scrCon.ScreenLoot]);
            else
                savedBtn[(int)scrCon.Screen].btn[scrCon.ScreenLoot] = null;
        };

        // ��ʑJ�ڎ��ɂ��{�^�����t�H�[�J�X����
        scrCon.changeScreen += () =>
        {
            if (savedBtn[(int)scrCon.Screen].btn[scrCon.ScreenLoot])
                focus.SetFocusBtn(savedBtn[(int)scrCon.Screen].btn[scrCon.ScreenLoot]);
            else
                savedBtn[(int)scrCon.Screen].btn[scrCon.ScreenLoot] = null;
        };
    }

    // �t�H�[�J�X����Ă����{�^����ۑ�
    public void SaveFocusedButton(Button btn)
    {
        scrCon ??= ScreenController.instance;

        InitLoot();

        savedBtn[(int)btn.scrAndLoot.scrType].btn[btn.scrAndLoot.scrLoot] = btn;
    }

    // �z��̒�����ݒ�
    void InitLoot()
    {
        // ����̂ݎ��s
        if(!setLootLength) {

            setLootLength = true;

            // �z��̒�����ݒ�
            savedBtn = new SaveButtonContent[scrData.screenList.Count];
            for (int i = 0; i < savedBtn.Length; i++)
            {
                savedBtn[i].btn = new Button[scrData.screenList[i].loot.Length];
            }
        }
    }
}
