using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W���Ǘ�
public class StageController : Singleton<StageController>
{
    [SerializeField] private StageData stageData;                   // Inspector��StageData���w��
    [SerializeField] private PlanetAmount planetAmount;             // Inspector��PlanetAmount���w��
    [SerializeField] private CreateStage createStage;               // Inspector��CreateStage���w��
    [SerializeField] private Initialize initialize;                 // Inspector��Initialize���w��
    [SerializeField] private ScreenData screenData;                 // Inspector��ScreenData���w��

    private ScreenController scrCon;


    private int missionNum;                                         // �~�b�V�����ԍ�

    public int stageNum = 0;                                        // �X�e�[�W�ԍ�
    public delegate void StageCrearDele();                          // �X�e�[�W�N���A���̃f���Q�[�g
    public delegate void GameOverDele();                            // �Q�[���I�[�o�[���̃f���Q�[�g
    public StageCrearDele stageCrearDele;
    public GameOverDele gameOverDele;

    void Start()
    {
        scrCon = ScreenController.instance;
    }

    void Update()
    {
        // �~�b�V�����ԍ�����
        if (missionNum != stageData.stageList[stageNum].missionNum)
            missionNum = stageData.stageList[stageNum].missionNum;

        // �~�b�V������"�S�Ă̘f����j��"���N���A������B�������Ȃ�(�Q�[����ʂ�)
        if ((missionNum == 0) && (planetAmount.planetDestroyAmount >= stageData.stageList[stageNum].planetNum) && (scrCon.ScreenNum == 5))
        {
            // �X�e�[�W�N���A���̃f���Q�[�g�𔭉�
            stageCrearDele();
        }

        // �X�e�[�W��\��/��\��
        if(createStage.NowRenderState() != screenData.screenList[scrCon.ScreenNum].drawStage)
            createStage.Render(screenData.screenList[scrCon.ScreenNum].drawStage);
    }
}
