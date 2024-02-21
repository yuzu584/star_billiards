using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W���Ǘ�
public class StageController : MonoBehaviour
{
    [SerializeField] private StageData stageData;                   // Inspector��StageData���w��
    [SerializeField] private PlanetAmount planetAmount;             // Inspector��PlanetAmount���w��
    [SerializeField] private CreateStage createStage;               // Inspector��CreateStage���w��
    [SerializeField] private ScreenController screenController;     // Inspector��ScreenController���w��
    [SerializeField] private Initialize initialize;                 // Inspector��Initialize���w��
    public int stageNum = 0;                                        // �X�e�[�W�ԍ�
    public bool stageCrear = false;                                 // �X�e�[�W���N���A�������ǂ���
    public bool gameOver = false;                                   // �Q�[���I�[�o�[���ǂ���

    private int missionNum;              // �~�b�V�����ԍ�
    private bool stageCreated = false;   // �X�e�[�W�𐶐�������

    // �X�e�[�W�N���A���̏���
    void StageCrear()
    {
        stageCrear = true;
    }

    // �Q�[���I�[�o�[���̏���
    void GameOver()
    {
        gameOver = true;
    }

    // �X�e�[�W�Ɋւ��鐔�l��������
    public void Init()
    {
        stageCrear = false;
        gameOver = false;
    }

    void Start()
    {
        // �f���Q�[�g�ɏ������֐���o�^
        initialize.init_Stage += Init;
    }

    void Update()
    {
        // �~�b�V�����ԍ�����
        if (missionNum != stageData.stageList[stageNum].missionNum)
            missionNum = stageData.stageList[stageNum].missionNum;

        // �~�b�V������"�S�Ă̘f����j��"���N���A������B�������Ȃ�
        if (missionNum == 0 && (planetAmount.planetDestroyAmount >= stageData.stageList[stageNum].planetNum))
        {
            // �X�e�[�W�N���A
            StageCrear();
        }

        // �X�e�[�W��\��/��\��
        if (stageCreated != screenController.canStageDraw)
        {
            stageCreated = screenController.canStageDraw;
            createStage.Draw(screenController.canStageDraw);
        }
    }
}
