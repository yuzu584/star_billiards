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

    public List<GameObject> keyGuideObjs = new List<GameObject>();
    public List<KeyGuide> keyGuideComponents = new List<KeyGuide>();

    public bool smoothSwitching = true;                 // キー操作ガイドUIのスムーズな切り替えを行うか

    private bool isFirstDraw = true;

    private Lerp lerp;

    private void Start()
    {
        lerp ??= gameObject.AddComponent<Lerp>();
    }

    // キー操作のガイドのUIを描画
    public void DrawGuide(KeyGuide.KeyGuideIconAndTextType[] types)
    {
        // ガイドが何も表示されていなければ今回の描画は最初の描画
        if (keyGuideObjs.Count == 0)
        {
            isFirstDraw = true;
        }

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
            keyGuideComponents.Add(obj.GetComponent<KeyGuide>());   // コンポーネント取得

            // ガイドのテキストとアイコンの種類を設定(アイコンの数によって分岐)
            if (types[i].icon.Length == 1)
                keyGuideComponents[i].IconAndText = types[i];
            else if (types[i].icon.Length > 1)
            {
                for (int j = 0; j < types[i].icon.Length - 1; j++)
                {
                    keyGuideComponents[i].DuplicateImage(types[i]);
                }
            }

            keyGuideObjs.Add(obj);                                     // リストに追加
        }

        // HorizontalLayoutGroup を更新させる
        StartCoroutine(UpdateLayoutGroup());
    }

    // キー操作ガイドUIを削除
    public void DestroyGuide()
    {
        // 全ての線形補間を止める
        lerp ??= gameObject.AddComponent<Lerp>();
        lerp.StopAll();

        // List の中身を空にする
        for (int i = 0; i < keyGuideObjs.Count; ++i)
        {
            Destroy(keyGuideObjs[i]);
        }
        keyGuideObjs.Clear();
        keyGuideComponents.Clear();
    }

    // 現在表示しているガイドと同じガイドを描画しようとしているかチェック
    bool RedrawCheck(KeyGuide.KeyGuideIconAndTextType[] types)
    {
        int redrawCount = 0;

        // 比較する配列の長さが違うなら true を返す
        if (keyGuideObjs.Count != types.Length) return true;

        for (int i = 0; i < keyGuideObjs.Count; ++i)
        {
            // コンポーネント取得
            KeyGuide component = keyGuideObjs[i].GetComponent<KeyGuide>();

            // 同じガイドならカウント
            if ((component.IconAndText.CheckIconEquals(types[i].icon)) && (component.IconAndText.text == types[i].text))
                ++redrawCount;
        }

        // 全てのガイドが描画済みなら false
        if (redrawCount >= keyGuideObjs.Count)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // HorizontalLayoutGroup を更新させる
    public IEnumerator UpdateLayoutGroup()
    {
        horizontalLayoutGroup.enabled = false;

        // スムーズな切り替えを行うなら
        if (smoothSwitching)
        {
            for (int i = 0; i < keyGuideComponents.Count; ++i)
                keyGuideComponents[i].GuideEnabled(false);
        }

        // 一瞬待つ
        //yield return new WaitForSecondsRealtime(0.01f);
        yield return null;

        // スムーズな切り替えを行うなら
        if (smoothSwitching)
        {
            for (int i = 0; i < keyGuideComponents.Count; ++i)
                keyGuideComponents[i].GuideEnabled(true);

            lerp ??= gameObject.AddComponent<Lerp>();

            Color32 alpha255 = new Color32(255, 255, 255, 255);
            Color32 alpha0 = new Color32(255, 255, 255, 0);
            float fadeTime = 0.5f;

            lerp.StopAll();

            for (int i = 0; i < keyGuideComponents.Count; ++i)
            {
                StartCoroutine(lerp.Color_Text(keyGuideComponents[i].text, alpha0, alpha255, fadeTime));
                for (int j = 0; j < keyGuideComponents[i].image.Length; ++j)
                    StartCoroutine(lerp.Color_Image(keyGuideComponents[i].image[j], alpha0, alpha255, fadeTime));
            }
        }

        horizontalLayoutGroup.enabled = true;
    }
}
