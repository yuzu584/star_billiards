using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundEffect : Singleton<BackGroundEffect>
{
    [SerializeField] private Image squareEffect;            // 四角形のエフェクトのプレハブ
    [SerializeField] private RectTransform canvasRect;

    private int effectAmount = 20;                          // 生成するエフェクトの数

    public float canvasWidth;                               // Canvas の幅
    public float canvasHeight;                              // Canvas の高さ

    private void Start()
    {
        canvasWidth = canvasRect.sizeDelta.x;
        canvasHeight = canvasRect.sizeDelta.y;
    }

    // エフェクトを生成して描画
    public void DrawEffect(Transform parent)
    {
        for (int i = 0; i < effectAmount; i++)
        {
            Image ins;

            // インスタンス生成
            ins = Instantiate(squareEffect);

            // 親を設定
            ins.transform.SetParent(parent, false);
        }
    }
}
