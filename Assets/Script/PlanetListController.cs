using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 惑星リストを管理
public class PlanetListController : MonoBehaviour
{
    [SerializeField] StageData stageData;             // InspectorでStageDataを指定
    [SerializeField] StageController stageController; // InspectorでStageControllerを指定
    [SerializeField] GameObject starParentObj;        // ステージの星をまとめる親オブジェクト

    public List<GameObject> planetList; // 惑星のリスト

    // 惑星リストを作成
    public void CreateList()
    {
        // リストの要素をすべて削除
        planetList.Clear();

        // 子オブジェクトの数繰り返す
        for (int i = 0; i < starParentObj.transform.childCount; i++)
        {
            // 子オブジェクトを取得
            Transform childTransform = starParentObj.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;

            // 子オブジェクトが惑星ならリストに追加
            if (childObject.tag == "Planet")
                planetList.Add(childObject);
        }
    }
}
