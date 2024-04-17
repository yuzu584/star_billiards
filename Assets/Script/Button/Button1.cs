using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �{�^��1���Ǘ�
public class Button1 : Button
{
    private Arrow arrow;
    private SkillSelect skillSelect;
    private PopupManager popupMana;
    private Localize localize;

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
        base.EnterProcess();

        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, true);
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    public override void ExitProcess()
    {
        base.ExitProcess();

        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, false);
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    public override void ClickProcess()
    {
        base.ClickProcess();

        // �{�^�����������Ƃ��̌��ʂɂ���ĕ���
        switch (clickAction)
        {
            case ClickAction.ChangeScreen:          ChangeScreen();             break;  // ��ʑJ��
            case ClickAction.CreatePlanetDirArrow:  CreatePlanetDirArrow();     break;  // �f���̕������w���������𐶐�
            case ClickAction.ApplySkill:            ApplySkill();               break;  // �I�������X�L����K�p
            case ClickAction.ResetSelectSkill:      ResetSelectSkill();         break;  // �I�������X�L�������Z�b�g
            case ClickAction.ExitGame:              ExitGame();                 break;  // �Q�[���I��
            case ClickAction.Action:                action?.Invoke();           break;  // �C�ӂ� Action �����s
            default: break;
        }
    }

    // ��ʑJ��
    private void ChangeScreen()
    {
        scrCon.Screen = nextScreen;
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
            popupMana.DrawPopup(PopupManager.PopupType.InMenuPopup1, localize.GetString("message_data", "apply_skill"));
        }
        else
        {
            // �|�b�v�A�b�v�\��
            popupMana.DrawPopup(PopupManager.PopupType.InMenuPopup1, localize.GetString("message_data", "plaese_select_3_skills"));
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
        GameObject g = popupMana.DrawPopup(PopupManager.PopupType.DialogPopup1, localize.GetString("message_data", "exit_game"));

        // �|�b�v�A�b�v�����ς݂Ȃ�I��
        if (g == null) return;

        scrCon.ScreenLoot = 1;

        // �|�b�v�A�b�v�̃R���|�[�l���g���擾
        DialogPopup1 dp1 = g.GetComponent<DialogPopup1>();

        // �|�b�v�A�b�v�̃{�^���̒l��ݒ�
        dp1.SetScreenAndLoot(ScreenController.ScreenType.MainMenu, 1);

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
    }

    protected override void Start()
    {
        base.Start();

        arrow = Arrow.instance;
        skillSelect = SkillSelect.instance;
        popupMana = PopupManager.instance;
        localize = Localize.instance;
    }
}
