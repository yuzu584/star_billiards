using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// メインメニューのUIを管理
public class MainMenuUIController : MonoBehaviour
{
    // メインメニューを表示/非表示
    public void DrawMainMenu(bool draw, GameObject allMainMenuUI)
    {
        // 表示/ 非表示切り替え
        if(allMainMenuUI.activeSelf != draw)
            allMainMenuUI.SetActive(draw);
    }
}
