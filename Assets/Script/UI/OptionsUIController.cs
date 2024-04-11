using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 設定画面のUIを管理
public class OptionsUIController : MonoBehaviour
{
    public BackButton buckBtn;
    public GameObject[] lootObj;    // 階層ごとのゲームオブジェクト

    private OptionsController opCon;
    private ScreenController scrCon;

    private void Start()
    {
        scrCon ??= ScreenController.instance;

        opCon.opUICon = this;
        opCon.SetBuckButtonAction();
    }

    private void OnDestroy()
    {
        scrCon.changeLoot -= SwitchLoot;
        opCon.opUICon = null;
    }

    // 表示する階層を切り替え
    private void SwitchLoot()
    {
        scrCon ??= ScreenController.instance;

        // 階層ごとのオブジェクトの数繰り返す
        for (int i = 0; i < lootObj.Length; ++i)
        {
            // 表示する階層なら表示
            if ((i == scrCon.ScreenLoot) && (lootObj[i]))
                lootObj[i].SetActive(true);
            // 非表示
            else if (lootObj[i])
                lootObj[i].SetActive(false);
        }
    }

    void OnEnable()
    {
        opCon = OptionsController.instance;

        // 最初は Top を表示
        SwitchLoot();
    }

    void Update()
    {
        // 階層が変わったら画面を切り替える
        scrCon.changeLoot += SwitchLoot;
    }
}
