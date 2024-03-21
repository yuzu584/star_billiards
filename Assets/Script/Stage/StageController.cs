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
    [SerializeField] private ScreenData screenData;                 // InspectorでScreenDataを指定

    private ScreenController scrCon;


    private int missionNum;                                         // ミッション番号

    public int stageNum = 0;                                        // ステージ番号
    public delegate void StageCrearDele();                          // ステージクリア時のデリゲート
    public delegate void GameOverDele();                            // ゲームオーバー時のデリゲート
    public StageCrearDele stageCrearDele;
    public GameOverDele gameOverDele;

    void Start()
    {
        scrCon = ScreenController.instance;
    }

    void Update()
    {
        // ミッション番号を代入
        if (missionNum != stageData.stageList[stageNum].missionNum)
            missionNum = stageData.stageList[stageNum].missionNum;

        // ミッションが"全ての惑星を破壊"かつクリア条件を達成したなら(ゲーム画面で)
        if ((missionNum == 0) && (planetAmount.planetDestroyAmount >= stageData.stageList[stageNum].planetNum) && (scrCon.ScreenNum == 5))
        {
            // ステージクリア時のデリゲートを発火
            stageCrearDele();
        }

        // ステージを表示/非表示
        if(createStage.NowRenderState() != screenData.screenList[scrCon.ScreenNum].drawStage)
            createStage.Render(screenData.screenList[scrCon.ScreenNum].drawStage);
    }
}
