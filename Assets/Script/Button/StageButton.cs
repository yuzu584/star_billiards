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

    private StageSelectUIController stageSelectUICon;
    private StageController stageCon;

    // �}�E�X�|�C���^�[����������̏���
    public override void EnterProcess()
    {
        // �K�w��ݒ�
        scrCon.ScreenLoot = 0;

        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, true);
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    public override void ExitProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, false);
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    public override void ClickProcess()
    {
        // �A�j���[�V�������ł͂Ȃ��Ȃ�
        if (!anim)
        {
            // �K�w��ݒ�
            scrCon.ScreenLoot = 1;

            stageCon.stageNum = num;
            stageSelectUICon.DrawStageInfo(this.transform.localPosition, this.gameObject, this);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        stageSelectUICon = StageSelectUIController.instance;
        stageCon = StageController.instance;

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
