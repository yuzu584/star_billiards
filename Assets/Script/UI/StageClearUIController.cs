using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ステージクリア画面のUIを管理
public class StageClearUIController : Lerp
{
    [SerializeField] private Material stageClearButtonMat;                // ボタンのマテリアル
    [SerializeField] private PostProcessController postProcessController; // InspectorでPostProcessControllerを指定
    [SerializeField] private UIController uIController;                   // InspectorでUIControllerを指定
    [SerializeField] private Lerp lerp;                                   // InspectorでLerpを指定

    public float fadeTime = 0.4f; // フェード時間

    // ステージクリア画面のUIを描画
    public void DrawStageClearUI()
    {
        // ボタンを非表示
        for (int i = 0; i < uIController.stageClearUI.button.Length; i++)
            uIController.stageClearUI.button[i].SetActive(false);

        // ステージクリア画面を動かす
        StartCoroutine(MoveStageClearUI());
    }

    // ステージクリア画面を動かす
    IEnumerator MoveStageClearUI()
    {
        Vector3[] defaultPos = new Vector3[uIController.stageClearUI.button.Length]; // 初期位置
        Vector3 startPos;   // 開始位置
        Vector3 endPos;     // 終了位置
        Color32 startColor; // 開始時の色
        Color32 endColor;   // 終了時の色

        // テキストを動かす
        startPos = new Vector3(300.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 0.0f, 0.0f);
        StartCoroutine(Position_Text(uIController.stageClearUI.stageClearText, startPos, endPos, fadeTime));

        // 一瞬待つ
        yield return new WaitForSecondsRealtime(2.0f);

        // テキストを動かす
        startPos = new Vector3(0.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 100.0f, 0.0f);
        StartCoroutine(Position_Text(uIController.stageClearUI.stageClearText, startPos, endPos, fadeTime));

        // ボタンを表示
        for (int i = 0; i < uIController.stageClearUI.button.Length; i++)
            uIController.stageClearUI.button[i].SetActive(true);

        // ボタンのアニメーション
        for (int i = 0; i < defaultPos.Length; ++i)
        {
            defaultPos[i] = uIController.stageClearUI.button[i].transform.localPosition;
        }

        for (int i = 0; i < uIController.stageClearUI.button.Length; ++i)
        {
            // ボタン移動
            startPos = defaultPos[i] + new Vector3(300.0f, 0.0f, 0.0f);
            endPos = defaultPos[i];
            StartCoroutine(Position_GameObject(uIController.stageClearUI.button[i], startPos, endPos, fadeTime));

            // 透明度変化
            startColor = new Color32(255, 255, 255, 0);
            endColor = new Color32(255, 255, 255, 255);
            StartCoroutine(Color_Material(stageClearButtonMat, startColor, endColor, fadeTime));
        }
    }
}
