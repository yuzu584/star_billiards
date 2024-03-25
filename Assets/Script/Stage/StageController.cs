using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W���Ǘ�
public class StageController : Singleton<StageController>
{
    [SerializeField] private ScreenData scrData;                    // Inspector��ScreenData���w��
    [SerializeField] private StageData stageData;                   // Inspector��StageData���w��

    private ScreenController scrCon;
    private PlanetAmount planetAmount;
    private CreateStage createStage;

    private int missionNum;                                         // �~�b�V�����ԍ�

    public int stageNum = 0;                                        // �X�e�[�W�ԍ�
    public delegate void StageCrearDele();                          // �X�e�[�W�N���A���̃f���Q�[�g
    public delegate void GameOverDele();                            // �Q�[���I�[�o�[���̃f���Q�[�g
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
        // �~�b�V�����ԍ�����
        if (missionNum != stageData.stageList[stageNum].missionNum)
            missionNum = stageData.stageList[stageNum].missionNum;

        // �~�b�V������"�S�Ă̘f����j��"���N���A������B�������Ȃ�(�Q�[����ʂ�)
        if ((missionNum == 0) && (planetAmount.planetDestroyAmount >= stageData.stageList[stageNum].planetNum) && (scrCon.Screen == ScreenController.ScreenType.InGame))
        {
            // �X�e�[�W�N���A���̃f���Q�[�g�𔭉�
            stageCrearDele();
        }

        // �X�e�[�W��\��/��\��
        if(createStage.NowRenderState() != scrData.screenList[(int)scrCon.Screen].drawStage)
            createStage.Render(scrData.screenList[(int)scrCon.Screen].drawStage);
    }
}
