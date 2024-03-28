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
        public int[] num;
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
    }

    // �t�H�[�J�X����Ă����{�^����ۑ�
    public void SaveFocusedButton(Button btn)
    {
        scrCon ??= ScreenController.instance;

        InitLoot();

        savedBtn[(int)btn.scrAndLoot.scrType].num[btn.scrAndLoot.scrLoot] = btn.btnNum;
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
                savedBtn[i].num = new int[scrData.screenList[i].loot.Length];

                // -1 �������ď�����
                for(int j = 0; j < savedBtn[i].num.Length; j++)
                {
                    savedBtn[i].num[j] = -1;
                }
            }
        }
    }
}
