using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 背景色を管理
public class BackGroundColor : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Material material;
    [SerializeField] private Transform parentT;
    private BackGroundEffect bgEffect;

    // 色をランダムで設定
    private void Start()
    {
        bgEffect = BackGroundEffect.instance;

        // 背景のエフェクトを生成
        bgEffect.parentT = parentT;
        bgEffect.DrawEffect();

        // 背景の ButtomColor を設定
        material.SetColor("_TopColor", bgEffect.mainColor);
        material.SetColor("_ButtomColor", bgEffect.buttomColor);
    }
}
