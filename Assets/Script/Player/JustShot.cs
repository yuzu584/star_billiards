using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ジャストショットを管理
public class JustShot : Singleton<JustShot>
{
    [SerializeField] private float justShotGrace = 0.1f; // ジャストショットの猶予時間

    [System.NonSerialized] public float time = 0.0f;     // ジャストショットの猶予時間をカウント

    private UIController uiCon;

    // ジャストショットの猶予時間をカウント
    public IEnumerator JustShotCount()
    {
        // 猶予時間をカウント
        while (time < justShotGrace)
        {
            time += Time.deltaTime;

            yield return null;
        }
        time = 0.0f;
    }

    // ジャストショットUIのアニメーション
    public IEnumerator UIAnimation()
    {
        // ジャストショットのテキストを表示
        uiCon.otherUI.justShotText.enabled = true;

        // 少し待つ
        yield return new WaitForSeconds(1.5f);

        // ジャストショットのテキストを非表示
        uiCon.otherUI.justShotText.enabled = false;
    }

    void Start()
    {
        uiCon = UIController.instance;

        // ジャストショットのテキストを非表示
        uiCon.otherUI.justShotText.enabled = false;
    }
}
