using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Const;

public class SkillSlot : Button
{
    [SerializeField] private int skillNum = 0;     // スキル番号
    [SerializeField] private Text nameText;        // スキル名を表すテキスト
    [SerializeField] private Text selectNumText;   // スキルの選択した順を表すテキスト

    // instanceを代入する変数
    private SkillController skillCon;
    private SkillSelectUIController skillSelectUICon;

    // マウスポインターが乗った時の処理
    public override void EnterProcess()
    {
        // ボタンのアニメーション処理
        BtnAnimProcess(imageStructs, textStructs, true);

        // スキルの情報を描画
        if(skillSelectUICon != null)
            skillSelectUICon.DrawSkillInfo(skillNum);
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
        // 既に選択されたスキルでなければ、選択しているスキルスロットを設定
        if (CheckDoubleSelect(skillNum))
        {
            SetSelectSlot(skillNum);
        }
    }

    // スキル名を表すテキストを設定
    void SetNameText()
    {
        nameText.text = AppConst.SKILL_NAME[skillNum];
    }

    // スキルの選択した順を表すテキストを設定
    void SetSelectNumText()
    {
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            if (skillCon.selectSlot[i] == skillNum)
            {
                selectNumText.enabled = true;
                selectNumText.text = (i + 1).ToString("0");
                return;
            }
        }

        // スキルがセットされていなければテキストを非表示
        selectNumText.enabled = false;
    }

    // 選択しているスキルスロットを設定
    void SetSelectSlot(int num)
    {
        if (skillCon.count >= AppConst.SKILL_SLOT_AMOUNT)
        {
            skillCon.InitSelectSlot();
        }
        skillCon.selectSlot[skillCon.count] = num;
        ++skillCon.count;
    }

    // 同じスキルを選択していないか検知
    bool CheckDoubleSelect(int num)
    {
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            if (skillCon.selectSlot[i] == num) { return false; }
        }
        return true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // ボタンの初期化処理
        BtnInit(imageStructs, textStructs);
    }

    protected override void Start()
    {
        base.Start();

        skillCon = SkillController.instance;
        skillSelectUICon = SkillSelectUIController.instance;

        // スキル名のテキストを設定
        SetNameText();
    }

    void Update()
    {
        // スキルの選択した順を表すテキストを設定
        SetSelectNumText();
    }
}
