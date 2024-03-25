using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 画面遷移時の処理
public class ScreenSwitch : Singleton<ScreenSwitch>
{
    private ScreenController scrCon;

    void Start()
    {
        scrCon = ScreenController.instance;
    }

    void Update()
    {
        // 前回のフレームと現在のフレームで画面が異なったら
        if (scrCon.Screen != scrCon.oldFrameScreen)
        {
            // 前回の画面を保存
            scrCon.oldScreen = scrCon.oldFrameScreen;

            // 1フレーム前の画面に現在の画面を代入
            scrCon.oldFrameScreen = scrCon.Screen;

            // 画面遷移したときの処理
            if (scrCon.changeScreen != null)
                scrCon.changeScreen();
        }

        // 前回のフレームと現在のフレームで階層が異なったら
        if (scrCon.ScreenLoot != scrCon.oldFrameScreenLoot)
        {
            // 前回の階層を保存
            scrCon.oldScreenLoot = scrCon.oldFrameScreenLoot;

            // 1フレーム前の階層に現在の階層を代入
            scrCon.oldFrameScreenLoot = scrCon.ScreenLoot;

            // 階層が遷移したときの処理
            if (scrCon.changeLoot != null)
                scrCon.changeLoot();
        }
    }
}
