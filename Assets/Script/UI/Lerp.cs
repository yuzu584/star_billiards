using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// 線形補完
public class Lerp : MonoBehaviour
{
    // 線形補完で色を変更
    public IEnumerator ChangeColor(Image image, Color colorA, Color colorB, float fadeTime)
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
            image.color = Color.Lerp(colorA, colorB, t);

            // 1フレーム待つ
            yield return null;
        }
    }

    // 線形補完で座標を変更(GameObject)
    public IEnumerator ChangePosition(GameObject obj, Vector3 startPos, Vector3 endPos, float fadeTime)
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
            obj.transform.position = Vector3.Lerp(startPos, endPos, t);

            // 1フレーム待つ
            yield return null;
        }
    }

    // 線形補完で座標を変更(Image)
    public IEnumerator ChangePosition(Image obj, Vector3 startPos, Vector3 endPos, float fadeTime)
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
            obj.rectTransform.position = Vector3.Lerp(startPos, endPos, t);

            // 1フレーム待つ
            yield return null;
        }
    }
}
