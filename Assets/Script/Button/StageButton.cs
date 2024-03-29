using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class StageButton : Button
{
    [SerializeField]
    private int stageNum;           // セットするステージ番号

    [SerializeField]
    private Text stageName;         // ステージ名のテキスト

    [SerializeField]
    private StageData stageData;    // ステージのデータをまとめたScriptableObject

    [SerializeField]
    private Button1 startBtn;       // ステージスタートのボタン

    public bool anim = false;       // ボタンがアニメーション中か

    private StageController stageCon;

    // マウスポインターが乗った時の処理
    public override void EnterProcess()
    {
        // アニメーション中ではないなら
        if (!anim)
        {
            // フォーカス先のボタンを設定
            startBtn.buttonUp = buttonUp;
            startBtn.buttonDown = buttonDown;
            startBtn.buttonRight = buttonRight;
            startBtn.buttonLeft = buttonLeft;

            focus.SetFocusBtn(startBtn);

            stageCon ??= StageController.instance;

            stageCon.stageNum = stageNum;
            stageCon.DSIdele?.Invoke(transform.localPosition, gameObject, this);
        }

        // ボタンのアニメーション処理
        BtnAnimProcess(imageStructs, textStructs, true);
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

    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        stageCon ??= StageController.instance;

        // テキストをステージ名に設定
        stageName.text = stageData.stageList[stageNum].stageName;
    }

    void Update()
    {
        if (anim)
        {
            stageName.enabled = false;
        }
        else
        {
            stageName.enabled = true;
        }
    }
}
