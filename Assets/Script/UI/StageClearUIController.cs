using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ステージクリア画面のUIを管理
public class StageClearUIController : MonoBehaviour
{
    [SerializeField] private Material stageClearButtonMat;              // ボタンのマテリアル
    public float fadeTime = 0.4f;                                       // フェード時間

    [SerializeField] private StageData stageData;
    private StageScore stageScore;
    private StageController stageCon;
    private TimeLimit timeLimit;
    private Lerp lerp;

    private int score = 0;                                              // 獲得スコア
    private int oldScore = 0;                                           // 獲得スコアを反映していないスコア
    private bool newRecord = false;                                     // ハイスコアを更新したか
    private Color hideColor = new Color(1, 1, 1, 0);                    // 透明

    [SerializeField] private GameObject[] btn;                          // 画面に存在するボタン
    [SerializeField] private Text stageClearText;                       // ステージクリア画面のテキスト
    [SerializeField] private Text[] scoreText;                          // スコアの項目名のテキスト
    [SerializeField] private Text[] scoreValueText;                     // スコアの値のテキスト
    [SerializeField] private Text newRecordText;                        // ハイスコア更新したときのテキスト


    // ステージクリア画面のUIを描画
    void DrawStageClearUI()
    {
        // ボタンを非表示
        for (int i = 0; i < btn.Length; i++)
            btn[i].SetActive(false);

        // ハイスコア更新したときのテキストを非表示
        newRecordText.enabled = false;

        // 獲得スコアを計算
        score = (int)((timeLimit.time / stageData.stageList[stageCon.stageNum].timeLimit) * 100000);

        // スコアを設定
        SetScore(score);

        // スコアのUIを非表示
        ScoreUIAnim(false);

        // ステージクリア画面を動かす
        StartCoroutine(MoveStageClearUI());
    }

    // ステージクリア画面を動かす
    private IEnumerator MoveStageClearUI()
    {
        Vector3[] defaultPos = new Vector3[btn.Length]; // 初期位置
        Vector3 startPos;   // 開始位置
        Vector3 endPos;     // 終了位置
        Color32 startColor; // 開始時の色
        Color32 endColor;   // 終了時の色

        // テキストを動かす
        startPos = new Vector3(300.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 0.0f, 0.0f);
        StartCoroutine(lerp.Position_Text(stageClearText, startPos, endPos, fadeTime));

        // 一瞬待つ
        yield return new WaitForSecondsRealtime(1.0f);

        // テキストを動かす
        startPos = new Vector3(0.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 100.0f, 0.0f);
        StartCoroutine(lerp.Position_Text(stageClearText, startPos, endPos, fadeTime));

        // スコアのUIを表示してアニメーション
        ScoreUIAnim(true);

        // 一瞬待つ
        yield return new WaitForSecondsRealtime(1.0f);

        // ボタンの数繰り返す
        for (int i = 0; i < btn.Length; ++i)
        {
            // ボタンを表示
            btn[i].SetActive(true);

            // 初期位置を代入
            defaultPos[i] = btn[i].transform.localPosition;

            // ボタン移動
            startPos = defaultPos[i] + new Vector3(300.0f, 0.0f, 0.0f);
            endPos = defaultPos[i];
            StartCoroutine(lerp.Position_GameObject(btn[i], startPos, endPos, fadeTime));

            // 透明度変化
            startColor = new Color32(255, 255, 255, 0);
            endColor = new Color32(255, 255, 255, 255);
            StartCoroutine(lerp.Color_Material(stageClearButtonMat, startColor, endColor, fadeTime));
        }
    }

    // スコアのUIのアニメーション
    void ScoreUIAnim(bool orDraw)
    {
        // スコアのUIを表示/非表示
        for (int i = 0; i < scoreText.Length; i++)
            scoreText[i].enabled = orDraw;

        for (int i = 0; i < scoreValueText.Length; i++)
            scoreValueText[i].enabled = orDraw;

        // 描画するなら
        if (orDraw)
        {

            // スコアのテキストを設定
            scoreValueText[0].text = score.ToString();

            // ハイスコアのテキストを設定
            if (oldScore == 0)
                scoreValueText[1].text = "--------";
            else
                scoreValueText[1].text = oldScore.ToString();

            // スコアのUIの色を線形補完で変更
            for (int i = 0; i < scoreText.Length; i++)
                StartCoroutine(lerp.Color_Text(scoreText[i], hideColor, new Color(1, 1, 1, 1), fadeTime));

            for (int i = 0; i < scoreValueText.Length; i++)
                StartCoroutine(lerp.Color_Text(scoreValueText[i], hideColor, new Color(1, 1, 1, 1), fadeTime));
        }

        // ハイスコアを更新したかつ描画するなら
        if ((newRecord) && (orDraw))
        {
            // ハイスコア更新したときのテキストを表示
            newRecordText.enabled = true;
            StartCoroutine(lerp.Color_Text(newRecordText, hideColor, new Color(1, 1, 1, 1), fadeTime));
        }
    }

    // スコアを設定
    void SetScore(int s)
    {
        oldScore = stageScore.score[stageCon.stageNum];

        // 獲得スコアがそのステージのハイスコアを更新していたら
        if (oldScore < s)
        {
            // ハイスコアを上書き
            stageScore.score[stageCon.stageNum] = s;

            newRecord = true;
        }
    }

    private void Start()
    {
        lerp = gameObject.AddComponent<Lerp>();
        stageScore = StageScore.instance;
        stageCon = StageController.instance;
        timeLimit = TimeLimit.instance;

        DrawStageClearUI();
    }
}
