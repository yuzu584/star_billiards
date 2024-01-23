using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ポップアップを管理
public class PopupController : MonoBehaviour
{
    [SerializeField] private GameObject popUp;                  // ポップアップのプレハブ
    [SerializeField] private UIController uIController;         // InspectorでUIControllerを指定
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定

    [System.NonSerialized] public GameObject[] drawingPopup = new GameObject[10]; // ポップアップの配列

    // ポップアップを動かす
    public IEnumerator MovePopup(float time, float fadeTime, GameObject popup, float moveDistance, Vector3 defaultPosition, int i)
    {
        // 指定した時間が経過するまで繰り返す
        while (time < fadeTime)
        {
            // ポップアップが存在しなければ終了
            if (!popup)
            {
                yield break;
            }

            // 時間をカウント
            time += Time.deltaTime;

            // 進み具合を計算
            float t = time / fadeTime;

            // ポップアップを移動
            popup.transform.position = Vector3.Lerp(defaultPosition, defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), t);

            // 1フレーム待つ
            yield return null;
        }

        if (moveDistance < 0)
            // ポップアップを削除
            Destroy(drawingPopup[i].gameObject);
    }

    // ポップアップを描画
    public IEnumerator DrawDestroyPlanetPopup(string text)
    {
        float destroyTime = 10.0f;   // 惑星を破壊するまでの時間
        int i = 0;                   // 数を数える変数
        float fadeTime = 0.2f;       // フェード時間
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
        drawingPopup[i].transform.SetParent(uIController.messageUI.Message.transform, false);

        // 位置を設定
        drawingPopup[i].transform.position += new Vector3(-moveDistance, i * -40.0f, 0.0f);

        // プレハブのテキストを取得
        Text popupText = drawingPopup[i].transform.GetChild(1).GetComponent<Text>();

        // プレハブのテキストを設定
        popupText.text = text;

        // 経過時間をカウント
        float time = 0;

        // デフォルト位置を設定
        defaultPosition = drawingPopup[i].transform.position;

        // ポップアップを動かす
        StartCoroutine(MovePopup(time, fadeTime, drawingPopup[i], moveDistance, defaultPosition, i));

        // ポップアップが時間が経過するまで待つ
        yield return new WaitForSeconds(destroyTime);

        // ポップアップを動かす
        StartCoroutine(MovePopup(time, fadeTime, drawingPopup[i], -moveDistance, defaultPosition, i));
    }

    // ポップアップを初期化
    public void InitPopUp()
    {
        for(int i = 0; i < drawingPopup.Length; i++)
        {
            // インスタンスが存在したら削除
            if (drawingPopup[i])
            {
                Destroy(drawingPopup[i]);
            }
        }
    }

    void Start()
    {
        // ポップアップが描画されているかを管理する変数を初期化
        for (int i = 0; i < drawingPopup.Length; i++)
            drawingPopup[i] = null;
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
                if ((screenController.screenNum == 5) && (!drawingPopup[i].activeSelf))

                    // 表示する
                    drawingPopup[i].SetActive(true);

                // ゲーム画面以外かつ表示されているなら
                else if ((screenController.screenNum != 5) && (drawingPopup[i].activeSelf))

                    // 非表示にする
                    drawingPopup[i].SetActive(false);
            }
        }
    }
}
