using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Button;

// 設定画面を管理
public class OptionsController : Singleton<OptionsController>
{
    public OptionsUIController opUICon;
    private ScreenController scrCon;
    private InputController input;

    private void Start()
    {
        input = InputController.instance;
        scrCon = ScreenController.instance;
    }

    // 表示する階層を切り替え
    public void SwitchLoot(int loot)
    {
        if (opUICon == null) return;

        // 階層を設定
        scrCon.ScreenLoot = loot;
    }

    // 戻るボタンのクリック時の動作を変更
    public void SetBuckButtonAction(BackButton backBtn)
    {
        backBtn.action = () =>
        {
            if (scrCon.ScreenLoot == 0)
            {
                // 階層が0以下かつオブジェクトが有効なら
                if ((scrCon.ScreenLoot <= 0) && (backBtn.gameObject.activeInHierarchy))
                {
                    //音を再生
                    backBtn.PlayBtnSound(BtnSounds.ClickSound);

                    // 前の画面に戻る
                    scrCon.Screen = backBtn.oldScreen;
                }
            }
            else if (scrCon.ScreenLoot > 0)
            {
                SwitchLoot(0);
            }
        };
    }
}
