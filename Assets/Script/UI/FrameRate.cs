using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �t���[�����[�g���Ǘ�
public class FrameRate : MonoBehaviour
{
    [SerializeField] private Text FPSText; // �t���[�����[�g��\���e�L�X�g

    private int frameCount = 0;            // �o�߂����t���[���𐔂���
    private float prevTime = 0.0f;         // �v���J�n�������Ԃ�ۑ�
    private float waitTime = 0.2f;         // �t���[�����[�g�̕`��Ԋu

    void Update()
    {
        // �o�߂����t���[���𐔂���
        ++frameCount;

        // �o�ߎ��Ԃ��v�Z����
        float time = Time.realtimeSinceStartup - prevTime;

        // �o�ߎ��Ԃ���莞�Ԉȏ�Ȃ�FPS�̒l���X�V
        if (time >= waitTime)
        {
            FPSText.text = (frameCount / time).ToString("0") + " FPS";

            // ���l��������
            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }
}
