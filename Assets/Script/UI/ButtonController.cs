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
    [SerializeField] private ScreenController screenController;             // InspectorでScreenControllerを指定
    [SerializeField] private UIController uIController;                     // InspectorでUIControllerを指定
    [SerializeField] private PauseUIController pauseUIController;           // InspectorでPauseUIControllerを指定
    [SerializeField] private GameObject planetInfo;                         // 惑星情報UI
    [SerializeField] private enum ClickAction                               // ボタンを押したときの効果
    {
        ReturnToGame,     // ゲームに戻る
        Setting,          // 設定画面を開く
        ReturnToMainMenu, // メインメニューに戻る
        StageSelect,      // ステージ選択画面に戻る
        StageStart,       // ステージスタート
    }
    [SerializeField] private ClickAction clickAction; // ボタンを押したときの効果

    // マウスポインターがボタンの上に乗ったら
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ボタンのアニメーション
        StopAllCoroutines();
        StartCoroutine(ButtonAnimation(defaultColor, OnPointerColor));
    }

    // マウスポインターがボタンの上から離れたら
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ボタンのアニメーション
        StopAllCoroutines();
        StartCoroutine(ButtonAnimation(OnPointerColor, defaultColor));
    }
    
    // ボタンがクリックされたら
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // ボタンごとの効果によって分岐
        switch (clickAction)
        {
            case ClickAction.ReturnToGame:     // ゲームに戻る
                ReturnToGame();
                break;
            case ClickAction.Setting:          // 設定画面を開く
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
            default:
                break;
        }
    }

    // ゲームに戻る
    void ReturnToGame()
    {
        // 画面番号をInGameに変更
        screenController.screenNum = 0;

        // ポーズ画面のUIを非表示
        pauseUIController.DrawPauseUI(false);

        // 惑星情報UIを表示
        planetInfo.SetActive(true);

        // 時間の流れを元に戻す
        Time.timeScale = 1.0f;
    }

    // メインメニューに戻る
    void ReturnToMainMenu()
    {
        // 画面番号をTitleに変更
        screenController.screenNum = 3;
    }

    // ステージ選択画面に遷移
    void StageSelect()
    {
        // 画面番号をStageSelectに変更
        screenController.screenNum = 4;
    }

    // ステージスタート
    void StageStart()
    {
        // 画面番号をInGameに変更
        screenController.screenNum = 0;
    }

    // ボタンのアニメーション
    IEnumerator ButtonAnimation(Color colorA, Color colorB)
    {
        float time = 0; // 経過時間をカウント

        // 指定した時間が経過するまで繰り返す
        while (time < fadeTime)
        {
            // 時間をカウント
            time += Time.unscaledDeltaTime;

            // 進み具合を計算
            float t = time / fadeTime;

            // ボタンの色を変更
            Btn.color = Color.Lerp(colorA, colorB, t);

            // 1フレーム待つ
            yield return null;
        }
    }
}
