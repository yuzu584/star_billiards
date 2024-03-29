using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class StageButton : Button
{
    [SerializeField]
    private int stageNum;           // �Z�b�g����X�e�[�W�ԍ�

    [SerializeField]
    private Text stageName;         // �X�e�[�W���̃e�L�X�g

    [SerializeField]
    private StageData stageData;    // �X�e�[�W�̃f�[�^���܂Ƃ߂�ScriptableObject

    [SerializeField]
    private Button1 startBtn;       // �X�e�[�W�X�^�[�g�̃{�^��

    public bool anim = false;       // �{�^�����A�j���[�V��������

    private StageController stageCon;

    // �}�E�X�|�C���^�[����������̏���
    public override void EnterProcess()
    {
        // �A�j���[�V�������ł͂Ȃ��Ȃ�
        if (!anim)
        {
            // �t�H�[�J�X��̃{�^����ݒ�
            startBtn.buttonUp = buttonUp;
            startBtn.buttonDown = buttonDown;
            startBtn.buttonRight = buttonRight;
            startBtn.buttonLeft = buttonLeft;

            focus.SetFocusBtn(startBtn);

            stageCon ??= StageController.instance;

            stageCon.stageNum = stageNum;
            stageCon.DSIdele?.Invoke(transform.localPosition, gameObject, this);
        }

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

    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        stageCon ??= StageController.instance;

        // �e�L�X�g���X�e�[�W���ɐݒ�
        stageName.text = stageData.stageList[stageNum].stageName;
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
