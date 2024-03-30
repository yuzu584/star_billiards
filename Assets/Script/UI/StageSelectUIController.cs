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
    [SerializeField] private Text missionName;                  // ステージのミッション名のテキスト

    private StageController stageCon;

    private GameObject oldButton;                               // 取得したステージボタン
    private Vector3 oldPos;                                     // 取得したステージボタンの座標
    private StageButton oldStageBtn;                            // 取得したStageButton
    private float fadeTime = 0.4f;                              // フェード時間
    private StageButton sBtn;                                   // StageButtonを代入する変数

    private Localize localize;
    private Lerp lerp;

    void Start()
    {
        stageCon ??= StageController.instance;

        stageCon.DSIdele += DrawStageInfo;
        
        localize = Localize.instance;
        lerp ??= gameObject.AddComponent<Lerp>();

        // テキストのフォントを設定
        stageName.font = localize.GetFont();
        missionName.font = localize.GetFont();
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
        newPos.x = Mathf.Clamp(newPos.x, -200.0f, 200.0f);
        newPos.y = Mathf.Clamp(newPos.y, -100.0f, 100.0f);
        stageInfoObj.transform.localPosition = newPos;

        stageCon ??= StageController.instance;

        // ステージ名を設定
        switch (stageCon.stageNum)
        {
            case 0: stageName.text = localize.GetString(StringGroup.StageName, StringType.Stage1); break;
            case 1: stageName.text = localize.GetString(StringGroup.StageName, StringType.Stage2); break;
            case 2: stageName.text = localize.GetString(StringGroup.StageName, StringType.Stage3); break;
            case 3: stageName.text = localize.GetString(StringGroup.StageName, StringType.Stage4); break;
            case 4: stageName.text = localize.GetString(StringGroup.StageName, StringType.Stage5); break;
            default:break;
        }

        // ミッション名を設定
        switch (stageData.stageList[stageCon.stageNum].missionNum)
        {
            case 0: // 惑星を破壊しろ
                missionName.text = localize.GetString(StringGroup.Mission, StringType.DestroyPlanet);
                break;
            case 1: // ゴールにたどり着け
                missionName.text = localize.GetString(StringGroup.Mission, StringType.ReachTheGoal);
                break;
        }

        // ステージボタンを動かす
        StartCoroutine(lerp.Position_GameObject(oldButton, oldButton.transform.localPosition, newPos + new Vector3(-85.0f, 20.0f, 0.0f), fadeTime));
        StartCoroutine(lerp.Scale_GameObject(oldButton, oldButton.transform.localScale, new Vector2(0.4f, 0.4f), fadeTime));

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
