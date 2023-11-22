using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 惑星の量を管理
public class PlanetAmount : MonoBehaviour
{
    public int planetDestroyAmount = 0; // 惑星を破壊した数

    // 惑星が破壊された際のポップアップを描画するコルーチンを呼び出す
    public void DrawDestroyPlanetPopup(PopupController popupController, string name)
    {
        StartCoroutine(popupController.DrawDestroyPlanetPopup(name + " was destroyed"));
    }
}
