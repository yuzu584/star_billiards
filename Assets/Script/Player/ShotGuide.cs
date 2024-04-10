using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ショット時のガイド
public class ShotGuide : MonoBehaviour
{
    [SerializeField] private GameObject guideObj;           // ガイドに使用するプレイヤーの見た目のオブジェクト
    [SerializeField] private GameObject parentObj;          // 親オブジェクト

    private PredictionLine pl;
    private EnergyController eneCon;
    private InputController input;

    public GameObject instance;                             // プレハブのインスタンス

    private void Start()
    {
        pl = PredictionLine.instance;
        eneCon = EnergyController.instance;
        input = InputController.instance;

        input.game_OnShotDele += GuideProcess;
    }

    // ガイドの処理
    public void GuideProcess(float value)
    {
        // インスタンス未生成なら生成
        if(instance == null)
        {
            // インスタンス生成
            instance = Instantiate(guideObj);

            // 生成したインスタンスを非表示
            instance.SetActive(false);

            // 親オブジェクトを設定
            instance.transform.SetParent(parentObj.transform, false);
        }

        // エネルギーがある状態でショットボタンが押されていたら
        if ((value > 0) && (eneCon.energy.Value > 0))
        {
            // 非表示なら表示
            if (!instance.activeSelf)
                instance.SetActive(true);

            // ガイドの座標を移動
            Vector3 pos = pl.hit1.point;
            pos += (pl.hit1.normal * (instance.transform.localScale.x / 2));
            instance.transform.localPosition = pos;
        }
        // ガイドが表示されていたら非表示
        else if(instance.activeSelf)
        {
            instance.SetActive(false);
        }
    }
}
