using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージを作成
public class CreateStage : MonoBehaviour
{
    [SerializeField] StageData stageData;             // InspectorでStageDataを指定
    [SerializeField] StageController stageController; // InspectorでStageControllerを指定

    private GameObject[] star;   // 星のGameObject
    private GameObject[] planet; // 惑星のGameObject

    // ステージを作成
    public void Create()
    {
        // 配列を初期化
        star = new GameObject[stageData.stageList[stageController.stageNum].fixedStar.Length];
        planet = new GameObject[stageData.stageList[stageController.stageNum].planet.Length];

        // 恒星のインスタンスを生成・名前から(clone)を削除
        for(int i = 0; i < stageData.stageList[stageController.stageNum].fixedStar.Length; i++)
        {
            star[i] = Instantiate(stageData.stageList[stageController.stageNum].fixedStar[i]);
            star[i].name = stageData.stageList[stageController.stageNum].fixedStar[i].name;
        }

        // 惑星のインスタンスを生成・名前から(clone)を削除
        for (int i = 0; i < stageData.stageList[stageController.stageNum].planet.Length; i++)
        {
            planet[i] = Instantiate(stageData.stageList[stageController.stageNum].planet[i]);
            planet[i].name = stageData.stageList[stageController.stageNum].planet[i].name;
        }
    }

    // ステージを削除
    public void Delete()
    {
        // 恒星のインスタンスを削除
        for (int i = 0; i < stageData.stageList[stageController.stageNum].fixedStar.Length; i++)
        {
            Destroy(star[i]);
            star[i] = null;
        }

        // 惑星のインスタンスを削除
        for (int i = 0; i < stageData.stageList[stageController.stageNum].planet.Length; i++)
        {
            Destroy(planet[i]);
            planet[i] = null;
        }
    }
}
