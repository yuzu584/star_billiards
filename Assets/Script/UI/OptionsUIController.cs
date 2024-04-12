using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 設定画面のUIを管理
public class OptionsUIController : MonoBehaviour
{
    public BackButton backBtn;
    public GameObject[] lootObj;        // 階層ごとのプレハブ
    public GameObject lootObjIns;       // 階層ごとのインスタンス
    public GameObject lootParent;       // 階層の親オブジェクト

    private OptionsController opCon;
    private ScreenController scrCon;

    private void Start()
    {
        scrCon ??= ScreenController.instance;

        opCon.opUICon = this;
        opCon.SetBuckButtonAction(backBtn);

        // 階層が変わったら画面を切り替える
        scrCon.changeLoot += SwitchLoot;
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

        // インスタンス生成済みなら削除
        if (lootObjIns)
        {
            Destroy(lootObjIns);
            lootObjIns = null;
        }

        // 階層のインスタンスを生成
        lootObjIns = Instantiate(lootObj[scrCon.ScreenLoot]);
        lootObjIns.transform.SetParent(lootParent.transform, false);
    }

    void OnEnable()
    {
        opCon = OptionsController.instance;
        
        // 最初は Top を表示
        SwitchLoot();
    }
}
