using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// タイトル画面を管理
public class TitleController : MonoBehaviour
{
    private ScreenController scrCon;
    private InputController input;

    void Start()
    {
        scrCon = ScreenController.instance;
        input = InputController.instance;
    }

    void Update()
    {
        // タイトル画面で何かしらの入力が行われたらメインメニューに遷移
        if ((Input.anyKey) && (scrCon.Screen == ScreenController.ScreenType.Title) && (input.canInput))
        {
            scrCon.Screen = ScreenController.ScreenType.MainMenu;
        }
    }
}
