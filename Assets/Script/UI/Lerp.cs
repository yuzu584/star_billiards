using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// 線形補完
public class Lerp : MonoBehaviour
{
    // 汎用的な補完
    private IEnumerator GenericLerp(float fadeTime, Action<float> lerpFunction)
    {
        float time = 0; // 経過時間をカウント
        float t = 0;    // 進み具合

        // 指定した時間が経過するまで繰り返す
        while (time < fadeTime)
        {
            // 時間をカウント
            time += Time.unscaledDeltaTime;

            // 進み具合を計算
            t = time / fadeTime;

            // コールバックを呼び出す
            lerpFunction(t);

            // 1フレーム待つ
            yield return null;
        }
    }

    // 線形補完で色を変更(Image)
    public IEnumerator Color_Image(Image image, Color colorA, Color colorB, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            image.color = Color.Lerp(colorA, colorB, t);
        };

        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }

    // 線形補完で色を変更(Material)
    public IEnumerator Color_Material(Material mat, Color32 colorA, Color32 colorB, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            mat.color = Color.Lerp(colorA, colorB, t);
        };

        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }

    // 線形補完で座標を変更(Image)
    public IEnumerator Position_Image(Image obj, Vector3 startPos, Vector3 endPos, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            obj.rectTransform.position = Vector3.Lerp(startPos, endPos, t);
        };
        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }

    // 線形補完で座標を変更(Text)
    public IEnumerator Position_Text(Text obj, Vector3 startPos, Vector3 endPos, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            obj.rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
        };
        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }

    // 線形補完で座標を変更(GameObject)
    public IEnumerator Position_GameObject(GameObject obj, Vector3 startPos, Vector3 endPos, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            obj.transform.position = Vector3.Lerp(startPos, endPos, t);
        };
        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }
}
