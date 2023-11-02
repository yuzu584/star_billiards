using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// ボタンの見た目を管理
public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color color;                       // ポインターが乗った時の色
    [SerializeField] private Color defaultColor;                // デフォルトの色
    [SerializeField] private Image Btn;                         // ボタンの画像
    [SerializeField] private Image BtnOutline;                  // ボタンの枠の画像
    [SerializeField] private Text BtnText;                      // ボタンのテキスト
    [SerializeField] private ScreenController screenController; // ScreenController型
    [SerializeField] private UIController uIController;         // UIController型の変数
    [SerializeField] private CursorController cursorController; // CursorController型の変数
    [SerializeField] private GameObject planetInfo;             // 惑星情報UI
    [SerializeField] private enum ClickAction                   // ボタンが押されたときの効果
    {
        ReturnToGame,  // ゲームに戻る
        Setting,       // 設定画面を開く
        ReturnToTitle, // タイトル画面に戻る
    }
    [SerializeField] private ClickAction clickAction;           // ボタンを押したときの効果

    private Vector3[] defaultPos = new Vector3[3]; // ボタンの位置

    void Start()
    {
        // ボタンの位置を保存
        defaultPos[0] = Btn.rectTransform.position;
        defaultPos[1] = BtnOutline.rectTransform.position;
        defaultPos[2] = BtnText.rectTransform.position;
    }

    // マウスポインターがボタンの上に乗ったら
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ボタンの色を変更
        Btn.color = color;

        // ボタンをアニメーション
        StartCoroutine(ButtonAnimation(true, 0.2f));
    }

    // マウスポインターがボタンの上から離れたら
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ボタンの色を元に戻す
        Btn.color = defaultColor;

        // ボタンをアニメーション
        StartCoroutine(ButtonAnimation(false, 0.2f));
    }
    
    // ボタンがクリックされたら
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // ボタンをアニメーション
        StartCoroutine(ButtonAnimation(false, 0.2f));

        // ボタンごとの効果によって分岐
        switch (clickAction)
        {
            case ClickAction.ReturnToGame:  // ゲームに戻る
                ReturnToGame();
                break;
            case ClickAction.Setting:    // 設定画面を開く
                break;
            case ClickAction.ReturnToTitle: // タイトル画面に戻る
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
        uIController.DrawPauseUI(false);

        // マウスカーソルを非表示
        cursorController.DrawCursol(false);

        // 惑星情報UIを表示
        planetInfo.SetActive(true);

        // 時間の流れを元に戻す
        Time.timeScale = 1.0f;
    }

    // ボタンのアニメーション
    IEnumerator ButtonAnimation(bool onoff, float time)
    {
        Vector3[] startPos = new Vector3[3];       // 開始位置
        Vector3[] endPos = new Vector3[3];         // 終了位置
        Vector3 moveLength = new Vector3(5, 5, 0); // アニメーション時の移動距離
        float elapseTime = 0;                      // 経過時間

        // 時間が経過するまで繰り返す
        while (elapseTime < time)
        {
            elapseTime += Time.unscaledDeltaTime;

            // 時間が経過した割合(0 ～ 1)
            float t = elapseTime / time;

            if (onoff)
            {
                // 開始位置を設定
                startPos[0] = defaultPos[0];
                startPos[1] = defaultPos[1];
                startPos[2] = defaultPos[2];

                // 終了位置を設定
                endPos[0] = defaultPos[0] += moveLength;
                endPos[1] = defaultPos[1] += moveLength;
                endPos[2] = defaultPos[2] += moveLength;
            }
            else
            {
                // 開始位置を設定
                startPos[0] = Btn.rectTransform.position;
                startPos[1] = BtnOutline.rectTransform.position;
                startPos[2] = BtnText.rectTransform.position;

                // 終了位置を設定
                endPos[0] = defaultPos[0];
                endPos[1] = defaultPos[1];
                endPos[2] = defaultPos[2];
            }

            // 補完でアニメーション
            Btn.rectTransform.position = Vector3.Lerp(startPos[0], endPos[0], t);
            BtnOutline.rectTransform.position = Vector3.Lerp(startPos[1], endPos[1], t);
            BtnText.rectTransform.position = Vector3.Lerp(startPos[2], endPos[2], t);

            // 1フレーム待つ
            yield return null;
        }
    }
}
