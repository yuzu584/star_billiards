using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ステージ選択画面のUIを管理
public class StageSelectUIController : Lerp
{
    [SerializeField] private StageData stageData;             // InspectorでStageDataを指定
    [SerializeField] private StageController stageController; // InspectorでStageControllerを指定
    [SerializeField] private UIController uIController;       // InspectorでUIControllerを指定

    [SerializeField] private float fadeTime; // フェード時間
    private bool buttonMoved = false;        // ボタンが移動済みか

    // ステージ情報UIを描画
    public void DrawStageInfo()
    {
        // ステージ情報UIを表示
        uIController.stageSelectUI.stageInfoUI.SetActive(true);

        // ステージ名を設定
        uIController.stageSelectUI.name.text = stageData.stageList[stageController.stageNum].stageName;

        // ミッション名を設定
        switch (stageData.stageList[stageController.stageNum].missionNum)
        {
            case 0: // 全ての惑星を破壊
                uIController.stageSelectUI.mission.text = "Destroy all planets";
                break;
            case 1: // 時間内にゴールにたどり着け
                uIController.stageSelectUI.mission.text = "Reach the goal";
                break;
        }

        // 移動済みでなければボタンを移動
        if (!buttonMoved)
        {
            buttonMoved = true;
            StartCoroutine(Position_GameObject(uIController.stageSelectUI.buttons, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(200.0f, 0.0f, 0.0f), fadeTime));
        }
    }

    // ステージ情報UIを非表示
    public void HideStageInfo()
    {
        // 移動済みならボタンを戻す
        if (buttonMoved)
        {
            buttonMoved = false;
            StartCoroutine(Position_GameObject(uIController.stageSelectUI.buttons, new Vector3(200.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), fadeTime));
        }

        // ステージ情報UIを非表示
        uIController.stageSelectUI.stageInfoUI.SetActive(false);
    }

    void OnEnable()
    {
        // ステージ情報UIを非表示
        HideStageInfo();
    }
}
