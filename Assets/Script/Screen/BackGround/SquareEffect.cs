using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���V����l�p�`�̃G�t�F�N�g���Ǘ�
public class SquareEffect : MonoBehaviour
{
    private Image image;

    private BackGroundEffect bgEffect;

    private float rotationSpeed;            // ��]���x
    private float scaleDecleaseSpeed;       // �k�����x
    private bool scaling = true;            // �g�傩�k����( true : �g�� false : �k��)
    private Vector2 defaultScale;

    public bool fastDraw = false;           // �ŏ��̊g����X�L�b�v���đf�����`�悷�邩

    private void Start()
    {
        bgEffect = BackGroundEffect.instance;

        // ��]���x��ݒ�
        rotationSpeed = Random.Range(1.0f, 100.0f);

        // ������ Image �R���|�[�l���g���擾
        image = GetComponent<Image>();

        // �X�P�[���𗐐��Őݒ�
        float randScale = Random.Range(0.05f, 0.6f);
        defaultScale = new Vector2(randScale, randScale);

        // �k�����x��ݒ�
        scaleDecleaseSpeed = (defaultScale.x / Random.Range(1.0f, 5.0f));

        // �X�P�[���� 0 �ɂ���
        image.rectTransform.localScale *= 0;

        // ���W�𗐐��Őݒ�
        // ���݂� Canvas �̃T�C�Y����v�Z
        float scrX = Random.Range(bgEffect.canvasWidth / -2, bgEffect.canvasWidth / 2);
        float scrY = Random.Range(bgEffect.canvasHeight / -2, bgEffect.canvasHeight / 2);
        Vector3 pos = new Vector3(scrX, scrY, 0.0f);
        image.rectTransform.localPosition = pos;

        // �F��ݒ�
        image.color = bgEffect.effectColor;
    }

    private void Update()
    {
        // �f�����`�悷��Ȃ�g����X�L�b�v
        if (fastDraw && scaling)
        {
            image.rectTransform.localScale = defaultScale;
            scaling = false;
        }

        if (scaling)
        {
            // �X�P�[���� 1 �ȏ�Ȃ�k���J�n
            if (image.rectTransform.localScale.x >= 1)
            {
                scaling = false;
            }

            // ���񂾂�傫������
            Vector3 scaleDSpeed = new Vector3(scaleDecleaseSpeed, scaleDecleaseSpeed, scaleDecleaseSpeed);
            image.rectTransform.localScale += (scaleDSpeed * Time.unscaledDeltaTime);
        }
        else
        {
            // �X�P�[���� 0 �ȉ��Ȃ�j��
            if (image.rectTransform.localScale.x <= 0)
            {
                // �V���ɃG�t�F�N�g�𐶐�
                bgEffect.GenerateSquareEffect(false);
                Destroy(gameObject);
            }

            // ���񂾂񏬂�������
            Vector3 scaleDSpeed = new Vector3(scaleDecleaseSpeed, scaleDecleaseSpeed, scaleDecleaseSpeed);
            image.rectTransform.localScale -= (scaleDSpeed * Time.unscaledDeltaTime);
        }

        // ��]
        image.rectTransform.Rotate(0.0f, 0.0f, rotationSpeed * Time.unscaledDeltaTime);
    }
}
