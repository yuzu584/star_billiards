using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 制限時間を管理
public class TimeLimit : MonoBehaviour
{
    [SerializeField] private StageData stageData;             // InspectorでStageDataを指定
    [SerializeField] private StageController stageController; // InspectorでStageControllerを指定
    [SerializeField] private UIController uIController;       // InspectorでStageControllerを指定
    [SerializeField] private GameOver gameOver;               // InspectorでGameOverを指定
    [SerializeField] private Initialize initialize;           // InspectorでInitializeを指定

    private float time = 0;         // 制限時間
    private Vector2 imageSize;      // ゲージの画像のサイズの初期値

    // 制限時間を設定
    void SetTimeLimit()
    {
        if (time != stageData.stageList[stageController.stageNum].timeLimit)
            time = stageData.stageList[stageController.stageNum].timeLimit;
    }

    // 制限時間のUIを描画
    void Render()
    {
        uIController.timeLimitUI.value.text = time.ToString("0.0");
        uIController.timeLimitUI.gauge.rectTransform.sizeDelta = new Vector2(imageSize.x * (time / stageData.stageList[stageController.stageNum].timeLimit), imageSize.y);
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
        // デリゲートに描画関数を登録
        uIController.timeLimitUI.renderTimeLimit += Render;

        // デリゲートに初期化関数を登録
        initialize.init_Stage += SetTimeLimit;

        // 制限時間を設定
        SetTimeLimit();

        // ゲージの画像のサイズの初期値を設定
        imageSize = uIController.timeLimitUI.gauge.rectTransform.sizeDelta;
    }
}
