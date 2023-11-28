using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ステージ選択画面のUIを管理
public class StageSelectUIController : MonoBehaviour
{
    [SerializeField] private StageData stageData;             // InspectorでStageDataを指定
    [SerializeField] private StageController stageController; // InspectorでStageControllerを指定
    [SerializeField] private GameObject[] stageIcon;          // ステージのアイコン

    // ステージ情報を描画
    public void DrawStageInfo(Text name, Text mission)
    {
        // ステージ名を設定
        name.text = stageData.stageList[stageController.stageNum].stageName;

        // ミッション名を設定
        switch (stageData.stageList[stageController.stageNum].missionNum)
        {
            case 0: // 全ての惑星を破壊
                mission.text = "Destroy all planets";
                break;
            case 1: // 時間内にゴールにたどり着け
                mission.text = "Reach the goal";
                break;
        }
    }

    // ステージのアイコンを表示/非表示
    public void DrawStageIcon(bool draw)
    {
        // ステージのアイコンを表示/非表示切り替え
        for (int i = 0; i < stageIcon.Length;  i++)
        {
            stageIcon[i].SetActive(draw);
        }
    }
}
