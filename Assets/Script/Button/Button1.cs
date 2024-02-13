using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;

// �{�^��1���Ǘ�
public class Button1 : Button
{
    // Find�ŒT��GameObject
    private GameObject Player;
    private GameObject ArrowController;
    private GameObject InitializeController;
    private GameObject Stage;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    private SkillController skillController;
    private Arrow arrow;
    private Initialize initialize;
    private CreateStage createStage;

    public enum ClickAction // �{�^�����������Ƃ��̌���
    {
        None,                 // ���ʂȂ�
        ChangeScreen,         // �w�肵����ʂɑJ��
        StageStart,           // �X�e�[�W�X�^�[�g
        CreatePlanetDirArrow, // �f���̕������w���������𐶐�
        ApplySkill,           // �I�������X�L����K�p
        ResetSelectSkill,     // �I�������X�L�������Z�b�g
    }

    [SerializeField] private ClickAction clickAction;
    [SerializeField] private int nextScreen = 0; // �J�ڐ�̉�ʔԍ�

    // �}�E�X�|�C���^�[����������̏���
    protected override void EnterProcess()
    {
        // �{�^���̃A�j���[�V����
        StopAllCoroutines();
        for (int i = 0; i < imageStructs.Length; i++)
        {
            StartCoroutine(lerp.Color_Image(imageStructs[i].image, imageStructs[i].startColor, imageStructs[i].endColor, imageStructs[i].fadeTime));
            StartCoroutine(lerp.Color_Text(textStructs[i].text, textStructs[i].startColor, textStructs[i].endColor, textStructs[i].fadeTime));
        }
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    protected override void ExitProcess()
    {
        // �{�^���̃A�j���[�V����
        StopAllCoroutines();
        for (int i = 0; i < imageStructs.Length; i++)
        {
            StartCoroutine(lerp.Color_Image(imageStructs[i].image, imageStructs[i].endColor, imageStructs[i].startColor, imageStructs[i].fadeTime));
            StartCoroutine(lerp.Color_Text(textStructs[i].text, textStructs[i].endColor, textStructs[i].startColor, textStructs[i].fadeTime));
        }
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    protected override void ClickProcess()
    {
        // �{�^�����������Ƃ��̌��ʂɂ���ĕ���
        switch (clickAction)
        {
            case ClickAction.ChangeScreen:         // ��ʑJ��
                ChangeScreen();
                break;
            case ClickAction.StageStart:           // �X�e�[�W�X�^�[�g
                StageStart();
                break;
            case ClickAction.CreatePlanetDirArrow: // �f���̕������w���������𐶐�
                CreatePlanetDirArrow();
                break;
            case ClickAction.ApplySkill:           // �I�������X�L����K�p
                ApplySkill();
                break;
            case ClickAction.ResetSelectSkill:     // �I�������X�L�������Z�b�g
                ResetSelectSkill();
                break;
            default:
                break;
        }
    }

    // ��ʑJ��
    private void ChangeScreen()
    {
        screenController.screenNum = nextScreen;
    }

    // �X�e�[�W�X�^�[�g
    void StageStart()
    {
        // ��ʔԍ���InGame�ɕύX
        screenController.screenNum = 5;

        // �X�e�[�W�Ɋւ��鐔�l��������
        initialize.init_Stage();

        // �X�e�[�W����
        createStage.Destroy();
        createStage.Create();
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
        skillController.SetSelectSlot();
    }

    // �I�������X�L�������Z�b�g
    void ResetSelectSkill()
    {
        skillController.InitSelectSlot();
    }

    void OnEnable()
    {
        // �{�^���̐F�����Z�b�g
        StopAllCoroutines();
        for (int i = 0; i < imageStructs.Length; i++)
        {
            imageStructs[i].image.color = imageStructs[i].startColor;
            textStructs[i].text.color = textStructs[i].startColor;
        }
    }

    new void Start()
    {
        base.Start();

        // �I�u�W�F�N�g��T���ăR���|�[�l���g���擾
        Player = GameObject.Find("Player");
        ArrowController = GameObject.Find("ArrowController");
        InitializeController = GameObject.Find("InitializeController");
        Stage = GameObject.Find("Stage");
        skillController = Player.GetComponent<SkillController>();
        arrow = ArrowController.GetComponent<Arrow>();
        initialize = InitializeController.GetComponent<Initialize>();
        createStage = Stage.GetComponent<CreateStage>();
    }
}
