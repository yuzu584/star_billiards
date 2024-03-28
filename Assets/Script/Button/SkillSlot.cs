using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Const;

public class SkillSlot : Button
{
    [SerializeField] private SkillController.SkillType skill = 0;       // スキル
    [SerializeField] private Text nameText;                             // スキル名を表すテキスト
    [SerializeField] private Image selectedImage;                       // スキルの選択状態を表す画像

    // instanceを代入する変数
    private SkillController skillCon;
    private SkillSelectUIController skillSelectUICon;
    private SkillSelect skillSelect;

    private Color defaultSelectImageColor = new(255.0f, 255.0f, 255.0f, 0.04f);
    private Color selectedSelectImageColor = new(255.0f, 255.0f, 255.0f, 1.0f);

    // マウスポインターが乗った時の処理
    public override void EnterProcess()
    {
        // ボタンのアニメーション処理
        BtnAnimProcess(imageStructs, textStructs, true);

        // スキルの情報を描画
        if(skillSelectUICon != null)
            skillSelectUICon.DrawSkillInfo((int)skill);
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

    // スキル名を表すテキストを設定
    void SetNameText()
    {
        nameText.text = AppConst.SKILL_NAME[(int)skill];
    }

    // スキルの選択状態表す画像の色を設定
    void SetSelectImageColor()
    {
        // スキルスロットの数繰り返す
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            // スキルがセットされていれば色を明るくする
            if (skillSelect.selectSlot[i] == skill)
            {
                selectedImage.color = selectedSelectImageColor;
                return;
            }
        }

        // スキルがセットされていなければ色を薄くする
        selectedImage.color = defaultSelectImageColor;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        skillCon = SkillController.instance;
        skillSelectUICon = SkillSelectUIController.instance;
        skillSelect = SkillSelect.instance;

        // スキル名のテキストを設定
        SetNameText();
    }

    void Update()
    {
        // スキルの選択状態表す画像の色を設定
        SetSelectImageColor();
    }
}
