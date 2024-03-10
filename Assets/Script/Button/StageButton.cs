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

    // Findで探すGameObject
    private GameObject StageController;

    // Findで探したGameObjectのコンポーネント
    private StageSelectUIController stageSelectUIController;
    private StageController stageController;

    // マウスポインターが乗った時の処理
    protected override void EnterProcess()
    {
        // 階層を設定
        screenController.ScreenLoot = 0;

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
        // アニメーション中ではないなら
        if (!anim)
        {
            // 階層を設定
            screenController.ScreenLoot = 1;

            stageController.stageNum = num;
            stageSelectUIController.DrawStageInfo(this.transform.localPosition, this.gameObject, this);
        }
    }

    new void OnEnable()
    {
        base.OnEnable();

        // ボタンの初期化処理
        BtnInit(imageStructs, textStructs);
    }

    new void Start()
    {
        base.Start();

        // オブジェクトを探してコンポーネントを取得
        StageController = GameObject.Find("StageController");

        stageSelectUIController = UIFunctionController.GetComponent<StageSelectUIController>();
        stageController = StageController.GetComponent<StageController>();

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
