using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// 惑星リストUIを管理
public class PlanetListUIController : MonoBehaviour
{
    [SerializeField] private GameObject planetListBtn;                  // ボタンのプレハブ
    [SerializeField] private GameObject parentObj;                      // ボタンのプレハブの親オブジェクト
    [SerializeField] private PlanetListController planetListController; // InspectorでPlanetListControllerを指定

    public List<GameObject> btnList; // ボタンのプレハブのリスト

    // 惑星リストUIを描画
    public void DrawPlanetList()
    {
        // 惑星リストを作成
        planetListController.CreateList();

        // 惑星リストの要素数分繰り返す
        for (int i = 0; i < planetListController.planetList.Count; i++)
        {
            // ボタンのインスタンスを生成
            btnList.Add(Instantiate(planetListBtn));

            // ポップアップの名前を設定
            btnList[i].name = planetListController.planetList[i].name;

            // 親を設定
            btnList[i].transform.SetParent(parentObj.transform, false);

            // 位置を設定
            btnList[i].transform.position += new Vector3(0.0f, i * -75.0f, 0.0f);

            // プレハブのテキストを取得
            Text btnText = btnList[i].transform.GetChild(2).GetComponent<Text>();

            // プレハブのテキストを設定
            btnText.text = planetListController.planetList[i].name;

            // ボタンを押したときの効果を設定
            ButtonController buttonController = btnList[i].transform.GetChild(0).GetComponent<ButtonController>();
            buttonController.clickAction = ButtonController.ClickAction.LockOnPlanet;
        }
    }

    // 惑星リストUIの要素を削除
    public void DeletePlanetListContent()
    {
        // インスタンスを削除
        for (int i = 0; i < btnList.Count; i++)
        {
            Destroy(btnList[i].gameObject);
        }

        // リストを初期化
        btnList.Clear();
    }
}
