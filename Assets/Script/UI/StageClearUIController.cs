using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ステージクリア画面のUIを管理
public class StageClearUIController : MonoBehaviour
{
    [SerializeField] private Material stageClearButtonMat;                // ボタンのマテリアル
    [SerializeField] private PostProcessController postProcessController; // InspectorでPostProcessControllerを指定
    [SerializeField] private UIController uIController;                   // InspectorでUIControllerを指定

    // ステージクリア画面のUIを描画
    public void DrawStageClearUI(bool draw, GameObject allStageClearUI, GameObject[] button, Text stageClearText)
    {
        // ステージクリア画面を表示/非表示
        allStageClearUI.SetActive(draw);

        // ゲーム画面のUIを表示/非表示
        uIController.inGameUI.allInGameUI.SetActive(!draw);

        // 被写界深度をON/OFF
        postProcessController.DepthOfFieldOnOff(draw);

        // ボタンを非表示
        for (int i = 0; i < button.Length; i++)
            button[i].SetActive(false);

        // ステージクリア画面を動かす
        StartCoroutine(MoveStageClearUI(
            stageClearText,
            button));
    }

    // ステージクリア画面を動かす
    IEnumerator MoveStageClearUI(
        Text stageClearText,
        GameObject[] button)
    {
        // テキストを動かす
        StartCoroutine(MoveStageClearText(
            stageClearText,
            new Vector3(300.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 0.0f, 0.0f),
            0.2f,
            true));

        // 一瞬待つ
        yield return new WaitForSeconds(2.0f);

        // テキストを動かす
        StartCoroutine(MoveStageClearText(
            stageClearText,
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 100.0f, 0.0f),
            0.3f,
            false));

        // ボタンを動かす
        StartCoroutine(MoveStageClearButton(
            button,
            0.3f));
    }

    // ステージクリア画面のテキストを動かす
    IEnumerator MoveStageClearText(
        Text stageClearText,
        Vector3 startPos,
        Vector3 endPos,
        float fadeTime,
        bool fadeColor)
    {
        float time = 0; // 経過時間

        // 指定した時間が経過するまで繰り返す
        while (time < fadeTime)
        {
            // 時間をカウント
            time += Time.unscaledDeltaTime;

            // 進み具合を計算
            float t = time / fadeTime;

            // テキスト移動
            stageClearText.transform.localPosition = Vector3.Lerp(startPos, endPos, t);

            // カラーフェードが有効なら透明度変化
            if ( fadeColor )
                stageClearText.color = Color32.Lerp(new Color32(255, 255, 255, 0), new Color32(255, 255, 255, 255), t);

            // 1フレーム待つ
            yield return null;
        }
    }

    // ステージクリア画面のボタンを動かす
    IEnumerator MoveStageClearButton(
        GameObject[] button,
        float fadeTime)
    {
        float time = 0;        // 経過時間

        // 初期位置を保存
        Vector3[] defaultPosition = new Vector3[button.Length];
        for (int i = 0; i < button.Length; i++)
            defaultPosition[i] = button[i].transform.position;

        // ボタンを表示
        for (int i = 0; i < button.Length; i++)
            button[i].SetActive(true);

        // 指定した時間が経過するまで繰り返す
        while (time < fadeTime)
        {
            // 時間をカウント
            time += Time.unscaledDeltaTime;

            // 進み具合を計算
            float t = time / fadeTime;

            // ボタン移動・透明度変化
            for (int i = 0; i < button.Length; i++)
            {
                button[i].transform.position = Vector3.Lerp(defaultPosition[i] + new Vector3(300.0f, 0.0f, 0.0f), defaultPosition[i], t);
                stageClearButtonMat.color = Color32.Lerp(new Color32(255, 255, 255, 0), new Color32(255, 255, 255, 255), t);
            }

            // 1フレーム待つ
            yield return null;
        }
    }
}
