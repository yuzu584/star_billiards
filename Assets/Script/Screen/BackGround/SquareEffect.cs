using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 浮遊する四角形のエフェクトを管理
public class SquareEffect : MonoBehaviour
{
    private Image image;

    private BackGroundEffect bgEffect;

    private float rotationSpeed;

    private void Start()
    {
        bgEffect = BackGroundEffect.instance;

        // 回転速度を設定
        rotationSpeed = Random.Range(1.0f, 50.0f);

        // 自分の Image コンポーネントを取得
        image = GetComponent<Image>();

        // 大きさを乱数で設定
        float randScale = Random.Range(0.05f, 1.5f);
        Vector2 scale = new Vector3(randScale, randScale);
        image.rectTransform.localScale = scale;

        // 座標を乱数で設定
        // 現在の Canvas のサイズから計算
        float scrX = Random.Range(bgEffect.canvasWidth / -2, bgEffect.canvasWidth / 2);
        float scrY = Random.Range(bgEffect.canvasHeight / -2, bgEffect.canvasHeight / 2);
        Vector3 pos = new Vector3(scrX, scrY, 0.0f);
        image.rectTransform.localPosition = pos;

        // 色を設定
        image.color = bgEffect.effectColor;
    }

    private void Update()
    {
        // 回転
        image.rectTransform.Rotate(0.0f, 0.0f, rotationSpeed * Time.unscaledDeltaTime);
    }
}
