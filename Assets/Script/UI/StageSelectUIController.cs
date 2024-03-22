using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ステージ選択画面のUIを管理
public class StageSelectUIController : Singleton<StageSelectUIController>
{
    [SerializeField] private StageData stageData;               // InspectorでStageDataを指定

    private StageController stageCon;
    private ScreenController scrCon;
    private UIController uICon;

    private GameObject oldButton;                               // 取得したステージボタン
    private Vector3 oldPos;                                     // 取得したステージボタンの座標
    private StageButton oldStageBtn;                            // 取得したStageButton
    private float fadeTime = 0.4f;                              // フェード時間
    private StageButton sBtn;                                   // StageButtonを代入する変数
    private Lerp lerp;

    void Start()
    {
        stageCon = StageController.instance;
        scrCon = ScreenController.instance;
        uICon ??= UIController.instance;
        
        lerp = gameObject.AddComponent<Lerp>();
    }

    void Update()
    {
        if (sBtn != null)
        {
            if ((sBtn.anim) && (scrCon.ScreenLoot == 0))
                HideStageInfo(false);
        }
    }

    // ステージ情報UIを描画
    public void DrawStageInfo(Vector3 pos, GameObject button, StageButton stageButton)
    {
        // ステージボタンの見た目が変えられていたら見た目を元に戻す
        ResetDetail(false);

        sBtn = stageButton;
        oldPos = pos;
        oldButton = button;
        oldStageBtn = sBtn;

        // アニメーション中にする
        sBtn.anim = true;

        // ステージ情報UIの座標を設定
        Vector3 newPos = oldPos;
        newPos.x = Mathf.Clamp(newPos.x, -200.0f, 200.0f);
        newPos.y = Mathf.Clamp(newPos.y, -100.0f, 100.0f);
        uICon.stageSelectUI.stageInfoUI.transform.localPosition = newPos;

        // ステージ名を設定
        uICon.stageSelectUI.name.text = stageData.stageList[stageCon.stageNum].stageName;

        // ミッション名を設定
        switch (stageData.stageList[stageCon.stageNum].missionNum)
        {
            case 0: // 全ての惑星を破壊
                uICon.stageSelectUI.mission.text = "Destroy all planets";
                break;
            case 1: // 時間内にゴールにたどり着け
                uICon.stageSelectUI.mission.text = "Reach the goal";
                break;
        }

        // ステージボタンを動かす
        StartCoroutine(lerp.Position_GameObject(oldButton, oldButton.transform.localPosition, newPos + new Vector3(-85.0f, 20.0f, 0.0f), fadeTime));
        StartCoroutine(lerp.Scale_GameObject(oldButton, oldButton.transform.localScale, new Vector2(0.4f, 0.4f), fadeTime));

        // ステージ情報UIを表示
        uICon.stageSelectUI.stageInfoUI.SetActive(true);
        StartCoroutine(lerp.Scale_GameObject(uICon.stageSelectUI.stageInfoUI, new Vector2(0.0f, 0.0f), new Vector2(1.0f, 1.0f), fadeTime));
    }

    // ステージボタンの見た目を元に戻す
    void ResetDetail(bool orFast)
    {
        if (oldButton != null)
        {
            // 処理を高速で行うなら線形補完を行わずに直接値を変える
            if(orFast)
            {
                oldButton.transform.localPosition = oldPos;
                oldButton.transform.localScale = new Vector2(1.0f, 1.0f);
            }
            // 高速で処理を行わないなら線形補完を使用して値を変える
            else
            {
                StartCoroutine(lerp.Position_GameObject(oldButton, oldButton.transform.localPosition, oldPos, fadeTime));
                StartCoroutine(lerp.Scale_GameObject(oldButton, oldButton.transform.localScale, new Vector2(1.0f, 1.0f), fadeTime));
            }

            // ボタンをアニメーション中ではなくする
            oldStageBtn.anim = false;
        }
    }

    // ステージ情報UIを非表示
    public void HideStageInfo(bool orFast)
    {
        // ステージ情報UIを非表示
        uICon.stageSelectUI.stageInfoUI.SetActive(false);

        // ステージボタンが存在していたら見た目を元に戻す
        ResetDetail(orFast);
    }

    void OnEnable()
    {
        uICon ??= UIController.instance;

        // ステージ情報UIを非表示
        HideStageInfo(true);
    }
}
