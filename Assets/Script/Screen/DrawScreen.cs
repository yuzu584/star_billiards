using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スクリーンを描画する
public class DrawScreen : MonoBehaviour
{
    [SerializeField] private ScreenData scrData;
    [SerializeField] private GameObject parentObj;

    private ScreenController scrCon;
    private GameObject Ins;                // スクリーンのインスタンス

    private void Start()
    {
        scrCon = ScreenController.instance;

        // 画面遷移時にスクリーンを描画
        scrCon.changeScreen += Draw;

        Draw();
    }

    // スクリーンを描画
    void Draw()
    {
        // 前回のスクリーンのインスタンスを削除
        if (Ins)
        {
            Destroy(Ins);
            Ins = null;
        }

        // スクリーンのインスタンスを生成
        Ins = Instantiate(scrData.screenList[(int)scrCon.Screen].screenObj);

        // スクリーンの親オブジェクトを設定
        Ins.transform.SetParent(parentObj.transform, false);

        // 親オブジェクトの中で先頭にする
        Ins.transform.SetAsFirstSibling();
    }
}
