using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class StageButton : Button
{
    [SerializeField]
    private int num; // �Z�b�g����X�e�[�W�ԍ�

    [SerializeField]
    private Text stageName; // �X�e�[�W���̃e�L�X�g

    [SerializeField]
    private StageData stageData;  // �X�e�[�W�̃f�[�^���܂Ƃ߂�ScriptableObject

    public bool anim = false; // �{�^�����A�j���[�V��������

    // Find�ŒT��GameObject
    private GameObject StageController;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    private StageSelectUIController stageSelectUIController;
    private StageController stageController;

    // �}�E�X�|�C���^�[����������̏���
    protected override void EnterProcess()
    {
        // �K�w��ݒ�
        screenController.ScreenLoot = 0;

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
        // �A�j���[�V�������ł͂Ȃ��Ȃ�
        if (!anim)
        {
            // �K�w��ݒ�
            screenController.ScreenLoot = 1;

            stageController.stageNum = num;
            stageSelectUIController.DrawStageInfo(this.transform.localPosition, this.gameObject, this);
        }
    }

    new void OnEnable()
    {
        base.OnEnable();

        // �{�^���̏���������
        BtnInit(imageStructs, textStructs);
    }

    new void Start()
    {
        base.Start();

        // �I�u�W�F�N�g��T���ăR���|�[�l���g���擾
        StageController = GameObject.Find("StageController");

        stageSelectUIController = UIFunctionController.GetComponent<StageSelectUIController>();
        stageController = StageController.GetComponent<StageController>();

        // �e�L�X�g���X�e�[�W���ɐݒ�
        stageName.text = stageData.stageList[num].stageName;
    }

    void Update()
    {
        if (anim)
        {
            stageName.enabled = false;
        }
        else
        {
            stageName.enabled = true;
        }
    }
}
