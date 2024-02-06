using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// �{�^���̌����ڂ��Ǘ�
public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float fadeTime;                                // �t�F�[�h����
    [SerializeField] private Image image;                                   // �{�^���̉摜
    [SerializeField] private Text text;                                  // �{�^���̃e�L�X�g
    public enum ClickAction                                                 // �{�^�����������Ƃ��̌���
    {
        None,             // ���ʂȂ�
        ReturnToGame,     // �Q�[���ɖ߂�
        Setting,          // �ݒ��ʂ��J��
        ReturnToMainMenu, // ���C�����j���[�ɖ߂�
        StageSelect,      // �X�e�[�W�I����ʂɖ߂�
        StageStart,       // �X�e�[�W�X�^�[�g
        PlanetList,       // �f������ʂ��J��
        SkillSelect,      // �X�L���I����ʂ��J��
        ApplySkill,       // �I�������X�L����K�p
        ResetSelectSkill, // �I�������X�L�������Z�b�g
    }
    public ClickAction clickAction;                                         // �{�^�����������Ƃ��̌���

    private Vector3 defaultPos;                             // �����ʒu
    private Vector3 moveDistance = new Vector3(-3, -3, 0);  // �ړ�����
    private Color defaultColor;                             // �f�t�H���g�̐F
    private Color fadeColor = new Color(0, 0, 0, 0.1f);     // �ς��F

    // Find�ŒT��GameObject
    private GameObject ScreenController;
    private GameObject Canvas;
    private GameObject UIFunctionController;
    private GameObject PlanetInfo;
    private GameObject ArrowController;
    private GameObject StageController;
    private GameObject Player;
    private GameObject InitializeController;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    private ScreenController screenController;
    private UIController uIController;
    private PauseUIController pauseUIController;
    private Arrow arrow;
    private StageController stageController;
    private Lerp lerp;
    private SkillController skillController;
    private Initialize initialize;

    // �}�E�X�|�C���^�[���{�^���̏�ɏ������
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �{�^���̃A�j���[�V����
        StopAllCoroutines();
        StartCoroutine(lerp.Color_Image(image, defaultColor, defaultColor + fadeColor, fadeTime));
        StartCoroutine(lerp.Position_Image(image, defaultPos, defaultPos + moveDistance, fadeTime));
    }

    // �}�E�X�|�C���^�[���{�^���̏ォ�痣�ꂽ��
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �{�^���̃A�j���[�V����
        StopAllCoroutines();
        StartCoroutine(lerp.Color_Image(image, defaultColor + fadeColor, defaultColor, fadeTime));
        StartCoroutine(lerp.Position_Image(image, defaultPos + moveDistance, defaultPos, fadeTime));
    }
    
    // �{�^�����N���b�N���ꂽ��
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // �{�^���̐F�����Z�b�g
        image.color = defaultColor;

        // �{�^�����������Ƃ��̌��ʂɂ���ĕ���
        switch (clickAction)
        {
            case ClickAction.ReturnToGame:     // �Q�[���ɖ߂�
                ReturnToGame();
                break;
            case ClickAction.Setting:          // �ݒ��ʂ��J��
                Setting();
                break;
            case ClickAction.ReturnToMainMenu: // ���C�����j���[�ɖ߂�
                ReturnToMainMenu();
                break;
            case ClickAction.StageSelect:      // �X�e�[�W�I����ʂɑJ��
                StageSelect();
                break;
            case ClickAction.StageStart:       // �X�e�[�W�X�^�[�g
                StageStart();
                break;
            case ClickAction.PlanetList:       // �f�����X�g��ʂ��J��
                PlanetList();
                break;
            case ClickAction.SkillSelect:      // �X�L���I����ʂ��J��
                SkillSelect();
                break;
            case ClickAction.ApplySkill:       // �I�������X�L����K�p
                ApplySkill();
                break;
            case ClickAction.ResetSelectSkill: // �I�������X�L�������Z�b�g
                ResetSelectSkill();
                break;
            default:
                break;
        }
    }

    // �Q�[���ɖ߂�
    void ReturnToGame()
    {
        // ��ʔԍ���InGame�ɕύX
        screenController.screenNum = 5;

        // �f�����UI��\��
        PlanetInfo.SetActive(true);
    }

    // �ݒ��ʂ��J��
    void Setting()
    {
        // ��ʔԍ���Setting�ɕύX
        screenController.screenNum = 3;
    }

    // ���C�����j���[�ɖ߂�
    void ReturnToMainMenu()
    {
        // ��ʔԍ���MainMenu�ɕύX
        screenController.screenNum = 1;
    }

    // �X�e�[�W�I����ʂɑJ��
    void StageSelect()
    {
        // ��ʔԍ���StageSelect�ɕύX
        screenController.screenNum = 2;
    }

    // �X�e�[�W�X�^�[�g
    void StageStart()
    {
        // ��ʔԍ���InGame�ɕύX
        screenController.screenNum = 5;

        // �X�e�[�W�Ɋւ��鐔�l��������
        initialize.init_Stage();
    }

    // �f������ʂ��J��
    void PlanetList()
    {
        GameObject target = GameObject.Find(transform.parent.gameObject.name);
        arrow.Create(target);
    }

    // �X�L���I����ʂ��J��
    void SkillSelect()
    {
        // ��ʔԍ���SkillSelect�ɕύX
        screenController.screenNum = 4;
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

    void Start()
    {
        // �����l��ݒ�
        defaultPos = image.rectTransform.position;
        defaultColor = image.color;

        // GameObject��T��
        ScreenController = GameObject.Find("ScreenController");
        Canvas = GameObject.Find("Canvas");
        UIFunctionController = GameObject.Find("UIFunctionController");
        PlanetInfo = GameObject.Find("Planet Info");
        ArrowController = GameObject.Find("ArrowController");
        StageController = GameObject.Find("StageController");
        Player = GameObject.Find("Player");
        InitializeController = GameObject.Find("InitializeController");

        // �T����GameObject�̃R���|�[�l���g���擾
        screenController = ScreenController.gameObject.GetComponent<ScreenController>();
        uIController = Canvas.gameObject.GetComponent<UIController>();
        pauseUIController = UIFunctionController.GetComponent<PauseUIController>();
        arrow = ArrowController.GetComponent<Arrow>();
        stageController = StageController.GetComponent<StageController>();
        lerp = UIFunctionController.GetComponent<Lerp>();
        skillController = Player.GetComponent<SkillController>();
        initialize = InitializeController.GetComponent<Initialize>();
    }
}
