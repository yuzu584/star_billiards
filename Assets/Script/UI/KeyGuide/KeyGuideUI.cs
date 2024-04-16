using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// キー操作のガイドのUIを管理
public class KeyGuideUI : Singleton<KeyGuideUI>
{
    [SerializeField] private GameObject guideObj;       // キー操作ガイドのプレハブ
    [SerializeField] private GameObject parentObj;      // キー操作ガイドのプレハブの親オブジェクト

    public List<GameObject> keyGuides = new List<GameObject>();

    private void Start()
    {
        EnumKeyGuide[] g =
        {
            EnumKeyGuide.None,
            EnumKeyGuide.Positive,
            EnumKeyGuide.Negative,
            EnumKeyGuide.MoveCursol,
        };

        Draw(g);
    }

    // キー操作のガイドのUIを描画
    void Draw(EnumKeyGuide[] types)
    {
        // List の中身を空にする
        for(int i = 0; i < keyGuides.Count; ++i)
        {
            Destroy(keyGuides[i]);
        }
        keyGuides.Clear();

        // 引数の数ガイドを生成
        for (int i = 0; i < types.Length; i++)
        {
            GameObject obj = Instantiate(guideObj);                 // インスタンス生成
            obj.transform.SetParent(parentObj.transform, false);    // 親オブジェクトを設定
            var component = obj.GetComponent<KeyGuide>();           // コンポーネント取得
            component.EnumKeyGuide = types[i];                      // ガイドの種類を設定
            keyGuides.Add(obj);                                     // リストに追加
        }
    }
}
