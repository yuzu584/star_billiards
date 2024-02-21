using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : Button
{
    [SerializeField]
    private int num; // セットするステージ番号

    // Findで探すGameObject
    private GameObject StageController;

    // Findで探したGameObjectのコンポーネント
    private StageSelectUIController stageSelectUIController;
    private StageController stageController;

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
        stageController.stageNum = num;
        stageSelectUIController.DrawStageInfo();
    }

    void OnEnable()
    {
        // ボタンの初期化処理
        BtnInit(imageStructs, textStructs);

        // ステージ情報UIを非表示
        if (stageSelectUIController != null)
            stageSelectUIController.HideStageInfo();
    }

    new void Start()
    {
        base.Start();

        // オブジェクトを探してコンポーネントを取得
        StageController = GameObject.Find("StageController");

        stageSelectUIController = UIFunctionController.GetComponent<StageSelectUIController>();
        stageController = StageController.GetComponent<StageController>();
    }
}
