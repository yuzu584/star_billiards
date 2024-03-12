using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// タイトル画面を管理
public class TitleController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定
    [SerializeField] private InputController input;             // InspectorでInputを指定

    void Update()
    {
        // タイトル画面で何かしらの入力が行われたらメインメニューに遷移
        if ((Input.anyKey) && (screenController.ScreenNum == 0) && (input.canInput))
        {
            screenController.ScreenNum = 1;
        }
    }
}
