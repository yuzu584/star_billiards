using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���j���[��ʂ̃|�b�v�A�b�v1
public class InMenuPopup1 : PopupParent
{
    [SerializeField] private Text messageText;              // �e�L�X�g
    [SerializeField] private Image image;                   // �摜
    [SerializeField] private float destroyTime = 3.0f;      // �|�b�v�A�b�v��������܂ł̎���

    protected override void Start()
    {
        base.Start();
    }

    // �|�b�v�A�b�v�̏���
    public override IEnumerator Process(string text, Transform parentT, int num)
    {
        Color[] startColor = new Color[2];
        Color[] endColor = new Color[2];
        startColor[0] = new(0, 0, 0, 0);
        startColor[1] = new(1, 1, 1, 0);
        endColor[0] = new(0, 0, 0, 0.78f);
        endColor[1] = new(1, 1, 1, 1);
        float fadeTime = 0.2f;;

        index = num;

        // �e�I�u�W�F�N�g��ݒ�
        gameObject.transform.SetParent(parentT, false);

        // �e�L�X�g��ݒ�
        messageText.text = text;

        lerp ??= gameObject.AddComponent<Lerp>();

        // ���`��ԂŃA�j���[�V����
        StartCoroutine(lerp.Color_Image(image, startColor[0], endColor[0], fadeTime));
        StartCoroutine(lerp.Color_Text(messageText, startColor[1], endColor[1], fadeTime));

        // ���b�҂�
        yield return new WaitForSecondsRealtime(destroyTime);

        // ���`��ԂŃA�j���[�V����
        StartCoroutine(lerp.Color_Image(image, endColor[0], startColor[0], fadeTime));
        StartCoroutine(lerp.Color_Text(messageText, endColor[1], startColor[1], fadeTime));

        // ���b�҂�
        yield return new WaitForSecondsRealtime(fadeTime);

        // ������j��
        Destroy();
    }
}
