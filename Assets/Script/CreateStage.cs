using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージを作成
public class CreateStage : MonoBehaviour
{
    [SerializeField] StageData stageData;             // InspectorでStageDataを指定
    [SerializeField] StageController stageController; // InspectorでStageControllerを指定
    [SerializeField] GameObject sphere;               // スフィア

    private GameObject[] star;   // 恒星のGameObject
    private GameObject[] planet; // 惑星のGameObject

    // ステージを作成
    public void Create()
    {
        // 配列を初期化
        star = new GameObject[stageData.stageList[stageController.stageNum].fixedStar.Length];
        planet = new GameObject[stageData.stageList[stageController.stageNum].planet.Length];

        // 恒星のインスタンスを生成・名前から(clone)を削除・親を設定
        for (int i = 0; i < stageData.stageList[stageController.stageNum].fixedStar.Length; i++)
        {
            star[i] = Instantiate(stageData.stageList[stageController.stageNum].fixedStar[i]);
            star[i].name = stageData.stageList[stageController.stageNum].fixedStar[i].name;
            star[i].transform.SetParent(this.transform, false);
        }

        // 惑星のインスタンスを生成・名前から(clone)を削除・親を設定
        for (int i = 0; i < stageData.stageList[stageController.stageNum].planet.Length; i++)
        {
            planet[i] = Instantiate(stageData.stageList[stageController.stageNum].planet[i]);
            planet[i].name = stageData.stageList[stageController.stageNum].planet[i].name;
            planet[i].transform.SetParent(this.transform, false);
        }
    }

    // ステージ削除
    public void Destroy()
    {
        if(star != null)
        {
            // 恒星のインスタンスを削除
            for (int i = 0; i < stageData.stageList[stageController.stageNum].fixedStar.Length; i++)
                Destroy(star[i]);
        }

        if(planet != null)
        {
            // 惑星のインスタンスを削除
            for (int i = 0; i < stageData.stageList[stageController.stageNum].planet.Length; i++)
                Destroy(planet[i]);
        }
    }

    // ステージを表示/非表示
    public void Draw(bool draw)
    {
        // 恒星を表示/非表示
        for (int i = 0; i < stageData.stageList[stageController.stageNum].fixedStar.Length; i++)
        {
            if (star[i] != null)
                star[i].SetActive(draw);
        }

        // 惑星を表示/非表示
        for (int i = 0; i < stageData.stageList[stageController.stageNum].planet.Length; i++)
        {
            if (planet[i] != null)
                planet[i].SetActive(draw);
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
