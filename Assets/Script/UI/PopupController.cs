using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ポップアップを管理
public class PopupController : Singleton<PopupController>
{
    [SerializeField] private GameObject popUp;                  // ポップアップのプレハブ

    private UIController uICon;
    private ScreenController scrCon;
    private Initialize init;

    private Lerp lerp;

    [System.NonSerialized] public GameObject[] drawingPopup = new GameObject[10]; // ポップアップの配列

    void Start()
    {
        uICon = UIController.instance;
        scrCon = ScreenController.instance;
        init = Initialize.instance;

        lerp = gameObject.AddComponent<Lerp>();

        // デリゲートに初期化関数を登録
        init.init_Stage += Init;

        // 最初の初期化
        Init();
    }

    void Update()
    {
        // ポップアップの個数繰り返す
        for (int i = 0; i < drawingPopup.Length; i++)
        {
            // インスタンスが生成されていれば
            if (drawingPopup[i] != null)
            {
                // ゲーム画面かつ非表示なら
                if ((scrCon.ScreenNum == 5) && (!drawingPopup[i].activeSelf))

                    // 表示する
                    drawingPopup[i].SetActive(true);

                // ゲーム画面以外かつ表示されているなら
                else if ((scrCon.ScreenNum != 5) && (drawingPopup[i].activeSelf))

                    // 非表示にする
                    drawingPopup[i].SetActive(false);
            }
        }
    }

    // ポップアップを描画
    private IEnumerator DrawPopup(string text)
    {
        float destroyTime = 10.0f;   // 惑星を破壊するまでの時間
        int i = 0;                   // 数を数える変数
        float fadeTime = 1.0f;       // フェード時間
        float moveDistance = 300.0f; // 移動距離
        Vector3 defaultPosition;     // デフォルトの位置

        // 配列の空きが見つかるまで繰り返す
        while ((drawingPopup[i]))
        {
            // 配列の範囲外ならコルーチン終了
            if (i > drawingPopup.Length)
                yield break;
            i++;
        }

        // ポップアップのインスタンスを生成
        drawingPopup[i] = Instantiate(popUp);

        // ポップアップの名前を設定
        drawingPopup[i].name = text;

        // 親を設定
        drawingPopup[i].transform.SetParent(uICon.messageUI.Message.transform, false);

        // 位置を設定
        drawingPopup[i].transform.localPosition += new Vector3(-moveDistance, i * -20.0f, 0.0f);

        // プレハブのテキストを取得
        Text popupText = drawingPopup[i].transform.GetChild(1).GetComponent<Text>();

        // プレハブのテキストを設定
        popupText.text = text;

        // デフォルト位置を設定
        defaultPosition = drawingPopup[i].transform.localPosition;

        // ポップアップを動かす
        yield return StartCoroutine(lerp.Position_GameObject(drawingPopup[i], defaultPosition, defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), fadeTime));

        // ポップアップが時間が経過するまで待つ
        yield return new WaitForSeconds(destroyTime);

        // ポップアップを動かす
        if (drawingPopup[i] != null) { }
            yield return StartCoroutine(lerp.Position_GameObject(drawingPopup[i], defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), defaultPosition, fadeTime));

        // ポップアップを削除
        Destroy(drawingPopup[i]);
    }

    // 惑星が破壊された際のポップアップを呼び出す
    public void DrawDestroyPlanetPopUp(string name)
    {
        StartCoroutine(DrawPopup(name + " was destroyed"));
    }

    // ポップアップを初期化
    public void Init()
    {
        for(int i = 0; i < drawingPopup.Length; i++)
        {
            // インスタンスが存在したら削除
            if (drawingPopup[i])
            {
                Destroy(drawingPopup[i]);
            }
        }

        // ポップアップが描画されているかを管理する変数を初期化
        for (int i = 0; i < drawingPopup.Length; i++)
            drawingPopup[i] = null;
    }
}
