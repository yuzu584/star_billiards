using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 画面遷移時の処理
public class ScreenSwitch : Singleton<ScreenSwitch>
{
    void Update()
    {
        // 前回のフレームと現在のフレームで画面番号が異なったら
        if (ScreenController.instance.ScreenNum != ScreenController.instance.oldFrameScreenNum)
        {
            // 前回の画面番号を保存
            ScreenController.instance.oldScreenNum = ScreenController.instance.oldFrameScreenNum;

            // 1フレーム前の画面番号に現在の画面番号を代入
            ScreenController.instance.oldFrameScreenNum = ScreenController.instance.ScreenNum;

            // 画面遷移したときの処理
            if (ScreenController.instance.changeScreen != null)
                ScreenController.instance.changeScreen();
        }

        // 前回のフレームと現在のフレームで階層が異なったら
        if (ScreenController.instance.ScreenLoot != ScreenController.instance.oldFrameScreenLoot)
        {
            // 前回の階層を保存
            ScreenController.instance.oldScreenLoot = ScreenController.instance.oldFrameScreenLoot;

            // 1フレーム前の階層に現在の階層を代入
            ScreenController.instance.oldFrameScreenLoot = ScreenController.instance.ScreenLoot;

            // 階層が遷移したときの処理
            if (ScreenController.instance.changeLoot != null)
                ScreenController.instance.changeLoot();
        }
    }
}
