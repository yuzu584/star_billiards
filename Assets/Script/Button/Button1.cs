using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;

// ボタン1を管理
public class Button1 : Button
{
    // Findで探すGameObject
    private GameObject Player;
    private GameObject ArrowController;
    private GameObject InitializeController;
    private GameObject Stage;

    // Findで探したGameObjectのコンポーネント
    private SkillController skillController;
    private Arrow arrow;
    private Initialize initialize;
    private CreateStage createStage;

    public enum ClickAction // ボタンを押したときの効果
    {
        None,                 // 効果なし
        ChangeScreen,         // 指定した画面に遷移
        StageStart,           // ステージスタート
        CreatePlanetDirArrow, // 惑星の方向を指し示す矢印を生成
        ApplySkill,           // 選択したスキルを適用
        ResetSelectSkill,     // 選択したスキルをリセット
    }

    [SerializeField] private ClickAction clickAction;
    [SerializeField] private int nextScreen = 0; // 遷移先の画面番号

    // マウスポインターが乗った時の処理
    protected override void EnterProcess()
    {
        // ボタンのアニメーション
        StopAllCoroutines();
        for (int i = 0; i < imageStructs.Length; i++)
        {
            StartCoroutine(lerp.Color_Image(imageStructs[i].image, imageStructs[i].startColor, imageStructs[i].endColor, imageStructs[i].fadeTime));
            StartCoroutine(lerp.Color_Text(textStructs[i].text, textStructs[i].startColor, textStructs[i].endColor, textStructs[i].fadeTime));
        }
    }

    // マウスポインターが離れたときの処理
    protected override void ExitProcess()
    {
        // ボタンのアニメーション
        StopAllCoroutines();
        for (int i = 0; i < imageStructs.Length; i++)
        {
            StartCoroutine(lerp.Color_Image(imageStructs[i].image, imageStructs[i].endColor, imageStructs[i].startColor, imageStructs[i].fadeTime));
            StartCoroutine(lerp.Color_Text(textStructs[i].text, textStructs[i].endColor, textStructs[i].startColor, textStructs[i].fadeTime));
        }
    }

    // クリックされたときの処理
    protected override void ClickProcess()
    {
        // ボタンを押したときの効果によって分岐
        switch (clickAction)
        {
            case ClickAction.ChangeScreen:         // 画面遷移
                ChangeScreen();
                break;
            case ClickAction.StageStart:           // ステージスタート
                StageStart();
                break;
            case ClickAction.CreatePlanetDirArrow: // 惑星の方向を指し示す矢印を生成
                CreatePlanetDirArrow();
                break;
            case ClickAction.ApplySkill:           // 選択したスキルを適用
                ApplySkill();
                break;
            case ClickAction.ResetSelectSkill:     // 選択したスキルをリセット
                ResetSelectSkill();
                break;
            default:
                break;
        }
    }

    // 画面遷移
    private void ChangeScreen()
    {
        screenController.screenNum = nextScreen;
    }

    // ステージスタート
    void StageStart()
    {
        // 画面番号をInGameに変更
        screenController.screenNum = 5;

        // ステージに関する数値を初期化
        initialize.init_Stage();

        // ステージ生成
        createStage.Destroy();
        createStage.Create();
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
        skillController.SetSelectSlot();
    }

    // 選択したスキルをリセット
    void ResetSelectSkill()
    {
        skillController.InitSelectSlot();
    }

    void OnEnable()
    {
        // ボタンの色をリセット
        StopAllCoroutines();
        for (int i = 0; i < imageStructs.Length; i++)
        {
            imageStructs[i].image.color = imageStructs[i].startColor;
            textStructs[i].text.color = textStructs[i].startColor;
        }
    }

    new void Start()
    {
        base.Start();

        // オブジェクトを探してコンポーネントを取得
        Player = GameObject.Find("Player");
        ArrowController = GameObject.Find("ArrowController");
        InitializeController = GameObject.Find("InitializeController");
        Stage = GameObject.Find("Stage");
        skillController = Player.GetComponent<SkillController>();
        arrow = ArrowController.GetComponent<Arrow>();
        initialize = InitializeController.GetComponent<Initialize>();
        createStage = Stage.GetComponent<CreateStage>();
    }
}
