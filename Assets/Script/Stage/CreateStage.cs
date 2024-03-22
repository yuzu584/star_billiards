using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージを作成
public class CreateStage : Singleton<CreateStage>
{
    [SerializeField] private StageData stageData;   // InspectorでStageDataを指定

    private StageController stageCon;
    private GameObject stagePrefab;                 // ステージのプレハブ

    private void Start()
    {
        stageCon = StageController.instance;
    }

    // ステージを作成
    public void Create()
    {
        // ステージのインスタンスを生成・名前から(clone)を削除・親を設定
        stagePrefab = Instantiate(stageData.stageList[stageCon.stageNum].stagePrefab);
        stagePrefab.name = stageData.stageList[stageCon.stageNum].stagePrefab.name;
        stagePrefab.transform.SetParent(transform, false);
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
    public void Render(bool draw)
    {
        // ステージを表示/非表示
        if(stagePrefab != null)
        {
            stagePrefab.SetActive(draw);
        }
    }

    // 現在ステージが描画されているかを返す
    public bool NowRenderState()
    {
        if(stagePrefab != null)
            return stagePrefab.activeSelf;
        else return false;
    }
}
