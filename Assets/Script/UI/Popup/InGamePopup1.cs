using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ゲーム画面のポップアップ1
public class InGamePopup1 : PopupParent
{
    protected override void Start()
    {
        base.Start();

        scrCon.changeScreen += () =>
        {
            // ゲーム画面なら表示、それ以外なら非表示
            for (int i = 0; i < popupMana.popupContent.Length; i++)
            {
                // インスタンスが生成されていれば
                if (popupMana.popupContent[(int)popupType].instance[i] != null)
                    popupMana.popupContent[(int)popupType].instance[i].SetActive(scrCon.Screen == ScreenController.ScreenType.InGame);
            }
        };
    }

    private void OnDestroy()
    {
        scrCon.changeScreen -= () =>
        {
            // ゲーム画面なら表示、それ以外なら非表示
            for (int i = 0; i < popupMana.popupContent.Length; i++)
            {
                // インスタンスが生成されていれば
                if (popupMana.popupContent[(int)popupType].instance[i] != null)
                    popupMana.popupContent[(int)popupType].instance[i].SetActive(scrCon.Screen == ScreenController.ScreenType.InGame);
            }
        };
    }

    // ポップアップの処理
    public override IEnumerator Process(string text, Transform parentT, int num)
    {
        float destroyTime = 10.0f;   // 惑星を破壊するまでの時間
        float fadeTime = 1.0f;       // フェード時間
        float moveDistance = 300.0f; // 移動距離
        Vector3 defaultPosition;     // デフォルトの位置

        index = num;

        popupMana ??= PopupManager.instance;

        // 親を設定
        popupMana.popupContent[(int)popupType].instance[index].transform.SetParent(parentT, false);

        // 位置を設定
        popupMana.popupContent[(int)popupType].instance[index].transform.localPosition += new Vector3(-moveDistance, index * -20.0f, 0.0f);

        // プレハブのテキストを取得
        Text popupText = popupMana.popupContent[(int)popupType].instance[index].transform.GetChild(1).GetComponent<Text>();

        // プレハブのテキストを設定
        popupText.text = text;

        // デフォルト位置を設定
        defaultPosition = popupMana.popupContent[(int)popupType].instance[index].transform.localPosition;

        lerp ??= gameObject.AddComponent<Lerp>();

        // ポップアップを動かす
        yield return StartCoroutine(lerp.Position_GameObject(popupMana.popupContent[(int)popupType].instance[index], defaultPosition, defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), fadeTime));

        // ポップアップが時間が経過するまで待つ
        yield return new WaitForSeconds(destroyTime);

        // ポップアップを動かす
        if (popupMana.popupContent[(int)popupType].instance[index] != null)
        yield return StartCoroutine(lerp.Position_GameObject(popupMana.popupContent[(int)popupType].instance[index], defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), defaultPosition, fadeTime));

        // ポップアップを削除
        Destroy();
    }
}
