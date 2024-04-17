using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// キー操作のガイドのUIを管理
public class KeyGuideUI : Singleton<KeyGuideUI>
{
    [SerializeField] private GameObject guideObj;       // キー操作ガイドのプレハブ
    [SerializeField] private GameObject parentObj;      // キー操作ガイドのプレハブの親オブジェクト
    [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;

    public List<GameObject> keyGuides = new List<GameObject>();

    private bool isFirstDraw = true;

    // キー操作のガイドのUIを描画
    public void DrawGuide(EnumKeyGuide[] types)
    {
        // ガイドが何も表示されていなければ今回の描画は最初の描画
        if (keyGuides.Count == 0) isFirstDraw = true;

        // 既にガイドが描画済みなら
        if (!isFirstDraw)
        {
            // 現在表示しているガイドと同じガイドを描画しようとしていたら終了
            if (!RedrawCheck(types)) return;
        }
        // これが最初の描画なら RedrawCheck を行わない
        else if(isFirstDraw)
        {
            isFirstDraw = false;
        }

        // List の中身を空にする
        DestroyGuide();

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

    // キー操作ガイドUIを削除
    public void DestroyGuide()
    {
        // List の中身を空にする
        for (int i = 0; i < keyGuides.Count; ++i)
        {
            Destroy(keyGuides[i]);
        }
        keyGuides.Clear();
    }

    // 現在表示しているガイドと同じガイドを描画しようとしているかチェック
    bool RedrawCheck(EnumKeyGuide[] types)
    {
        int redrawCount = 0;

        for (int i = 0; i < keyGuides.Count; ++i)
        {
            if (i >= types.Length)
            {
                continue;
            }

            // コンポーネント取得
            KeyGuide component = keyGuides[i].GetComponent<KeyGuide>();

            // 同じガイドならカウント
            if (component.EnumKeyGuide == types[i])
                ++redrawCount;
        }

        // 全てのガイドが描画済みなら false
        if (redrawCount >= keyGuides.Count)
        {
            return false;
        }
        else
        {
            Debug.Log("ガイドを更新");
            return true;
        }
    }

    // HorizontalLayoutGroup を更新させる
    public IEnumerator UpdateLayoutGroup()
    {
        horizontalLayoutGroup.enabled = false;

        // 一瞬待つ
        yield return new WaitForSecondsRealtime(0.03f);
        horizontalLayoutGroup.enabled = true;
    }
}
