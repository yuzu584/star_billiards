using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Const;

public class SkillSlot : Button
{
    [SerializeField] private int skillNum = 0;     // スキル番号
    [SerializeField] private Text selectNumText;   // スキルの選択した順を表すテキスト

    // Findで探すもの
    private GameObject Player;

    // Findで探したGameObjectのコンポーネント
    private SkillController skillController;

    // マウスポインターが乗った時の処理
    protected override void EnterProcess()
    {
        // ボタンのアニメーション処理
        BtnAnimProcess(imageStructs, textStructs, true);
    }

    // マウスポインターが離れたときの処理
    protected override void ExitProcess()
    {
        // ボタンのアニメーション処理
        BtnAnimProcess(imageStructs, textStructs, false);
    }

    // クリックされたときの処理
    protected override void ClickProcess()
    {
        // 既に選択されたスキルでなければ、選択しているスキルスロットを設定
        if (CheckDoubleSelect(skillNum))
        {
            SetSelectSlot(skillNum);
        }
    }

    // スキルの選択した順を表すテキストを設定
    void SetSelectNumText()
    {
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            if (skillController.selectSlot[i] == skillNum)
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
        if (skillController.count >= AppConst.SKILL_SLOT_AMOUNT)
        {
            skillController.InitSelectSlot();
        }
        skillController.selectSlot[skillController.count] = num;
        ++skillController.count;
    }

    // 同じスキルを選択していないか検知
    bool CheckDoubleSelect(int num)
    {
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            if (skillController.selectSlot[i] == num) { return false; }
        }
        return true;
    }

    void OnEnable()
    {
        // ボタンの初期化処理
        BtnInit(imageStructs, textStructs);
    }

    new void Start()
    {
        base.Start();

        // オブジェクトを探してコンポーネントを取得
        Player = GameObject.Find("Player");

        skillController = Player.GetComponent<SkillController>();
    }

    void Update()
    {
        // スキルの選択した順を表すテキストを設定
        SetSelectNumText();
    }
}
