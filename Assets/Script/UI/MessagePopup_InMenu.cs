using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// メッセージポップアップ
public class MessagePopup_InMenu : MonoBehaviour
{
    [SerializeField] private Text messageText;              // テキスト
    [SerializeField] private Image image;                   // 画像
    [SerializeField] private float destroyTime = 3.0f;      // ポップアップが消えるまでの時間

    private Lerp lerp;

    private void Awake()
    {
        lerp = gameObject.AddComponent<Lerp>();
    }

    // メッセージポップアップを生成
    public IEnumerator Generate(string text, Transform parentT)
    {
        Color[] startColor = new Color[2];
        Color[] endColor = new Color[2];
        startColor[0] = new(0, 0, 0, 0);
        startColor[1] = new(1, 1, 1, 0);
        endColor[0] = new(0, 0, 0, 0.78f);
        endColor[1] = new(1, 1, 1, 1);
        float fadeTime = 0.2f;;

        // 親オブジェクトを設定
        gameObject.transform.SetParent(parentT, false);

        // テキストを設定
        messageText.text = text;

        // 線形補間でアニメーション
        StartCoroutine(lerp.Color_Image(image, startColor[0], endColor[0], fadeTime));
        StartCoroutine(lerp.Color_Text(messageText, startColor[1], endColor[1], fadeTime));

        // 数秒待つ
        yield return new WaitForSecondsRealtime(destroyTime);

        // 線形補間でアニメーション
        StartCoroutine(lerp.Color_Image(image, endColor[0], startColor[0], fadeTime));
        StartCoroutine(lerp.Color_Text(messageText, endColor[1], startColor[1], fadeTime));

        // 数秒待つ
        yield return new WaitForSecondsRealtime(fadeTime);

        // 自分を破棄
        Destroy(gameObject);
    }
}
