using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージを管理
public class StageController : Singleton<StageController>
{
    [SerializeField] private StageData stageData;                   // InspectorでStageDataを指定
    [SerializeField] private PlanetAmount planetAmount;             // InspectorでPlanetAmountを指定
    [SerializeField] private CreateStage createStage;               // InspectorでCreateStageを指定
    [SerializeField] private Initialize initialize;                 // InspectorでInitializeを指定

    private ScreenController scrCon;

    public int stageNum = 0;                                        // ステージ番号
    public bool stageCrear = false;                                 // ステージをクリアしたかどうか
    public bool gameOver = false;                                   // ゲームオーバーかどうか

    private int missionNum;              // ミッション番号
    private bool stageCreated = false;   // ステージを生成したか

    // ステージクリア時の処理
    void StageCrear()
    {
        stageCrear = true;
    }

    // ゲームオーバー時の処理
    void GameOver()
    {
        gameOver = true;
    }

    // ステージに関する数値を初期化
    public void Init()
    {
        stageCrear = false;
        gameOver = false;
    }

    void Start()
    {
        scrCon = ScreenController.instance;

        // デリゲートに初期化関数を登録
        initialize.init_Stage += Init;
    }

    void Update()
    {
        // ミッション番号を代入
        if (missionNum != stageData.stageList[stageNum].missionNum)
            missionNum = stageData.stageList[stageNum].missionNum;

        // ミッションが"全ての惑星を破壊"かつクリア条件を達成したなら(ゲーム画面で)
        if ((missionNum == 0) && (planetAmount.planetDestroyAmount >= stageData.stageList[stageNum].planetNum) && (scrCon.ScreenNum == 5))
        {
            // ステージクリア
            StageCrear();
        }

        // ステージを表示/非表示
        if (stageCreated != scrCon.canStageDraw)
        {
            stageCreated = scrCon.canStageDraw;
            createStage.Draw(scrCon.canStageDraw);
        }
    }
}
