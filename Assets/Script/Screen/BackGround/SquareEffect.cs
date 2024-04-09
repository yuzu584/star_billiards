using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 浮遊する四角形のエフェクトを管理
public class SquareEffect : MonoBehaviour
{
    private Image image;

    private BackGroundEffect bgEffect;

    private float rotationSpeed;            // 回転速度
    private float scaleDecleaseSpeed;       // 縮小速度
    private bool scaling = true;            // 拡大か縮小か( true : 拡大 false : 縮小)
    private Vector2 defaultScale;

    public bool fastDraw = false;           // 最初の拡大をスキップして素早く描画するか

    private void Start()
    {
        bgEffect = BackGroundEffect.instance;

        // 回転速度を設定
        rotationSpeed = Random.Range(1.0f, 100.0f);

        // 自分の Image コンポーネントを取得
        image = GetComponent<Image>();

        // スケールを乱数で設定
        float randScale = Random.Range(0.05f, 0.6f);
        defaultScale = new Vector2(randScale, randScale);

        // 縮小速度を設定
        scaleDecleaseSpeed = (defaultScale.x / Random.Range(1.0f, 5.0f));

        // スケールを 0 にする
        image.rectTransform.localScale *= 0;

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
        // 素早く描画するなら拡大をスキップ
        if (fastDraw && scaling)
        {
            image.rectTransform.localScale = defaultScale;
            scaling = false;
        }

        if (scaling)
        {
            // スケールが 1 以上なら縮小開始
            if (image.rectTransform.localScale.x >= 1)
            {
                scaling = false;
            }

            // だんだん大きくする
            Vector3 scaleDSpeed = new Vector3(scaleDecleaseSpeed, scaleDecleaseSpeed, scaleDecleaseSpeed);
            image.rectTransform.localScale += (scaleDSpeed * Time.unscaledDeltaTime);
        }
        else
        {
            // スケールが 0 以下なら破壊
            if (image.rectTransform.localScale.x <= 0)
            {
                // 新たにエフェクトを生成
                bgEffect.GenerateSquareEffect(false);
                Destroy(gameObject);
            }

            // だんだん小さくする
            Vector3 scaleDSpeed = new Vector3(scaleDecleaseSpeed, scaleDecleaseSpeed, scaleDecleaseSpeed);
            image.rectTransform.localScale -= (scaleDSpeed * Time.unscaledDeltaTime);
        }

        // 回転
        image.rectTransform.Rotate(0.0f, 0.0f, rotationSpeed * Time.unscaledDeltaTime);
    }
}
