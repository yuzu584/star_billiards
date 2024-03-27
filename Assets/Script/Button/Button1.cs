using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �{�^��1���Ǘ�
public class Button1 : Button
{
    private Arrow arrow;
    private Initialize init;
    private CreateStage cStage;
    private SkillSelect skillSelect;
    private PopupManager popupMana;

    public enum ClickAction // �{�^�����������Ƃ��̌���
    {
        None,                   // ���ʂȂ�
        ChangeScreen,           // �w�肵����ʂɑJ��
        StageStart,             // �X�e�[�W�X�^�[�g
        CreatePlanetDirArrow,   // �f���̕������w���������𐶐�
        ApplySkill,             // �I�������X�L����K�p
        ResetSelectSkill,       // �I�������X�L�������Z�b�g
        ExitGame,               // �Q�[���I��
        Action,                 // �C�ӂ� Action �����s
    }

    [SerializeField] private ClickAction clickAction;
    [SerializeField] private ScreenController.ScreenType nextScreen = 0; // �J�ڐ�̉��

    public Action action;

    // �}�E�X�|�C���^�[����������̏���
    public override void EnterProcess()
    {
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
        // �{�^�����������Ƃ��̌��ʂɂ���ĕ���
        switch (clickAction)
        {
            case ClickAction.ChangeScreen:          ChangeScreen();             break;  // ��ʑJ��
            case ClickAction.StageStart:            StageStart();               break;  // �X�e�[�W�X�^�[�g
            case ClickAction.CreatePlanetDirArrow:  CreatePlanetDirArrow();     break;  // �f���̕������w���������𐶐�
            case ClickAction.ApplySkill:            ApplySkill();               break;  // �I�������X�L����K�p
            case ClickAction.ResetSelectSkill:      ResetSelectSkill();         break;  // �I�������X�L�������Z�b�g
            case ClickAction.ExitGame:              ExitGame();                 break;  // �Q�[���I��
            case ClickAction.Action:                if(action != null)action(); break;  // �C�ӂ� Action �����s
            default: break;
        }
    }

    // ��ʑJ��
    private void ChangeScreen()
    {
        scrCon.Screen = nextScreen;
    }

    // �X�e�[�W�X�^�[�g
    void StageStart()
    {
        // ��ʂ�InGame�ɕύX
        scrCon.Screen = ScreenController.ScreenType.InGame;
        scrCon.ScreenLoot = 0;

        // �X�e�[�W�Ɋւ��鐔�l��������
        init.init_Stage();

        // �|�b�v�A�b�v�̔z���������
        popupMana.Init(popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1]);

        // �X�e�[�W����
        cStage.Destroy();
        cStage.Create();
    }

    // �f���̕������w���������𐶐�
    void CreatePlanetDirArrow()
    {
        GameObject target = GameObject.Find(transform.parent.gameObject.name);
        arrow.Create(target);
    }

    // �I�������X�L����K�p
    void ApplySkill()
    {
        // �X�L����3�I������������
        if (skillSelect.CheckNone())
        {
            // �X�L�����m��
            skillSelect.SetSelectSlot();

            // �|�b�v�A�b�v�\��
            popupMana.DrawPopup(PopupManager.PopupType.InMenuPopup1, "was decided");
        }
        else
        {
            // �|�b�v�A�b�v�\��
            popupMana.DrawPopup(PopupManager.PopupType.InMenuPopup1, "Please select 3 skills");
        }
    }

    // �I�������X�L�������Z�b�g
    void ResetSelectSkill()
    {
        skillSelect.InitSelectSlot();
    }

    // �Q�[�����I��
    void ExitGame()
    {
        // �_�C�A���O�|�b�v�A�b�v�𐶐�
        GameObject g = popupMana.DrawPopup(PopupManager.PopupType.DialogPopup1, "Do you want to exit the game?");

        scrCon.ScreenLoot = 1;

        // �|�b�v�A�b�v�̃R���|�[�l���g���擾
        DialogPopup1 dp1 = g.GetComponent<DialogPopup1>();

        // �|�b�v�A�b�v�� OK �{�^�����������Ƃ��̏�����ݒ�
        dp1.Action = () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //�Q�[���v���C�I��
#else
        Application.Quit();//�Q�[���v���C�I��
#endif
        };
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // �{�^���̏���������
        BtnInit(imageStructs, textStructs);
    }

    protected override void Start()
    {
        base.Start();

        arrow = Arrow.instance;
        init = Initialize.instance;
        cStage = CreateStage.instance;
        skillSelect = SkillSelect.instance;
        popupMana = PopupManager.instance;
    }
}
