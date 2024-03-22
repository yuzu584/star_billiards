using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 制限時間を管理
public class TimeLimit : MonoBehaviour
{
    [SerializeField] private StageData stageData;           // InspectorでStageDataを指定

    private StageController stageCon;
    private UIController uICon;
    private GameOver gameOver;
    private Initialize init;

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
        uICon.timeLimitUI.value.text = time.ToString("0.0");
        uICon.timeLimitUI.gauge.rectTransform.sizeDelta = new Vector2(imageSize.x * (time / stageData.stageList[stageCon.stageNum].timeLimit), imageSize.y);
        time -= Time.deltaTime;
    }

    void Update()
    {
        // 時間切れなら時間を初期化してゲームオーバー処理
        if (time < 0)
        {
            SetTimeLimit();
            gameOver.GameOverProcess();
        }
    }

    void Start()
    {
        stageCon = StageController.instance;
        uICon = UIController.instance;
        gameOver = GameOver.instance;
        init = Initialize.instance;

        // デリゲートに描画関数を登録
        uICon.timeLimitUI.renderTimeLimit += Render;

        // デリゲートに初期化関数を登録
        init.init_Stage += SetTimeLimit;

        // 制限時間を設定
        SetTimeLimit();

        // ゲージの画像のサイズの初期値を設定
        imageSize = uICon.timeLimitUI.gauge.rectTransform.sizeDelta;
    }
}
