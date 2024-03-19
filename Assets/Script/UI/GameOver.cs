using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ゲームオーバー画面を管理
public class GameOver : Singleton<GameOver>
{
    [SerializeField] private ScreenController screenController;           // InspectorでScreenControllerを指定
    [SerializeField] private Material gameOverButtonMat;                  // ボタンのマテリアル
    [SerializeField] private PostProcessController postProcessController; // InspectorでPostProcessControllerを指定
    [SerializeField] private UIController uIController;                   // InspectorでUIControllerを指定

    private Lerp lerp;

    public float fadeTime = 0.4f; // フェード時間

    // ゲームオーバー処理
    public void GameOverProcess()
    {
        // ゲームオーバー画面に遷移
        screenController.ScreenNum = 9;
    }

    // ゲームオーバー画面のアニメーション
    void Animation()
    {
        // ボタンを非表示
        for (int i = 0; i < uIController.gameOverUI.button.Length; i++)
            uIController.gameOverUI.button[i].SetActive(false);

        // UIを動かす
        StartCoroutine(Move());
    }

    // UIを動かす
    IEnumerator Move()
    {
        Vector3[] defaultPos = new Vector3[uIController.gameOverUI.button.Length]; // 初期位置
        Vector3 startPos;   // 開始位置
        Vector3 endPos;     // 終了位置
        Color32 startColor; // 開始時の色
        Color32 endColor;   // 終了時の色

        // テキストを動かす
        startPos = new Vector3(300.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 0.0f, 0.0f);
        StartCoroutine(lerp.Position_Text(uIController.gameOverUI.GameOverText, startPos, endPos, fadeTime));

        // 一瞬待つ
        yield return new WaitForSecondsRealtime(2.0f);

        // テキストを動かす
        startPos = new Vector3(0.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 100.0f, 0.0f);
        StartCoroutine(lerp.Position_Text(uIController.gameOverUI.GameOverText, startPos, endPos, fadeTime));

        // ボタンを表示
        for (int i = 0; i < uIController.gameOverUI.button.Length; i++)
            uIController.gameOverUI.button[i].SetActive(true);

        // ボタンのアニメーション
        for (int i = 0; i < defaultPos.Length; ++i)
        {
            defaultPos[i] = uIController.gameOverUI.button[i].transform.localPosition;
        }

        for (int i = 0; i < uIController.gameOverUI.button.Length; ++i)
        {
            // ボタン移動
            startPos = defaultPos[i];
            endPos = defaultPos[i];
            StartCoroutine(lerp.Position_GameObject(uIController.gameOverUI.button[i], startPos, endPos, fadeTime));

            // 透明度変化
            startColor = new Color32(255, 255, 255, 0);
            endColor = new Color32(255, 255, 255, 255);
            StartCoroutine(lerp.Color_Material(gameOverButtonMat, startColor, endColor, fadeTime));
        }
    }

    void OnEnable()
    {
        if (lerp == null)
            lerp = gameObject.AddComponent<Lerp>();

        // アニメーション
        Animation();
    }
}
