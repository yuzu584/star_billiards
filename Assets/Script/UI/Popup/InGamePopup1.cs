using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ゲーム画面のポップアップ1
public class InGamePopup1 : PopupParent
{
    private ScreenController scrCon;
    private PopupManager popupMana;

    protected override void Start()
    {
        scrCon = ScreenController.instance;
        popupMana ??= PopupManager.instance;
    }

    void Update()
    {
        // ポップアップの個数繰り返す
        for (int i = 0; i < popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance.Length; i++)
        {
            // インスタンスが生成されていれば
            if (popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[i] != null)
            {
                // ゲーム画面かつ非表示なら
                if ((scrCon.Screen == ScreenController.ScreenType.InGame) && (!popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[i].activeSelf))

                    // 表示する
                    popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[i].SetActive(true);

                // ゲーム画面以外かつ表示されているなら
                else if ((scrCon.Screen != ScreenController.ScreenType.InGame) && (popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[i].activeSelf))

                    // 非表示にする
                    popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[i].SetActive(false);
            }
        }
    }

    // ポップアップの処理
    public override IEnumerator Process(string text, Transform parentT, int num)
    {
        float destroyTime = 10.0f;   // 惑星を破壊するまでの時間
        float fadeTime = 1.0f;       // フェード時間
        float moveDistance = 300.0f; // 移動距離
        Vector3 defaultPosition;     // デフォルトの位置

        popupMana ??= PopupManager.instance;

        // 親を設定
        popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num].transform.SetParent(parentT, false);

        // 位置を設定
        popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num].transform.localPosition += new Vector3(-moveDistance, num * -20.0f, 0.0f);

        // プレハブのテキストを取得
        Text popupText = popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num].transform.GetChild(1).GetComponent<Text>();

        // プレハブのテキストを設定
        popupText.text = text;

        // デフォルト位置を設定
        defaultPosition = popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num].transform.localPosition;

        lerp ??= gameObject.AddComponent<Lerp>();

        // ポップアップを動かす
        yield return StartCoroutine(lerp.Position_GameObject(popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num], defaultPosition, defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), fadeTime));

        // ポップアップが時間が経過するまで待つ
        yield return new WaitForSeconds(destroyTime);

        // ポップアップを動かす
        if (popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num] != null)
        yield return StartCoroutine(lerp.Position_GameObject(popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num], defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), defaultPosition, fadeTime));

        // ポップアップを削除
        if(gameObject)
            Destroy(gameObject);
    }
}
