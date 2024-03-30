using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ダイアログポップアップ1
public class DialogPopup1 : PopupParent
{
    [SerializeField] private Button1 okBtn, cancelBtn;  // ボタン1のコンポーネント

    private  Action action;                             // OK ボタンを押したときに実行する処理
    public Action Action
    {
        get { return action; }
        set {
            SetBtnAction(okBtn, value);                 // action 代入時にボタンのアクションも設定する
            action = value;
        }
    }

    private Focus focus;

    protected override void Start()
    {
        base.Start();

        focus ??= Focus.instance;

        // キャンセルボタンの処理はポップアップの破棄
        Action a = () =>
        {
            scrCon.ScreenLoot = scrCon.oldScreenLoot;
            focus.SetFocusBtn(focus.oldLootFocusBtn);
        };

        SetBtnAction(cancelBtn, a);

        // Negative ボタンを押したらキャンセルボタンのアクションを起こす
        input.ui_OnNegativeDele += CancelBtnProcess;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        input.ui_OnNegativeDele -= CancelBtnProcess;
    }

    // キャンセルボタンの処理を行う
    void CancelBtnProcess(float f)
    {
        cancelBtn.PlayBtnSound(cancelBtn.ClickSound);
        cancelBtn.ClickProcess();
    }

    // ボタンのアクションを設定
    void SetBtnAction(Button1 btn1, Action action)
    {
        btn1.action = action;
    }

    // ボタンの ScreenAndLoot を設定
    public void SetScreenAndLoot(ScreenController.ScreenType scrType, int loot)
    {
        okBtn.scrAndLoot.scrType = scrType;
        okBtn.scrAndLoot.scrLoot = loot;
        cancelBtn.scrAndLoot.scrType = scrType;
        cancelBtn.scrAndLoot.scrLoot = loot;
    }

    // ポップアップの処理
    public override IEnumerator Process(string text, Transform parentT, int num)
    {
        index = num;

        popupMana ??= PopupManager.instance;
        focus ??= Focus.instance;

        // 親を設定
        popupMana.popupContent[(int)popupType].instance[index].transform.SetParent(parentT, false);

        // テキストを設定
        popupText.text = text;

        // キャンセルボタンをフォーカス
        focus.SetFocusBtn(cancelBtn);

        yield return null;
    }
}
