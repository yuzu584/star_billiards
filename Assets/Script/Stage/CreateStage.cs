using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージを作成
public class CreateStage : MonoBehaviour
{
    [SerializeField] StageData stageData;             // InspectorでStageDataを指定
    [SerializeField] StageController stageController; // InspectorでStageControllerを指定
    [SerializeField] GameObject sphere;               // スフィア

    private GameObject stagePrefab; // ステージのプレハブ

    // ステージを作成
    public void Create()
    {
        // ステージのインスタンスを生成・名前から(clone)を削除・親を設定
        stagePrefab = Instantiate(stageData.stageList[stageController.stageNum].stagePrefab);
        stagePrefab.name = stageData.stageList[stageController.stageNum].stagePrefab.name;
        stagePrefab.transform.SetParent(this.transform, false);
    }

    // ステージ削除
    public void Destroy()
    {
        if(stagePrefab != null)
        {
            Destroy(stagePrefab);
        }
    }

    // ステージを表示/非表示
    public void Draw(bool draw)
    {
        // ステージを表示/非表示
        if(stagePrefab != null)
        {
            stagePrefab.SetActive(draw);
        }

        // スフィアを表示/非表示切り替え
        sphere.SetActive(draw);
    }

    void Start()
    {
        // スフィアを非表示
        sphere.SetActive(false);
    }
}
