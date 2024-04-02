using Const;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ステージ選択画面のUIを管理
public class StageSelectUIController : MonoBehaviour
{
    [SerializeField] private StageData stageData;               // InspectorでStageDataを指定
    [SerializeField] private GameObject stageInfoObj;           // ステージの情報を表示するオブジェクト
    [SerializeField] private Text stageName;                    // ステージ名のテキスト
    [SerializeField] private Text missionText;                  // ステージのミッション名のテキスト
    [SerializeField] private Text timeLimitText;                // ステージの制限時間のテキスト
    [SerializeField] private Text scoreText;                    // ステージのスコアのテキスト

    private GameObject oldButton;                               // 取得したステージボタン
    private Vector3 oldPos;                                     // 取得したステージボタンの座標
    private StageButton oldStageBtn;                            // 取得したStageButton
    private float fadeTime = 0.4f;                              // フェード時間
    private StageButton sBtn;                                   // StageButtonを代入する変数

    private StageController stageCon;
    private Localize localize;
    private StageScore stageScore;
    private Lerp lerp;

    void Start()
    {
        stageCon ??= StageController.instance;

        stageCon.DSIdele += DrawStageInfo;
        
        localize = Localize.instance;
        stageScore = StageScore.instance;
        lerp ??= gameObject.AddComponent<Lerp>();

        // テキストのフォントを設定
        stageName.font = localize.GetFont();
        missionText.font = localize.GetFont();
    }

    private void OnDestroy()
    {
        stageCon.DSIdele -= DrawStageInfo;
    }

    // ステージ情報UIを描画
    void DrawStageInfo(Vector3 pos, GameObject button, StageButton stageButton)
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
        newPos.x = Mathf.Clamp(newPos.x, -270.0f, 270.0f);
        newPos.y = Mathf.Clamp(newPos.y, -50.0f, 50.0f);
        stageInfoObj.transform.localPosition = newPos;

        stageCon ??= StageController.instance;

        // ステージ名を設定
        stageName.text = localize.GetString_StageName((EnumStageName)Enum.ToObject(typeof(EnumStageName), stageCon.stageNum));

        // ミッション名を設定
        missionText.text = localize.GetString_Mission((EnumMission)Enum.ToObject(typeof(EnumMission), stageData.stageList[stageCon.stageNum].missionNum));

        // 制限時間のテキストを設定
        timeLimitText.text = stageData.stageList[stageCon.stageNum].timeLimit.ToString() + "s";

        // スコアのテキストを設定
        if(stageScore.score[stageCon.stageNum] == 0)
            scoreText.text = "--------";
        else
            scoreText.text = stageScore.score[stageCon.stageNum].ToString();

        // ステージボタンを動かす
        StartCoroutine(lerp.Position_GameObject(oldButton, oldButton.transform.localPosition, newPos + new Vector3(-73.0f, 95.0f, 0.0f), fadeTime));
        StartCoroutine(lerp.Scale_GameObject(oldButton, oldButton.transform.localScale, new Vector2(0.8f, 0.8f), fadeTime));

        // ステージ情報UIを表示
        stageInfoObj.SetActive(true);
        StartCoroutine(lerp.Scale_GameObject(stageInfoObj, new Vector2(0.0f, 0.0f), new Vector2(1.0f, 1.0f), fadeTime));
    }

    // ステージボタンの見た目を元に戻す
    void ResetDetail(bool orFast)
    {
        lerp ??= gameObject.AddComponent<Lerp>();

        if (oldButton != null)
        {
            // 処理を高速で行うなら線形補完を行わずに直接値を変える
            if(orFast)
            {
                lerp.StopAll();
                oldButton.transform.localPosition = oldPos;
                oldButton.transform.localScale = new Vector2(1.0f, 1.0f);
            }
            // 高速で処理を行わないなら線形補完を使用して値を変える
            else
            {
                lerp.StopAll();
                StartCoroutine(lerp.Position_GameObject(oldButton, oldButton.transform.localPosition, oldPos, fadeTime));
                StartCoroutine(lerp.Scale_GameObject(oldButton, oldButton.transform.localScale, new Vector2(1.0f, 1.0f), fadeTime));
            }

            // ボタンをアニメーション中ではなくする
            oldStageBtn.anim = false;
        }
    }
}
