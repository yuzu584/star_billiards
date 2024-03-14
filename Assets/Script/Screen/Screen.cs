using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// これをGameObjectにアタッチすると指定した画面番号の時のみ表示されるようになる
public class Screen : MonoBehaviour
{
    [SerializeField] private int num = 0;                       // このGameObjectを表示する画面番号
    [SerializeField] private int loot = 0;                      // このGameObjectを表示する画面の階層
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定
    [SerializeField] private bool resetFov = false;             // スクリーンアクティブ時に視野角を初期値にするか

    // 表示する画面を切り替え
    public void SwitchScreen()
    {
        // 画面番号がnumと同じかつ画面の階層がlootと同じなら表示
        if((screenController.ScreenNum == num) && (screenController.ScreenLoot == loot) && (!gameObject.activeSelf))
        {
            gameObject.SetActive(true);

            if(resetFov)
            {
                // 視野角を初期値にする
                FOV.instance.ResetFOV();
            }
        }
        // 違うなら非表示
        else if (((screenController.ScreenNum != num) || (screenController.ScreenLoot != loot)) && (gameObject.activeSelf))
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        screenController.changeScreen += SwitchScreen;
        SwitchScreen();
    }
}
