using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ���`�⊮
public class Lerp : MonoBehaviour
{
    // �S�ẴR���[�`�����~
    public void StopAll()
    {
        StopAllCoroutines();
    }

    // �ėp�I�ȕ⊮
    private IEnumerator GenericLerp(float fadeTime, Action<float> lerpFunction)
    {
        float time = 0; // �o�ߎ��Ԃ��J�E���g
        float t = 0;    // �i�݋

        // �w�肵�����Ԃ��o�߂���܂ŌJ��Ԃ�
        while (time < fadeTime)
        {
            // ���Ԃ��J�E���g
            time += Time.unscaledDeltaTime;

            // �i�݋���v�Z
            t = time / fadeTime;
            t = easeOutQuint(t);

            // �R�[���o�b�N���Ăяo��
            lerpFunction(t);

            // 1�t���[���҂�
            yield return null;
        }
    }

    // ���`�⊮�ŐF��ύX(Image)
    public IEnumerator Color_Image(Image image, Color colorA, Color colorB, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            image.color = Color.Lerp(colorA, colorB, t);
        };

        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }

    // ���`�⊮�ŐF��ύX(Text)
    public IEnumerator Color_Text(Text text, Color colorA, Color colorB, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            text.color = Color.Lerp(colorA, colorB, t);
        };

        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }

    // ���`�⊮�ŐF��ύX(Material)
    public IEnumerator Color_Material(Material mat, Color32 colorA, Color32 colorB, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            mat.color = Color.Lerp(colorA, colorB, t);
        };

        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }

    // ���`�⊮�ō��W��ύX(Image)
    public IEnumerator Position_Image(Image obj, Vector3 startPos, Vector3 endPos, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            obj.rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
        };
        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }

    // ���`�⊮�ō��W��ύX(Text)
    public IEnumerator Position_Text(Text obj, Vector3 startPos, Vector3 endPos, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            obj.rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
        };
        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }

    // ���`�⊮�ō��W��ύX(GameObject)
    public IEnumerator Position_GameObject(GameObject obj, Vector3 startPos, Vector3 endPos, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            obj.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
        };
        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }

    // ���`�⊮�ŃX�P�[����ύX(Image)
    public IEnumerator Scale_Image(Image obj, Vector2 startScale, Vector2 endScale, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            obj.rectTransform.localScale = Vector2.Lerp(startScale, endScale, t);
        };
        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }

    // ���`�⊮�ŃX�P�[����ύX(Text)
    public IEnumerator Scale_Text(Text obj, Vector2 startScale, Vector2 endScale, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            obj.rectTransform.localScale = Vector2.Lerp(startScale, endScale, t);
        };
        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }

    // ���炩�Ȑ��`�⊮
    private float easeOutQuint(float t)
    {
        return 1 - Mathf.Pow(1 - t, 5);
    }
}
