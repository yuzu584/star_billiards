using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// タイトル画面を管理
public class TitleController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定

    void Update()
    {
        if (Input.anyKey)
        {
            screenController.screenNum = 1;
        }
    }
}
