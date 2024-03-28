using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 制限時間を管理
public class TimeLimit : MonoBehaviour
{
    [SerializeField] private StageData stageData;           // InspectorでStageDataを指定
    [SerializeField] private Text value;                    // 制限時間の数値のテキスト
    [SerializeField] private Image gauge;                   // 制限時間のゲージ

    private StageController stageCon;

    private float time = 0;                                 // 制限時間
    private Vector2 imageSize;                              // ゲージの画像のサイズの初期値

    // 制限時間を設定
    void SetTimeLimit()
    {
        if (time != stageData.stageList[stageCon.stageNum].timeLimit)
            time = stageData.stageList[stageCon.stageNum].timeLimit;
    }

    // 制限時間のUIを描画
    void Render()
    {
        value.text = time.ToString("0.0");
        gauge.rectTransform.sizeDelta = new Vector2(imageSize.x * (time / stageData.stageList[stageCon.stageNum].timeLimit), imageSize.y);
        time -= Time.deltaTime;
    }

    void Update()
    {
        // 時間切れなら時間を初期化してゲームオーバー処理
        if (time < 0)
        {
            SetTimeLimit();
            stageCon.gameOverDele?.Invoke();
        }

        Render();
    }

    void Start()
    {
        stageCon = StageController.instance;

        // 制限時間を設定
        SetTimeLimit();

        // ゲージの画像のサイズの初期値を設定
        imageSize = gauge.rectTransform.sizeDelta;
    }
}
