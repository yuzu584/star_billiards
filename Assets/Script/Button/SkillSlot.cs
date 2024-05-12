using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using AppConst;

public class SkillSlot : Button
{
    [SerializeField] private SkillController.SkillType skill = 0;       // スキル
    [SerializeField] private Text nameText;                             // スキル名を表すテキスト
    [SerializeField] private Image selectedImage;                       // スキルの選択状態を表す画像
    [SerializeField] private LocalizeText localizeText;

    private SkillSelect skillSelect;

    // マウスポインターが乗った時の処理
    public override void EnterProcess()
    {
        base.EnterProcess();

        skillSelect ??= SkillSelect.instance;

        // スキルの情報を描画
        skillSelect.DSIdele?.Invoke(skill);
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

        // 既に選択されたスキルでなければ、選択しているスキルスロットを設定
        if (skillSelect.CheckDoubleSelect(skill))
        {
            skillSelect.SetSelectSkill(skill);
        }
        // 既に選択済みならば、選択を解除する
        else
        {
            skillSelect.CancelSkill(skill);
        }
    }

    // スキルの選択状態表す画像の色を設定
    void SetSelectImageColor()
    {
        // スキルスロットの数繰り返す
        for (int i = 0; i < Const_Skill.SKILL_SLOT_AMOUNT; i++)
        {
            // スキルがセットされていれば色を明るくする
            if (skillSelect.selectSlot[i] == skill)
            {
                selectedImage.color = Const_Button.SKILLSLOT_SELECTIMAGE_SELECT_COLOR;
                return;
            }
        }

        // スキルがセットされていなければ色を薄くする
        selectedImage.color = Const_Button.SKILLSLOT_SELECTIMAGE_DEFAULT_COLOR;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // 翻訳テキストの設定
        localizeText.seet = "skill_name";
        localizeText.dataName = skill.ToString();
        localizeText.SetText();
    }

    protected override void Start()
    {
        base.Start();

        skillSelect ??= SkillSelect.instance;
    }

    void Update()
    {
        // スキルの選択状態表す画像の色を設定
        SetSelectImageColor();
    }
}
