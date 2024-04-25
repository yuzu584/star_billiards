using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundEffect : Singleton<BackGroundEffect>
{
    [SerializeField] private Image squareEffect;            // �l�p�`�̃G�t�F�N�g�̃v���n�u
    [SerializeField] private RectTransform canvasRect;
    public Material material;
    Color32 savedTopColor, savedButtomColor;                // �}�e���A���̏����F��ۑ�

    private int effectAmount = 20;                          // ��������G�t�F�N�g�̐�

    public Transform parentT;                               // �l�p�`�G�t�F�N�g�̐e
    public float canvasWidth;                               // Canvas �̕�
    public float canvasHeight;                              // Canvas �̍���
    public Color32 mainColor;                               // ���C���̔w�i�F
    public Color32 buttomColor;                             // �w�i�����̐F
    public Color32 effectColor;                             // �G�t�F�N�g�̐F
    public int maxMainColorRGB;                             // ���C���̔w�i�F�� RGB �̍ő�l
    public int minMainColorRGB;                             // ���C���̔w�i�F�� RGB �̍ŏ��l
    public int maxButtomColorRGB;                           // �w�i�����̐F�� RGB �̍ő�l
    public int minButtomColorRGB;                           // �w�i�����̐F�� RGB �̍ŏ��l
    public int maxEffectColorRGB;                           // �G�t�F�N�g�̐F�� RGB �̍ő�l
    public int minEffectColorRGB;                           // �G�t�F�N�g�̐F�� RGB �̍ŏ��l
    public int maxColorDiff;                                // ���C���̐F����ω������� RGB �̗ʂ̍ő�l
    public int minColorDiff;                                // ���C���̐F����ω������� RGB �̗ʂ̍ŏ��l

    override protected void Awake()
    {
        base.Awake();

        savedTopColor = material.GetColor("_TopColor");
        savedButtomColor = material.GetColor("_ButtomColor");
    }

    private void Start()
    {
        canvasWidth = canvasRect.sizeDelta.x;
        canvasHeight = canvasRect.sizeDelta.y;

        // �w�i�̃G�t�F�N�g�𐶐�
        DrawEffect();

        // �}�e���A���̐F��ݒ�
        SetMaterialColor();
    }

#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        // �A�v���P�[�V�����I�����ɕۑ����Ă������F���}�e���A���ɃZ�b�g���āA
        // git �̍������������Ȃ��悤�ɂ���
        material.SetColor("_TopColor", savedTopColor);
        material.SetColor("_ButtomColor", savedButtomColor);
    }
#endif

    // �G�t�F�N�g�𐶐����ĕ`��
    public void DrawEffect()
    {
        // �F�𗐐��Őݒ�
        SetColor();

        // �l�p�`�̃G�t�F�N�g�𐶐�
        for (int i = 0; i < effectAmount; i++)
        {
            GenerateSquareEffect(true);
        }
    }

    // �F�𗐐��Őݒ�
    public void SetColor()
    {
        // ���C���̐F��ݒ�
        mainColor.r = (byte)Random.Range(minMainColorRGB, maxMainColorRGB);
        mainColor.g = (byte)Random.Range(minMainColorRGB, maxMainColorRGB);
        mainColor.b = (byte)Random.Range(minMainColorRGB, maxMainColorRGB);
        mainColor.a = 255;

        // ���C���̐F����
        int mainR, mainG, mainB, mainA;
        mainR = mainColor.r;
        mainG = mainColor.g;
        mainB = mainColor.b;
        mainA = mainColor.a;

        // �w�i�� ButtomColor ��ݒ�( mainColor �Ə����Ⴄ�F)
        int r, g, b, a;
        r = RandMin(mainR, maxColorDiff, minColorDiff);
        g = RandMin(mainG, maxColorDiff, minColorDiff);
        b = RandMin(mainB, maxColorDiff, minColorDiff);
        a = mainA;
        r = Mathf.Clamp(r, minButtomColorRGB, maxButtomColorRGB);
        g = Mathf.Clamp(g, minButtomColorRGB, maxButtomColorRGB);
        b = Mathf.Clamp(b, minButtomColorRGB, maxButtomColorRGB);
        buttomColor = new Color32((byte)r, (byte)g, (byte)b, (byte)a);

        // �G�t�F�N�g�̐F�������_���Őݒ�( mainColor, buttomColor �Ə����Ⴄ�F)
        r = RandMin(mainR, maxColorDiff, minColorDiff);
        g = RandMin(mainG, maxColorDiff, minColorDiff);
        b = RandMin(mainB, maxColorDiff, minColorDiff);
        a = mainA;
        r = Mathf.Clamp(r, minEffectColorRGB, maxEffectColorRGB);
        g = Mathf.Clamp(g, minEffectColorRGB, maxEffectColorRGB);
        b = Mathf.Clamp(b, minEffectColorRGB, maxEffectColorRGB);
        effectColor = new Color32((byte)r, (byte)g, (byte)b, (byte)a);

        // �}�e���A���̐F��ݒ�
        SetMaterialColor();
    }

    // �}�e���A���̐F��ݒ�
    void SetMaterialColor()
    {
        // �w�i�� ButtomColor ��ݒ�
        material.SetColor("_TopColor", mainColor);
        material.SetColor("_ButtomColor", buttomColor);
    }

    // �w��̒l����w��̒l�ȏ�ω�����������Ԃ�
    // �� : value = 10, minDiff = 5 �̏ꍇ
    // 10 ���� +- 5 �ȏ�ω��������l���������ꂽ�Ƃ��̂ݕԂ�
    int RandMin(int value, int maxDiff, int minDiff)
    {
        int _value;
        int count = 1000;

        while (true)
        {
            // 1000 �񗐐��𐶐����Ă��I�������𖞂����Ȃ���΃��[�v�𔲂��� 0 ��Ԃ�
            if(count == 0)
            {
                _value = 0;
                Debug.Log("�������ȏ�ɂȂ�܂���ł����B");
                break;
            }

            // ��������
            _value = Random.Range(value + -maxDiff, value + maxDiff);

            // �������������� value �̍��� minDiff �ȏ�Ȃ�I��
            if (Mathf.Abs(_value - value) >= minDiff)
            {
                break;
            }

            --count;
        }

        return _value;
    }

    // �l�p�`�̃G�t�F�N�g�𐶐�
    public void GenerateSquareEffect(bool fastDraw)
    {
        // �e�I�u�W�F�N�g�����݂��Ȃ���ΏI��
        if (parentT == null) return;

        Image ins;

        // �C���X�^���X����
        ins = Instantiate(squareEffect);

        // �f�����`��
        if(fastDraw)
        {
            SquareEffect se = ins.GetComponent<SquareEffect>();
            se.fastDraw = true;
        }

        // �e��ݒ�
        ins.transform.SetParent(parentT, false);
    }
}
