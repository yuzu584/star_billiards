using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージを管理
public class StageController : Singleton<StageController>
{
    [SerializeField] private ScreenData scrData;                    // InspectorでScreenDataを指定
    [SerializeField] private StageData stageData;                   // InspectorでStageDataを指定

    private ScreenController scrCon;
    private PlanetAmount planetAmount;
    private CreateStage createStage;

    private int missionNum;                                         // ミッション番号

    public int stageNum = 0;                                        // ステージ番号
    public delegate void StageCrearDele();                          // ステージクリア時のデリゲート
    public delegate void GameOverDele();                            // ゲームオーバー時のデリゲート
    public StageCrearDele stageCrearDele;
    public GameOverDele gameOverDele;

    void Start()
    {
        scrCon = ScreenController.instance;
        planetAmount = PlanetAmount.instance;
        createStage = CreateStage.instance;
    }

    void Update()
    {
        // ミッション番号を代入
        if (missionNum != stageData.stageList[stageNum].missionNum)
            missionNum = stageData.stageList[stageNum].missionNum;

        // ミッションが"全ての惑星を破壊"かつクリア条件を達成したなら(ゲーム画面で)
        if ((missionNum == 0) && (planetAmount.planetDestroyAmount >= stageData.stageList[stageNum].planetNum) && (scrCon.Screen == ScreenController.ScreenType.InGame))
        {
            // ステージクリア時のデリゲートを発火
            stageCrearDele();
        }

        // ステージを表示/非表示
        if(createStage.NowRenderState() != scrData.screenList[(int)scrCon.Screen].drawStage)
            createStage.Render(scrData.screenList[(int)scrCon.Screen].drawStage);
    }
}
