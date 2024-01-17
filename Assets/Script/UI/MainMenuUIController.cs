using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Const;

// メインメニューのUIを管理
public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject btn;       // ボタンのプレハブ
    [SerializeField] private GameObject parentObj; // ボタンのプレハブの親オブジェクト

    void Start()
    {
        // ボタンのインスタンスを生成して初期設定を行う
        for (int i = 0; i < AppConst.MAINMENU_BTN_TEXT.Length; i++)
        {
            // インスタンスを生成
            GameObject btnPrefab = Instantiate(btn);

            // 名前を設定
            btnPrefab.name = AppConst.MAINMENU_BTN_TEXT[i];

            // 位置を設定
            btnPrefab.transform.position = new Vector3(-300.0f, 0.0f + (i * -20), 0.0f);

            // 親を設定
            btnPrefab.transform.SetParent(parentObj.transform, false);

            // テキストを取得
            Text btnText = btnPrefab.transform.GetChild(1).GetComponent<Text>();

            // テキストを設定
            btnText.text = AppConst.MAINMENU_BTN_TEXT[i];

            // ボタンを押したときの効果を設定
            ButtonController buttonController = btnPrefab.transform.GetChild(0).GetComponent<ButtonController>();
            switch (AppConst.MAINMENU_BTN_TEXT[i])
            {
                case "StageSelect":
                    buttonController.clickAction = ButtonController.ClickAction.StageSelect;
                    break;
                case "Setting":
                    buttonController.clickAction = ButtonController.ClickAction.Setting;
                    break;
                case "SkillSelect":
                    buttonController.clickAction = ButtonController.ClickAction.SkillSelect;
                    break;
                default:
                    buttonController.clickAction = ButtonController.ClickAction.None;
                    break;
            }
        }
    }

    // メインメニューを表示/非表示
    public void DrawMainMenu(bool draw, GameObject allMainMenuUI)
    {
        // 表示/ 非表示切り替え
        if(allMainMenuUI.activeSelf != draw)
            allMainMenuUI.SetActive(draw);
    }
}
