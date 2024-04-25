using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundEffect : Singleton<BackGroundEffect>
{
    [SerializeField] private Image squareEffect;            // 四角形のエフェクトのプレハブ
    [SerializeField] private RectTransform canvasRect;
    public Material material;
    Color32 savedTopColor, savedButtomColor;                // マテリアルの初期色を保存

    private int effectAmount = 20;                          // 生成するエフェクトの数

    public Transform parentT;                               // 四角形エフェクトの親
    public float canvasWidth;                               // Canvas の幅
    public float canvasHeight;                              // Canvas の高さ
    public Color32 mainColor;                               // メインの背景色
    public Color32 buttomColor;                             // 背景下部の色
    public Color32 effectColor;                             // エフェクトの色
    public int maxMainColorRGB;                             // メインの背景色の RGB の最大値
    public int minMainColorRGB;                             // メインの背景色の RGB の最小値
    public int maxButtomColorRGB;                           // 背景下部の色の RGB の最大値
    public int minButtomColorRGB;                           // 背景下部の色の RGB の最小値
    public int maxEffectColorRGB;                           // エフェクトの色の RGB の最大値
    public int minEffectColorRGB;                           // エフェクトの色の RGB の最小値
    public int maxColorDiff;                                // メインの色から変化させる RGB の量の最大値
    public int minColorDiff;                                // メインの色から変化させる RGB の量の最小値

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

        // 背景のエフェクトを生成
        DrawEffect();

        // マテリアルの色を設定
        SetMaterialColor();
    }

#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        // アプリケーション終了時に保存しておいた色をマテリアルにセットして、
        // git の差分が発生しないようにする
        material.SetColor("_TopColor", savedTopColor);
        material.SetColor("_ButtomColor", savedButtomColor);
    }
#endif

    // エフェクトを生成して描画
    public void DrawEffect()
    {
        // 色を乱数で設定
        SetColor();

        // 四角形のエフェクトを生成
        for (int i = 0; i < effectAmount; i++)
        {
            GenerateSquareEffect(true);
        }
    }

    // 色を乱数で設定
    public void SetColor()
    {
        // メインの色を設定
        mainColor.r = (byte)Random.Range(minMainColorRGB, maxMainColorRGB);
        mainColor.g = (byte)Random.Range(minMainColorRGB, maxMainColorRGB);
        mainColor.b = (byte)Random.Range(minMainColorRGB, maxMainColorRGB);
        mainColor.a = 255;

        // メインの色を代入
        int mainR, mainG, mainB, mainA;
        mainR = mainColor.r;
        mainG = mainColor.g;
        mainB = mainColor.b;
        mainA = mainColor.a;

        // 背景の ButtomColor を設定( mainColor と少し違う色)
        int r, g, b, a;
        r = RandMin(mainR, maxColorDiff, minColorDiff);
        g = RandMin(mainG, maxColorDiff, minColorDiff);
        b = RandMin(mainB, maxColorDiff, minColorDiff);
        a = mainA;
        r = Mathf.Clamp(r, minButtomColorRGB, maxButtomColorRGB);
        g = Mathf.Clamp(g, minButtomColorRGB, maxButtomColorRGB);
        b = Mathf.Clamp(b, minButtomColorRGB, maxButtomColorRGB);
        buttomColor = new Color32((byte)r, (byte)g, (byte)b, (byte)a);

        // エフェクトの色をランダムで設定( mainColor, buttomColor と少し違う色)
        r = RandMin(mainR, maxColorDiff, minColorDiff);
        g = RandMin(mainG, maxColorDiff, minColorDiff);
        b = RandMin(mainB, maxColorDiff, minColorDiff);
        a = mainA;
        r = Mathf.Clamp(r, minEffectColorRGB, maxEffectColorRGB);
        g = Mathf.Clamp(g, minEffectColorRGB, maxEffectColorRGB);
        b = Mathf.Clamp(b, minEffectColorRGB, maxEffectColorRGB);
        effectColor = new Color32((byte)r, (byte)g, (byte)b, (byte)a);

        // マテリアルの色を設定
        SetMaterialColor();
    }

    // マテリアルの色を設定
    void SetMaterialColor()
    {
        // 背景の ButtomColor を設定
        material.SetColor("_TopColor", mainColor);
        material.SetColor("_ButtomColor", buttomColor);
    }

    // 指定の値から指定の値以上変化した乱数を返す
    // 例 : value = 10, minDiff = 5 の場合
    // 10 から +- 5 以上変化した数値が生成されたときのみ返す
    int RandMin(int value, int maxDiff, int minDiff)
    {
        int _value;
        int count = 1000;

        while (true)
        {
            // 1000 回乱数を生成しても終了条件を満たせなければループを抜けて 0 を返す
            if(count == 0)
            {
                _value = 0;
                Debug.Log("差が一定以上になりませんでした。");
                break;
            }

            // 乱数生成
            _value = Random.Range(value + -maxDiff, value + maxDiff);

            // 生成した乱数と value の差が minDiff 以上なら終了
            if (Mathf.Abs(_value - value) >= minDiff)
            {
                break;
            }

            --count;
        }

        return _value;
    }

    // 四角形のエフェクトを生成
    public void GenerateSquareEffect(bool fastDraw)
    {
        // 親オブジェクトが存在しなければ終了
        if (parentT == null) return;

        Image ins;

        // インスタンス生成
        ins = Instantiate(squareEffect);

        // 素早く描画
        if(fastDraw)
        {
            SquareEffect se = ins.GetComponent<SquareEffect>();
            se.fastDraw = true;
        }

        // 親を設定
        ins.transform.SetParent(parentT, false);
    }
}
