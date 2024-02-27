using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スクリーンの親クラス
public class Screen : MonoBehaviour, IScreen
{
    [SerializeField] protected int num = 0;                       // このスクリーンを表示する画面番号
    [SerializeField] protected ScreenController screenController; // InspectorでScreenControllerを指定

    // 表示する画面を切り替え
    public void SwitchScreen()
    {
        // 画面番号がnumと同じなら表示
        if((screenController.screenNum == num) && (!gameObject.activeSelf))
        {
            gameObject.SetActive(true);
        }
        // 違うなら非表示
        else if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
