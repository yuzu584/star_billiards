using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ポップアップの親クラス
public class PopupParent : MonoBehaviour
{
    protected Lerp lerp;

    protected virtual void Start()
    {
        // Lerp をアタッチ
        lerp ??= gameObject.AddComponent<Lerp>();
    }

    // ポップアップの処理
    public virtual IEnumerator Process(string text, Transform parentT, int num)
    {
        Debug.Log("ポップアップの処理が設定されていません");
        yield return null;
    }
}
