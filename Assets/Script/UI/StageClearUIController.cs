using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ステージクリア画面のUIを管理
public class StageClearUIController : Singleton<StageClearUIController>
{
    [SerializeField] private Material stageClearButtonMat;              // ボタンのマテリアル
    public float fadeTime = 0.4f;                                       // フェード時間

    private UIController uICon;
    private ScreenController scrCon;
    private Lerp lerp;

    private int buttonNum;                                              // ボタンの数


    // ステージクリア画面のUIを描画
    void DrawStageClearUI()
    {
        // ボタンを非表示
        for (int i = 0; i < buttonNum; i++)
            uICon.stageClearUI.button[i].SetActive(false);

        // ステージクリア画面を動かす
        StartCoroutine(MoveStageClearUI());
    }

    // ステージクリア画面を動かす
    IEnumerator MoveStageClearUI()
    {
        Vector3[] defaultPos = new Vector3[buttonNum]; // 初期位置
        Vector3 startPos;   // 開始位置
        Vector3 endPos;     // 終了位置
        Color32 startColor; // 開始時の色
        Color32 endColor;   // 終了時の色

        // テキストを動かす
        startPos = new Vector3(300.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 0.0f, 0.0f);
        StartCoroutine(lerp.Position_Text(uICon.stageClearUI.stageClearText, startPos, endPos, fadeTime));

        // 一瞬待つ
        yield return new WaitForSecondsRealtime(2.0f);

        // テキストを動かす
        startPos = new Vector3(0.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 100.0f, 0.0f);
        StartCoroutine(lerp.Position_Text(uICon.stageClearUI.stageClearText, startPos, endPos, fadeTime));

        for (int i = 0; i < buttonNum; ++i)
        {
            // ボタンを表示
            uICon.stageClearUI.button[i].SetActive(true);

            // 初期位置を代入
            defaultPos[i] = uICon.stageClearUI.button[i].transform.localPosition;

            // ボタン移動
            startPos = defaultPos[i] + new Vector3(300.0f, 0.0f, 0.0f);
            endPos = defaultPos[i];
            StartCoroutine(lerp.Position_GameObject(uICon.stageClearUI.button[i], startPos, endPos, fadeTime));

            // 透明度変化
            startColor = new Color32(255, 255, 255, 0);
            endColor = new Color32(255, 255, 255, 255);
            StartCoroutine(lerp.Color_Material(stageClearButtonMat, startColor, endColor, fadeTime));
        }
    }

    private void Start()
    {
        lerp = gameObject.AddComponent<Lerp>();
        uICon = UIController.instance;
        scrCon = ScreenController.instance;

        // ステージクリア画面に遷移したならステージクリア画面の描画処理を行う
        scrCon.changeScreen += () =>
        {
            if (scrCon.ScreenNum == 8)
            {
                DrawStageClearUI();
            }
        };

        buttonNum = uICon.stageClearUI.button.Length;
    }
}
