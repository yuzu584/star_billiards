using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ボタン1を管理
public class Button1 : Button
{
    // Findで探したGameObjectのコンポーネント
    private SkillController skillCon;
    private Arrow arrow;
    private Initialize init;
    private CreateStage cStage;

    public enum ClickAction // ボタンを押したときの効果
    {
        None,                   // 効果なし
        ChangeScreen,           // 指定した画面に遷移
        StageStart,             // ステージスタート
        CreatePlanetDirArrow,   // 惑星の方向を指し示す矢印を生成
        ApplySkill,             // 選択したスキルを適用
        ResetSelectSkill,       // 選択したスキルをリセット
        ExitGame,               // ゲーム終了
    }

    [SerializeField] private ClickAction clickAction;
    [SerializeField] private int nextScreen = 0; // 遷移先の画面番号

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
            case ClickAction.ChangeScreen:          ChangeScreen();         break;  // 画面遷移
            case ClickAction.StageStart:            StageStart();           break;  // ステージスタート
            case ClickAction.CreatePlanetDirArrow:  CreatePlanetDirArrow(); break;  // 惑星の方向を指し示す矢印を生成
            case ClickAction.ApplySkill:            ApplySkill();           break;  // 選択したスキルを適用
            case ClickAction.ResetSelectSkill:      ResetSelectSkill();     break;  // 選択したスキルをリセット
            case ClickAction.ExitGame:              ExitGame();             break;  // ゲーム終了
            default: break;
        }
    }

    // 画面遷移
    private void ChangeScreen()
    {
        scrCon.ScreenNum = nextScreen;
    }

    // ステージスタート
    void StageStart()
    {
        // 画面番号をInGameに変更
        scrCon.ScreenNum = 5;
        scrCon.ScreenLoot = 0;

        // ステージに関する数値を初期化
        init.init_Stage();

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
        skillCon.SetSelectSlot();
    }

    // 選択したスキルをリセット
    void ResetSelectSkill()
    {
        skillCon.InitSelectSlot();
    }

    // ゲームを終了
    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // ボタンの初期化処理
        BtnInit(imageStructs, textStructs);
    }

    protected override void Start()
    {
        base.Start();

        skillCon = SkillController.instance;
        arrow = Arrow.instance;
        init = Initialize.instance;
        cStage = CreateStage.instance;
    }
}
