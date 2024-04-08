using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���V����l�p�`�̃G�t�F�N�g���Ǘ�
public class SquareEffect : MonoBehaviour
{
    private Image image;

    private BackGroundEffect bgEffect;

    private float rotationSpeed;

    private void Start()
    {
        bgEffect = BackGroundEffect.instance;

        // ��]���x��ݒ�
        rotationSpeed = Random.Range(0.1f, 100.0f);

        // ������ Image �R���|�[�l���g���擾
        image = GetComponent<Image>();

        // �傫���𗐐��Őݒ�
        float randScale = Random.Range(0.1f, 1.2f);
        Vector2 scale = new Vector3(randScale, randScale);
        image.rectTransform.localScale = scale;

        // ���W�𗐐��Őݒ�
        // ���݂� Canvas �̃T�C�Y����v�Z
        float scrX = Random.Range(bgEffect.canvasWidth / -2, bgEffect.canvasWidth / 2);
        float scrY = Random.Range(bgEffect.canvasHeight / -2, bgEffect.canvasHeight / 2);
        Vector3 pos = new Vector3(scrX, scrY, 0.0f);
        image.rectTransform.localPosition = pos;
    }

    private void Update()
    {
        // ��]
        image.rectTransform.Rotate(0.0f, 0.0f, rotationSpeed * Time.unscaledDeltaTime);
    }
}
