using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ダイアログポップアップ1
public class DialogPopup1 : PopupParent
{
    [SerializeField] private Button1 okBtn, cancelBtn;  // ボタン1のコンポーネント
    [SerializeField] private Text popupText;            // ポップアップのテキスト

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
            scrCon.ScreenLoot = 0;
            Destroy();
        };

        SetBtnAction(cancelBtn, a);
    }

    // ボタンのアクションを設定
    void SetBtnAction(Button1 btn1, Action action)
    {
        btn1.action = action;
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
