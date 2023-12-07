using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 惑星リストを管理
public class PlanetListController : MonoBehaviour
{
    [SerializeField] private StageData stageData;                           // InspectorでStageDataを指定
    [SerializeField] private StageController stageController;               // InspectorでStageControllerを指定
    [SerializeField] private PlanetListUIController planetListUIController; // InspectorでPlanetListUIControllerを指定
    [SerializeField] private ScreenController screenController;             // InspectorでScreenControllerを指定
    [SerializeField] private GameObject starParentObj;                      // ステージの星をまとめる親オブジェクト

    public List<GameObject> planetList;   // 惑星のリスト
    public bool uiDrawing = false;        // 惑星リストUIが描画されているか

    private bool isPushKey = false; // キーが押されているか

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

    void Update()
    {
        // ゲーム中か惑星リスト画面で惑星リスト表示ボタンが押されたら表示/非表示切り替え
        if (((screenController.screenNum == 0) || (screenController.screenNum == 5)) && (Input.GetAxisRaw("PlanetList") != 0) && (!isPushKey))
        {
            isPushKey = true;
            uiDrawing = !uiDrawing;

            // UIが描画されているかで分岐
            if (uiDrawing)
            {
                // 惑星リスト画面に遷移
                screenController.screenNum = 5;

                // UIを描画
                planetListUIController.DrawPlanetList();
            }
            else
            {
                // ゲーム画面に遷移
                screenController.screenNum = 0;

                // UIを削除
                planetListUIController.DeletePlanetListContent();
            }
        }
        // ゲーム中か惑星リスト画面でキーが押されていなければisPushKeyを押されていない状態にする
        else if (((screenController.screenNum == 0) || (screenController.screenNum == 5)) && (Input.GetAxisRaw("PlanetList") == 0) && (isPushKey))
            isPushKey = false;
    }
}
