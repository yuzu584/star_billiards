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
        // リストの要素をすべて削除
        btnList.Clear();

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
            btnList[i].transform.position += new Vector3(0.0f, i * -40.0f, 0.0f);

            // プレハブのテキストを取得
            Text btnText = btnList[i].transform.GetChild(2).GetComponent<Text>();

            // プレハブのテキストを設定
            btnText.text = planetListController.planetList[i].name;

            // ボタン識別用番号を設定
            ButtonController btnNum = btnList[i].GetComponent<ButtonController>();
            btnNum.number = i;
        }
    }
}
