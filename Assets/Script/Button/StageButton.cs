using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : Button
{
    [SerializeField]
    private int num; // �Z�b�g����X�e�[�W�ԍ�

    // Find�ŒT��GameObject
    private GameObject StageController;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    private StageSelectUIController stageSelectUIController;
    private StageController stageController;

    // �}�E�X�|�C���^�[����������̏���
    protected override void EnterProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, true);
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    protected override void ExitProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, false);
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    protected override void ClickProcess()
    {
        stageController.stageNum = num;
        stageSelectUIController.DrawStageInfo();
    }

    void OnEnable()
    {
        // �{�^���̏���������
        BtnInit(imageStructs, textStructs);

        // �X�e�[�W���UI���\��
        if (stageSelectUIController != null)
            stageSelectUIController.HideStageInfo();
    }

    new void Start()
    {
        base.Start();

        // �I�u�W�F�N�g��T���ăR���|�[�l���g���擾
        StageController = GameObject.Find("StageController");

        stageSelectUIController = UIFunctionController.GetComponent<StageSelectUIController>();
        stageController = StageController.GetComponent<StageController>();
    }
}
