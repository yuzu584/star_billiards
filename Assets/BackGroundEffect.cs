using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundEffect : Singleton<BackGroundEffect>
{
    [SerializeField] private Image squareEffect;            // �l�p�`�̃G�t�F�N�g�̃v���n�u
    [SerializeField] private RectTransform canvasRect;

    private int effectAmount = 20;                          // ��������G�t�F�N�g�̐�

    public float canvasWidth;                               // Canvas �̕�
    public float canvasHeight;                              // Canvas �̍���

    private void Start()
    {
        canvasWidth = canvasRect.sizeDelta.x;
        canvasHeight = canvasRect.sizeDelta.y;
    }

    // �G�t�F�N�g�𐶐����ĕ`��
    public void DrawEffect(Transform parent)
    {
        for (int i = 0; i < effectAmount; i++)
        {
            Image ins;

            // �C���X�^���X����
            ins = Instantiate(squareEffect);

            // �e��ݒ�
            ins.transform.SetParent(parent, false);
        }
    }
}
