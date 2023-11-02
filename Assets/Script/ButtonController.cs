using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// �{�^���̌����ڂ��Ǘ�
public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color color;                       // �|�C���^�[����������̐F
    [SerializeField] private Color defaultColor;                // �f�t�H���g�̐F
    [SerializeField] private Image Btn;                         // �{�^���̉摜
    [SerializeField] private Image BtnOutline;                  // �{�^���̘g�̉摜
    [SerializeField] private Text BtnText;                      // �{�^���̃e�L�X�g
    [SerializeField] private ScreenController screenController; // ScreenController�^
    [SerializeField] private UIController uIController;         // UIController�^�̕ϐ�
    [SerializeField] private CursorController cursorController; // CursorController�^�̕ϐ�
    [SerializeField] private GameObject planetInfo;             // �f�����UI
    [SerializeField] private enum ClickAction                   // �{�^���������ꂽ�Ƃ��̌���
    {
        ReturnToGame,  // �Q�[���ɖ߂�
        Setting,       // �ݒ��ʂ��J��
        ReturnToTitle, // �^�C�g����ʂɖ߂�
    }
    [SerializeField] private ClickAction clickAction; // �{�^�����������Ƃ��̌���

    private Vector3[] defaultPos = new Vector3[3]; // �{�^���̈ʒu
    private Vector3[] startPos = new Vector3[3];   // �{�^���̎n�_�ʒu
    private Vector3[] endPos = new Vector3[3];     // �{�^���̏I�_�ʒu
    private float buttonAnimationTime = 0.2f;      // �{�^���̃A�j���[�V��������
    Vector3 moveLength = new Vector3(5, 5, 0);     // �A�j���[�V�������̈ړ�����

    void Start()
    {
        // �{�^���̈ʒu��ۑ�
        defaultPos[0] = Btn.rectTransform.position;
        defaultPos[1] = BtnOutline.rectTransform.position;
        defaultPos[2] = BtnText.rectTransform.position;
    }

    // �}�E�X�|�C���^�[���{�^���̏�ɏ������
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �{�^���̐F��ύX
        Btn.color = color;

        // �{�^�����A�j���[�V����
        endPos[0] = defaultPos[0] + moveLength;
        endPos[1] = defaultPos[1] + moveLength;
        endPos[2] = defaultPos[2] + moveLength;
        StartCoroutine(ButtonAnimation(buttonAnimationTime, endPos));
    }

    // �}�E�X�|�C���^�[���{�^���̏ォ�痣�ꂽ��
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �{�^���̐F�����ɖ߂�
        Btn.color = defaultColor;

        // �{�^�����A�j���[�V����
        endPos[0] = defaultPos[0];
        endPos[1] = defaultPos[1];
        endPos[2] = defaultPos[2];
        StartCoroutine(ButtonAnimation(buttonAnimationTime, endPos));
    }
    
    // �{�^�����N���b�N���ꂽ��
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // �{�^�����Ƃ̌��ʂɂ���ĕ���
        switch (clickAction)
        {
            case ClickAction.ReturnToGame:  // �Q�[���ɖ߂�
                ReturnToGame();
                break;
            case ClickAction.Setting:    // �ݒ��ʂ��J��
                break;
            case ClickAction.ReturnToTitle: // �^�C�g����ʂɖ߂�
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
        uIController.DrawPauseUI(false);

        // �}�E�X�J�[�\�����\��
        cursorController.DrawCursol(false);

        // �f�����UI��\��
        planetInfo.SetActive(true);

        // ���Ԃ̗�������ɖ߂�
        Time.timeScale = 1.0f;
    }

    // �{�^���̃A�j���[�V����
    IEnumerator ButtonAnimation(float time, Vector3[] endPos)
    {
        Vector3[] startPos = new Vector3[3];       // �J�n�ʒu
        float elapseTime = 0;                      // �o�ߎ���

        // �J�n�ʒu��ݒ�
        startPos[0] = Btn.rectTransform.position;
        startPos[1] = BtnOutline.rectTransform.position;
        startPos[2] = BtnText.rectTransform.position;

        // ���Ԃ��o�߂���܂ŌJ��Ԃ�
        while (elapseTime < time)
        {
            // �o�ߎ��Ԃ��J�E���g
            elapseTime += Time.unscaledDeltaTime;

            // ���Ԃ��o�߂�������(0 �` 1)
            float t = elapseTime / time;

            // �⊮�ŃA�j���[�V����
            Btn.rectTransform.position = Vector3.Lerp(startPos[0], endPos[0], t);
            BtnOutline.rectTransform.position = Vector3.Lerp(startPos[1], endPos[1], t);
            BtnText.rectTransform.position = Vector3.Lerp(startPos[2], endPos[2], t);

            // 1�t���[���҂�
            yield return null;
        }
    }
}
