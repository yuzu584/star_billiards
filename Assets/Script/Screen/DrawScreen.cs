using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スクリーンを描画する
public class DrawScreen : MonoBehaviour
{
    [SerializeField] private ScreenData scrData;
    [SerializeField] private GameObject parentObj;

    private ScreenController scrCon;
    private KeyGuideUI keyGuideUI;

    private GameObject scrIns;              // スクリーンのインスタンス
    private GameObject backIns;             // 背景のインスタンス

    private void Start()
    {
        scrCon = ScreenController.instance;
        keyGuideUI = KeyGuideUI.instance;

        // 画面遷移時にスクリーンを描画
        scrCon.changeScreen += Draw;

        Draw();
    }

    // スクリーンを描画
    void Draw()
    {
        // 前回のスクリーンのインスタンスを削除
        DestroyInstance(ref scrIns);

        // インスタンスを生成
        scrIns = Instantiate(scrData.screenList[(int)scrCon.Screen].screenObj);

        // 親オブジェクトを設定
        scrIns.transform.SetParent(parentObj.transform, false);

        // 親オブジェクトの中で先頭にする
        scrIns.transform.SetAsFirstSibling();

        // 背景を描画する画面なら描画
        if (scrData.screenList[(int)scrCon.Screen].drawBackGround)
        {
            DrawBackGround();
        }
        // 描画しないならインスタンスを削除
        else
        {
            DestroyInstance(ref backIns);
        }
    }

    // 背景を描画
    void DrawBackGround()
    {
        // 前回の背景のインスタンスを削除
        DestroyInstance(ref backIns);

        // インスタンスを生成
        backIns = Instantiate(scrData.background);

        // 親オブジェクトを設定
        backIns.transform.SetParent(parentObj.transform, false);

        // 親オブジェクトの中で先頭にする
        backIns.transform.SetAsFirstSibling();
    }

    // インスタンスを削除して null を代入する(GameObject は参照を渡す)
    void DestroyInstance(ref GameObject obj)
    {
        if (obj)
        {
            Destroy(obj);
            obj = null;
        }
    }
}
