using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Const;

public class SkillSlot : Button
{
    [SerializeField] private int skillNum = 0;     // �X�L���ԍ�
    [SerializeField] private Text selectNumText;   // �X�L���̑I����������\���e�L�X�g

    // Find�ŒT������
    private GameObject Player;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    private SkillController skillController;
    private SkillSelectUIController skillSelectUIController;

    // �}�E�X�|�C���^�[����������̏���
    protected override void EnterProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, true);

        // �X�L���̏���`��
        if(skillSelectUIController != null)
            skillSelectUIController.DrawSkillInfo(skillNum);
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
        // ���ɑI�����ꂽ�X�L���łȂ���΁A�I�����Ă���X�L���X���b�g��ݒ�
        if (CheckDoubleSelect(skillNum))
        {
            SetSelectSlot(skillNum);
        }
    }

    // �X�L���̑I����������\���e�L�X�g��ݒ�
    void SetSelectNumText()
    {
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            if (skillController.selectSlot[i] == skillNum)
            {
                selectNumText.enabled = true;
                selectNumText.text = (i + 1).ToString("0");
                return;
            }
        }

        // �X�L�����Z�b�g����Ă��Ȃ���΃e�L�X�g���\��
        selectNumText.enabled = false;
    }

    // �I�����Ă���X�L���X���b�g��ݒ�
    void SetSelectSlot(int num)
    {
        if (skillController.count >= AppConst.SKILL_SLOT_AMOUNT)
        {
            skillController.InitSelectSlot();
        }
        skillController.selectSlot[skillController.count] = num;
        ++skillController.count;
    }

    // �����X�L����I�����Ă��Ȃ������m
    bool CheckDoubleSelect(int num)
    {
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            if (skillController.selectSlot[i] == num) { return false; }
        }
        return true;
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
        Player = GameObject.Find("Player");

        skillController = Player.GetComponent<SkillController>();
        skillSelectUIController = UIFunctionController.GetComponent<SkillSelectUIController>();
    }

    void Update()
    {
        // �X�L���̑I����������\���e�L�X�g��ݒ�
        SetSelectNumText();
    }
}
