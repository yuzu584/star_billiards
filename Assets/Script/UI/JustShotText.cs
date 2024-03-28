using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ジャストショット時に表示されるテキストを管理
public class JustShotText : MonoBehaviour
{
    [SerializeField] private Text justShotText; // ジャストショットのテキスト

    private JustShot jShot;

    private void Start()
    {
        jShot = JustShot.instance;

        // ジャストショット時にテキストを表示するデリゲートに登録
        jShot.drawJustShotText += Draw;

        // テキストを非表示
        Draw(false);
    }

    private void OnDestroy()
    {
        jShot.drawJustShotText -= Draw;
    }

    // テキストを表示/非表示
    void Draw(bool orDraw)
    {
        justShotText.enabled = orDraw;
    }
}
