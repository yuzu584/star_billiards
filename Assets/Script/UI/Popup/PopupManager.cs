using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

// ゲーム内のポップアップ全般を管理
[DefaultExecutionOrder(-100)]
public class PopupManager : Singleton<PopupManager>
{
    // ポップアップの要素
    [System.Serializable]
    public struct PopupContent
    {
        public GameObject obj;              // ポップアップのプレハブ
        public Transform parentTransform;   // ポップアップのプレハブの親オブジェクトの Transform
        public int maxDraw;                 // ポップアップの最大描画数
        public GameObject[] instance;       // ポップアップのインスタンス
        public PopupParent[] component;     // 取得したポップアップのコンポーネント
        public Coroutine[] coroutines;      // ポップアップのコルーチン
    }

    public PopupContent[] popupContent;

    private int count = 0;

    public enum PopupType
    {
        InGamePopup1,
        InMenuPopup1,
        DialogPopup1,
    }

    private void Start()
    {
        // 構造体の要素の長さを設定
        for(int i = 0; i < popupContent.Length; i++)
        {
            popupContent[i].instance = new GameObject[popupContent[i].maxDraw];
            popupContent[i].component = new PopupParent[popupContent[i].maxDraw];
            popupContent[i].coroutines = new Coroutine[popupContent[i].maxDraw];
        }
    }

    // 指定したポップアップを描画
    public GameObject DrawPopup(PopupType pType, string text)
    {
        // 空いている配列の場所を代入
        int count = CheckInstance(popupContent[(int)pType]);

        // 配列が開いていなければ終了
        if (count == -1) return null;

        // インスタンス生成
        popupContent[(int)pType].instance[count] = Instantiate(popupContent[(int)pType].obj);

        // コンポーネント取得
        popupContent[(int)pType].component[count] = popupContent[(int)pType].instance[count].GetComponent<PopupParent>();

        // ポップアップの処理を行う
        popupContent[(int)pType].coroutines[count] = StartCoroutine(popupContent[(int)pType].component[count].Process(text, popupContent[(int)pType].parentTransform, count));

        // 生成したインスタンスが返り値
        return popupContent[(int)pType].instance[count];
    }

    // インスタンスの配列の開いている場所を探して返す
    public int CheckInstance(PopupContent pCon)
    {
        count = 0;

        for (int i = 0; i < pCon.instance.Length; i++)
        {
            if (pCon.instance[i] == null) return count;

            ++count;
        }

        return -1;
    }

    // 配列を初期化
    public void Init(PopupContent pCon)
    {
        for(int i = 0; i < pCon.instance.Length; ++i)
        {
            // PopupParent のコンポーネントを取得済みならポップアップ削除処理を行う
            if (pCon.component[i] != null)
                pCon.component[i].Destroy();
        }
    }

    // 特定のコルーチンを停止させる(StopCoroutine を外部から呼び出すとエラーが出るためこの関数を使用してコルーチンを停止させる)
    public void StopCoroutineOfPopupContent(PopupContent pCon, int index)
    {
        StopCoroutine(pCon.coroutines[index]);
    }
}
