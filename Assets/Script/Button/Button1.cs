using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ボタン1を管理
public class Button1 : Button
{
    private Arrow arrow;
    private Initialize init;
    private CreateStage cStage;
    private SkillSelect skillSelect;
    private PopupManager popupMana;
    private Localize localize;

    public enum ClickAction // ボタンを押したときの効果
    {
        None,                   // 効果なし
        ChangeScreen,           // 指定した画面に遷移
        StageStart,             // ステージスタート
        CreatePlanetDirArrow,   // 惑星の方向を指し示す矢印を生成
        ApplySkill,             // 選択したスキルを適用
        ResetSelectSkill,       // 選択したスキルをリセット
        ExitGame,               // ゲーム終了
        Action,                 // 任意の Action を実行
    }

    [SerializeField] private ClickAction clickAction;
    [SerializeField] private ScreenController.ScreenType nextScreen = 0; // 遷移先の画面

    public Action action;

    // マウスポインターが乗った時の処理
    public override void EnterProcess()
    {
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
        // ボタンを押したときの効果によって分岐
        switch (clickAction)
        {
            case ClickAction.ChangeScreen:          ChangeScreen();             break;  // 画面遷移
            case ClickAction.StageStart:            StageStart();               break;  // ステージスタート
            case ClickAction.CreatePlanetDirArrow:  CreatePlanetDirArrow();     break;  // 惑星の方向を指し示す矢印を生成
            case ClickAction.ApplySkill:            ApplySkill();               break;  // 選択したスキルを適用
            case ClickAction.ResetSelectSkill:      ResetSelectSkill();         break;  // 選択したスキルをリセット
            case ClickAction.ExitGame:              ExitGame();                 break;  // ゲーム終了
            case ClickAction.Action:                action?.Invoke();           break;  // 任意の Action を実行
            default: break;
        }
    }

    // 画面遷移
    private void ChangeScreen()
    {
        scrCon.Screen = nextScreen;
    }

    // ステージスタート
    void StageStart()
    {
        // 画面をInGameに変更
        scrCon.Screen = ScreenController.ScreenType.InGame;

        // ステージに関する数値を初期化
        init.init_Stage();

        // ポップアップの配列を初期化
        popupMana.Init(popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1]);

        // ステージ生成
        cStage.Destroy();
        cStage.Create();
    }

    // 惑星の方向を指し示す矢印を生成
    void CreatePlanetDirArrow()
    {
        GameObject target = GameObject.Find(transform.parent.gameObject.name);
        arrow.Create(target);
    }

    // 選択したスキルを適用
    void ApplySkill()
    {
        // スキルを3つ選択したか判定
        if (skillSelect.CheckNone())
        {
            // スキルを確定
            skillSelect.SetSelectSlot();

            // ポップアップ表示
            popupMana.DrawPopup(PopupManager.PopupType.InMenuPopup1, localize.GetString(StringGroup.Message, StringType.WasDecided));
        }
        else
        {
            // ポップアップ表示
            popupMana.DrawPopup(PopupManager.PopupType.InMenuPopup1, localize.GetString(StringGroup.Message, StringType.PleaseSelect3Skills));
        }
    }

    // 選択したスキルをリセット
    void ResetSelectSkill()
    {
        skillSelect.InitSelectSlot();
    }

    // ゲームを終了
    void ExitGame()
    {
        // ダイアログポップアップを生成
        GameObject g = popupMana.DrawPopup(PopupManager.PopupType.DialogPopup1, localize.GetString(StringGroup.Message, StringType.ExitGameText));

        // ポップアップ生成済みなら終了
        if (g == null) return;

        scrCon.ScreenLoot = 1;

        // ポップアップのコンポーネントを取得
        DialogPopup1 dp1 = g.GetComponent<DialogPopup1>();

        // ポップアップのボタンの値を設定
        dp1.SetScreenAndLoot(ScreenController.ScreenType.MainMenu, 1);

        // ポップアップの OK ボタンを押したときの処理を設定
        dp1.Action = () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
        };
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        arrow = Arrow.instance;
        init = Initialize.instance;
        cStage = CreateStage.instance;
        skillSelect = SkillSelect.instance;
        popupMana = PopupManager.instance;
        localize = Localize.instance;
    }
}
