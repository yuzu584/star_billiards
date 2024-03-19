using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �{�^��1���Ǘ�
public class Button1 : Button
{
    // Find�ŒT����GameObject�̃R���|�[�l���g
    private SkillController skillCon;
    private Arrow arrow;
    private Initialize init;
    private CreateStage cStage;

    public enum ClickAction // �{�^�����������Ƃ��̌���
    {
        None,                   // ���ʂȂ�
        ChangeScreen,           // �w�肵����ʂɑJ��
        StageStart,             // �X�e�[�W�X�^�[�g
        CreatePlanetDirArrow,   // �f���̕������w���������𐶐�
        ApplySkill,             // �I�������X�L����K�p
        ResetSelectSkill,       // �I�������X�L�������Z�b�g
        ExitGame,               // �Q�[���I��
    }

    [SerializeField] private ClickAction clickAction;
    [SerializeField] private int nextScreen = 0; // �J�ڐ�̉�ʔԍ�

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
            case ClickAction.ChangeScreen:          // ��ʑJ��
                ChangeScreen();
                break;
            case ClickAction.StageStart:            // �X�e�[�W�X�^�[�g
                StageStart();
                break;
            case ClickAction.CreatePlanetDirArrow:  // �f���̕������w���������𐶐�
                CreatePlanetDirArrow();
                break;
            case ClickAction.ApplySkill:            // �I�������X�L����K�p
                ApplySkill();
                break;
            case ClickAction.ResetSelectSkill:      // �I�������X�L�������Z�b�g
                ResetSelectSkill();
                break;
            case ClickAction.ExitGame:              // �Q�[���I��
                ExitGame();
                break;
            default:
                break;
        }
    }

    // ��ʑJ��
    private void ChangeScreen()
    {
        scrCon.ScreenNum = nextScreen;
    }

    // �X�e�[�W�X�^�[�g
    void StageStart()
    {
        // ��ʔԍ���InGame�ɕύX
        scrCon.ScreenNum = 5;
        scrCon.ScreenLoot = 0;

        // �X�e�[�W�Ɋւ��鐔�l��������
        init.init_Stage();

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
        skillCon.SetSelectSlot();
    }

    // �I�������X�L�������Z�b�g
    void ResetSelectSkill()
    {
        skillCon.InitSelectSlot();
    }

    // �Q�[�����I��
    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //�Q�[���v���C�I��
#else
        Application.Quit();//�Q�[���v���C�I��
#endif
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

        skillCon = SkillController.instance;
        arrow = Arrow.instance;
        init = Initialize.instance;
        cStage = CreateStage.instance;
    }
}
