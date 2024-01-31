using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ���`�⊮
public class Lerp : MonoBehaviour
{
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

            // �R�[���o�b�N���Ăяo��
            lerpFunction(t);

            // 1�t���[���҂�
            yield return null;
        }
    }

    // ���`�⊮�ŐF��ύX
    public IEnumerator ChangeColor(Image image, Color colorA, Color colorB, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            image.color = Color.Lerp(colorA, colorB, t);
        };

        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }

    // ���`�⊮�ō��W��ύX(Image)
    public IEnumerator ChangePosition(Image obj, Vector3 startPos, Vector3 endPos, float fadeTime)
    {
        Action<float> lerpFunction = (float t) =>
        {
            obj.rectTransform.position = Vector3.Lerp(startPos, endPos, t);
        };
        yield return StartCoroutine(GenericLerp(fadeTime, lerpFunction));
    }
}
