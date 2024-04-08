using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 背景色を管理
public class BackGroundColor : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Material material;

    public Color32 mainColor;       // メインの色

    // 色をランダムで設定
    void Start()
    {
        // メインの色を設定
        mainColor.r = (byte)Random.Range(10, 100);
        mainColor.g = (byte)Random.Range(10, 100);
        mainColor.b = (byte)Random.Range(10, 100);
        mainColor.a = 255;

        // 背景の ButtomColor を設定( mainColor と少し違う色)
        int r, g, b, a;
        r = (mainColor.r + Random.Range(mainColor.r, 50 - mainColor.r));
        g = (mainColor.g + Random.Range(mainColor.g, 50 - mainColor.g));
        b = (mainColor.b + Random.Range(mainColor.b, 50 - mainColor.b));
        a = mainColor.a;
        Mathf.Clamp(r, 10, 100);
        Mathf.Clamp(g, 10, 100);
        Mathf.Clamp(b, 10, 100);
        Color32 buttomColor = new Color32((byte)r, (byte)g, (byte)b, (byte)a);
        material.SetColor("_TopColor", mainColor);
        material.SetColor("_ButtomColor", buttomColor);
    }
}
