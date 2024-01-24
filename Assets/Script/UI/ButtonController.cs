using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// ボタンの見た目を管理
public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color OnPointerColor;                          // ポインターが乗った時の色
    [SerializeField] private Color defaultColor;                            // デフォルトの色
    [SerializeField] private float fadeTime;                                // フェード時間
    [SerializeField] private Image Btn;                                     // ボタンの画像
    [SerializeField] private Text BtnText;                                  // ボタンのテキスト
    public enum ClickAction                                                 // ボタンを押したときの効果
    {
        None,             // 効果なし
        ReturnToGame,     // ゲームに戻る
        Setting,          // 設定画面を開く
        ReturnToMainMenu, // メインメニューに戻る
        StageSelect,      // ステージ選択画面に戻る
        StageStart,       // ステージスタート
        PlanetList,       // 惑星情報画面を開く
        SkillSelect,      // スキル選択画面を開く
        ApplySkill,       // 選択したスキルを適用
        ResetSelectSkill, // 選択したスキルをリセット
    }
    public ClickAction clickAction;                                         // ボタンを押したときの効果

    // Findで探すGameObject
    private GameObject ScreenController;
    private GameObject Canvas;
    private GameObject UIFunctionController;
    private GameObject PlanetInfo;
    private GameObject ArrowController;
    private GameObject StageController;
    private GameObject Player;

    // Findで探したGameObjectのコンポーネント
    private ScreenController screenController;
    private UIController uIController;
    private PauseUIController pauseUIController;
    private Arrow arrow;
    private StageController stageController;
    private Lerp lerp;
    private SkillController skillController;

    // マウスポインターがボタンの上に乗ったら
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ボタンのアニメーション
        StopAllCoroutines();
        StartCoroutine(lerp.ChangeColor(Btn,defaultColor, OnPointerColor, fadeTime));
    }

    // マウスポインターがボタンの上から離れたら
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ボタンのアニメーション
        StopAllCoroutines();
        StartCoroutine(lerp.ChangeColor(Btn, OnPointerColor, defaultColor, fadeTime));
    }
    
    // ボタンがクリックされたら
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // ボタンの色をリセット
        Btn.color = defaultColor;

        // ボタンを押したときの効果によって分岐
        switch (clickAction)
        {
            case ClickAction.ReturnToGame:     // ゲームに戻る
                ReturnToGame();
                break;
            case ClickAction.Setting:          // 設定画面を開く
                Setting();
                break;
            case ClickAction.ReturnToMainMenu: // メインメニューに戻る
                ReturnToMainMenu();
                break;
            case ClickAction.StageSelect:      // ステージ選択画面に遷移
                StageSelect();
                break;
            case ClickAction.StageStart:       // ステージスタート
                StageStart();
                break;
            case ClickAction.PlanetList:       // 惑星リスト画面を開く
                PlanetList();
                break;
            case ClickAction.SkillSelect:      // スキル選択画面を開く
                SkillSelect();
                break;
            case ClickAction.ApplySkill:       // 選択したスキルを適用
                ApplySkill();
                break;
            case ClickAction.ResetSelectSkill: // 選択したスキルをリセット
                ResetSelectSkill();
                break;
            default:
                break;
        }
    }

    // ゲームに戻る
    void ReturnToGame()
    {
        // 画面番号をInGameに変更
        screenController.screenNum = 5;

        // 惑星情報UIを表示
        PlanetInfo.SetActive(true);
    }

    // 設定画面を開く
    void Setting()
    {
        // 画面番号をSettingに変更
        screenController.screenNum = 3;
    }

    // メインメニューに戻る
    void ReturnToMainMenu()
    {
        // 画面番号をMainMenuに変更
        screenController.screenNum = 1;
    }

    // ステージ選択画面に遷移
    void StageSelect()
    {
        // 画面番号をStageSelectに変更
        screenController.screenNum = 2;
    }

    // ステージスタート
    void StageStart()
    {
        // 画面番号をInGameに変更
        screenController.screenNum = 5;

        // ステージに関する数値を初期化
        stageController.Init();
    }

    // 惑星情報画面を開く
    void PlanetList()
    {
        GameObject target = GameObject.Find(transform.parent.gameObject.name);
        arrow.Create(target);
    }

    // スキル選択画面を開く
    void SkillSelect()
    {
        // 画面番号をSkillSelectに変更
        screenController.screenNum = 4;
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

    void Start()
    {
        // GameObjectを探す
        ScreenController = GameObject.Find("ScreenController");
        Canvas = GameObject.Find("Canvas");
        UIFunctionController = GameObject.Find("UIFunctionController");
        PlanetInfo = GameObject.Find("Planet Info");
        ArrowController = GameObject.Find("ArrowController");
        StageController = GameObject.Find("StageController");
        Player = GameObject.Find("Player");

        // 探したGameObjectのコンポーネントを取得
        screenController = ScreenController.gameObject.GetComponent<ScreenController>();
        uIController = Canvas.gameObject.GetComponent<UIController>();
        pauseUIController = UIFunctionController.GetComponent<PauseUIController>();
        arrow = ArrowController.GetComponent<Arrow>();
        stageController = StageController.GetComponent<StageController>();
        lerp = UIFunctionController.GetComponent<Lerp>();
        skillController = Player.GetComponent<SkillController>();
    }
}
