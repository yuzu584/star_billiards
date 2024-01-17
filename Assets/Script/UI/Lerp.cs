using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ���`�⊮
public class Lerp : MonoBehaviour
{
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
}
