using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public Vector3 defaultPos;      // ボタンの初期位置

    private StageController stageCon;
    private Initialize init;
    private CreateStage cStage;
    private PopupManager popupMana;

    public override void OnPointerExit(PointerEventData pointerEventData)
    {
        ExitProcess();
    }

    // マウスポインターが乗った時の処理
    public override void EnterProcess()
    {
        base.EnterProcess();

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

            // ステージ開始ボタンを押したときの挙動を設定
            startBtn.action = () =>
            {
                stageCon.stageNum = stageNum;

                // 画面をInGameに変更
                scrCon.Screen = ScreenController.ScreenType.InGame;

                // ステージに関する数値を初期化
                init.init_Stage();

                // ポップアップの配列を初期化
                popupMana.Init(popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1]);

                // ステージ生成
                cStage.Destroy();
                cStage.Create();
            };

            stageCon.DSIdele?.Invoke(defaultPos, gameObject, this);
        }
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
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        defaultPos = gameObject.transform.localPosition;
    }

    protected override void Start()
    {
        base.Start();

        stageCon ??= StageController.instance;
        init = Initialize.instance;
        cStage = CreateStage.instance;
        popupMana = PopupManager.instance;
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
