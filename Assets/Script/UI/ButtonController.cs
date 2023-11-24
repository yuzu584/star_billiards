using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// �{�^���̌����ڂ��Ǘ�
public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color OnPointerColor;                          // �|�C���^�[����������̐F
    [SerializeField] private Color defaultColor;                            // �f�t�H���g�̐F
    [SerializeField] private float fadeTime;                                // �t�F�[�h����
    [SerializeField] private Image Btn;                                     // �{�^���̉摜
    [SerializeField] private Text BtnText;                                  // �{�^���̃e�L�X�g
    [SerializeField] private enum ClickAction                               // �{�^�����������Ƃ��̌���
    {
        ReturnToGame,     // �Q�[���ɖ߂�
        Setting,          // �ݒ��ʂ��J��
        ReturnToMainMenu, // ���C�����j���[�ɖ߂�
        StageSelect,      // �X�e�[�W�I����ʂɖ߂�
        StageStart,       // �X�e�[�W�X�^�[�g
    }
    [SerializeField] private ClickAction clickAction; // �{�^�����������Ƃ��̌���

    // Find�ŒT��GameObject
    private GameObject ScreenController;
    private GameObject Canvas;
    private GameObject UIFunctionController;
    private GameObject Stage;
    private GameObject PlanetInfo;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    private ScreenController screenController;
    private UIController uIController;
    private PauseUIController pauseUIController;
    private CreateStage createStage;

    private bool stageCreated = false; // �X�e�[�W���쐬������

    // �}�E�X�|�C���^�[���{�^���̏�ɏ������
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �{�^���̃A�j���[�V����
        StopAllCoroutines();
        StartCoroutine(ButtonAnimation(defaultColor, OnPointerColor));
    }

    // �}�E�X�|�C���^�[���{�^���̏ォ�痣�ꂽ��
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �{�^���̃A�j���[�V����
        StopAllCoroutines();
        StartCoroutine(ButtonAnimation(OnPointerColor, defaultColor));
    }
    
    // �{�^�����N���b�N���ꂽ��
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // �{�^���̐F�����Z�b�g
        Btn.color = defaultColor;

        // �{�^�����Ƃ̌��ʂɂ���ĕ���
        switch (clickAction)
        {
            case ClickAction.ReturnToGame:     // �Q�[���ɖ߂�
                ReturnToGame();
                break;
            case ClickAction.Setting:          // �ݒ��ʂ��J��
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
            default:
                break;
        }
    }

    // �Q�[���ɖ߂�
    void ReturnToGame()
    {
        // ��ʔԍ���InGame�ɕύX
        screenController.screenNum = 0;

        // �|�[�Y��ʂ�UI���\��
        pauseUIController.DrawPauseUI(false);

        // �f�����UI��\��
        PlanetInfo.SetActive(true);

        // ���Ԃ̗�������ɖ߂�
        Time.timeScale = 1.0f;
    }

    // ���C�����j���[�ɖ߂�
    void ReturnToMainMenu()
    {
        // ��ʔԍ���Title�ɕύX
        screenController.screenNum = 3;
    }

    // �X�e�[�W�I����ʂɑJ��
    void StageSelect()
    {
        // ��ʔԍ���StageSelect�ɕύX
        screenController.screenNum = 4;
    }

    // �X�e�[�W�X�^�[�g
    void StageStart()
    {
        // ��ʔԍ���InGame�ɕύX
        screenController.screenNum = 0;

        // �X�e�[�W�N���A�ς݂Ȃ�X�e�[�W���폜
        if (stageCreated)
            createStage.Delete();

        // �X�e�[�W���쐬
        createStage.Create();
        stageCreated = true;
    }

    // �{�^���̃A�j���[�V����
    IEnumerator ButtonAnimation(Color colorA, Color colorB)
    {
        float time = 0; // �o�ߎ��Ԃ��J�E���g

        // �w�肵�����Ԃ��o�߂���܂ŌJ��Ԃ�
        while (time < fadeTime)
        {
            // ���Ԃ��J�E���g
            time += Time.unscaledDeltaTime;

            // �i�݋���v�Z
            float t = time / fadeTime;

            // �{�^���̐F��ύX
            Btn.color = Color.Lerp(colorA, colorB, t);

            // 1�t���[���҂�
            yield return null;
        }
    }

    void Start()
    {
        // GameObject��T��
        ScreenController = GameObject.Find("ScreenController");
        Canvas = GameObject.Find("Canvas");
        UIFunctionController = GameObject.Find("UIFunctionController");
        Stage = GameObject.Find("Stage");
        PlanetInfo = GameObject.Find("Planet Info");

        // �T����GameObject�̃R���|�[�l���g���擾
        screenController = ScreenController.gameObject.GetComponent<ScreenController>();
        uIController = Canvas.gameObject.GetComponent<UIController>();
        pauseUIController = UIFunctionController.GetComponent<PauseUIController>();
        createStage = Stage.GetComponent<CreateStage>();
    }
}
