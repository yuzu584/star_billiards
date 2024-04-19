using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public Vector3 defaultPos;      // �{�^���̏����ʒu

    private StageController stageCon;
    private Initialize init;
    private CreateStage cStage;
    private PopupManager popupMana;

    public override void OnPointerExit(PointerEventData pointerEventData)
    {
        ExitProcess();
    }

    // �}�E�X�|�C���^�[����������̏���
    public override void EnterProcess()
    {
        base.EnterProcess();

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

            // �X�e�[�W�J�n�{�^�����������Ƃ��̋�����ݒ�
            startBtn.action = () =>
            {
                stageCon.stageNum = stageNum;

                // ��ʂ�InGame�ɕύX
                scrCon.Screen = ScreenController.ScreenType.InGame;

                // �X�e�[�W�Ɋւ��鐔�l��������
                init.init_Stage();

                // �|�b�v�A�b�v�̔z���������
                popupMana.Init(popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1]);

                // �X�e�[�W����
                cStage.Destroy();
                cStage.Create();
            };

            stageCon.DSIdele?.Invoke(defaultPos, gameObject, this);
        }
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    public override void ExitProcess()
    {
        base.ExitProcess();
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    public override void ClickProcess()
    {
        base.ClickProcess();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        defaultPos = gameObject.transform.localPosition;
    }

    protected override void Start()
    {
        base.Start();

        stageCon ??= StageController.instance;
        init = Initialize.instance;
        cStage = CreateStage.instance;
        popupMana = PopupManager.instance;
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
