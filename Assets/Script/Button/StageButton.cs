using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class StageButton : Button
{
    [SerializeField]
    private int num; // セットするステージ番号

    [SerializeField]
    private Text stageName; // ステージ名のテキスト

    [SerializeField]
    private StageData stageData;  // ステージのデータをまとめたScriptableObject

    public bool anim = false; // ボタンがアニメーション中か

    private StageSelectUIController stageSelectUICon;
    private StageController stageCon;

    // マウスポインターが乗った時の処理
    public override void EnterProcess()
    {
        // 階層を設定
        scrCon.ScreenLoot = 0;

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
        // アニメーション中ではないなら
        if (!anim)
        {
            // 階層を設定
            scrCon.ScreenLoot = 1;

            stageCon.stageNum = num;
            stageSelectUICon.DrawStageInfo(this.transform.localPosition, this.gameObject, this);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        stageSelectUICon = StageSelectUIController.instance;
        stageCon = StageController.instance;

        // テキストをステージ名に設定
        stageName.text = stageData.stageList[num].stageName;
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
