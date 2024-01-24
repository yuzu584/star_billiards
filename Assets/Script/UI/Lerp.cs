using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ���`�⊮
public class Lerp : MonoBehaviour
{
    /*
    // �ėp�I�ȕ⊮
    private IEnumerator GenericLerp<T>(T value, T startVal, T endVal, float fadeTime)
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

            // �{�^���̐F��ύX
            value = T.Lerp(startVal, endVal, t);

            // 1�t���[���҂�
            yield return null;
        }
    }*/

    // ���`�⊮�ŐF��ύX
    public IEnumerator ChangeColor(Image image, Color colorA, Color colorB, float fadeTime)
    {
        float time = 0; // �o�ߎ��Ԃ��J�E���g

        // �w�肵�����Ԃ��o�߂���܂ŌJ��Ԃ�
        while (time < fadeTime)
        {
            // ���Ԃ��J�E���g
            time += Time.unscaledDeltaTime;

            // �i�݋���v�Z
            float t = time / fadeTime;

            // �{�^���̐F��ύX
            image.color = Color.Lerp(colorA, colorB, t);

            // 1�t���[���҂�
            yield return null;
        }
    }

    // ���`�⊮�ō��W��ύX(Image)
    public IEnumerator ChangePosition(Image obj, Vector3 startPos, Vector3 endPos, float fadeTime)
    {
        float time = 0; // �o�ߎ��Ԃ��J�E���g

        // �w�肵�����Ԃ��o�߂���܂ŌJ��Ԃ�
        while (time < fadeTime)
        {
            // ���Ԃ��J�E���g
            time += Time.unscaledDeltaTime;

            // �i�݋���v�Z
            float t = time / fadeTime;

            // �{�^���̐F��ύX
            obj.rectTransform.position = Vector3.Lerp(startPos, endPos, t);

            // 1�t���[���҂�
            yield return null;
        }
    }
}
