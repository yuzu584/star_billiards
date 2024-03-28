using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 設定画面を管理
public class OptionsController : Singleton<OptionsController>
{
    public enum Loot   // 階層
    {
        Top = 0,       // 最初の画面
        GamePlay = 1,  // ゲーム内設定
        Video = 2,     // ビデオ設定
        Audio = 3,     // オーディオ設定
        KeyConfig = 4, // キー配置設定
        Language = 5,  // 言語設定
    }
    public Loot loot = 0;
    public Loot oldLoot = 0;        // 1フレーム前の階層

    public OptionsUIController opUICon;

    // 表示する階層を切り替え
    public void SwitchLoot(Loot l)
    {
        if (opUICon == null) return;

        // 階層を設定
        loot = l;
    }
}
