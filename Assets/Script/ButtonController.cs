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

    // �}�E�X�|�C���^�[���{�^���̏�ɏ������
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �{�^���̐F��ύX
        StartCoroutine(ButtonAnimation(color));
    }

    // �}�E�X�|�C���^�[���{�^���̏ォ�痣�ꂽ��
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �{�^���̐F�����ɖ߂�
        StartCoroutine(ButtonAnimation(defaultColor));
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
    IEnumerator ButtonAnimation(Color color)
    {
        float time = 0.5f;    // �{�^���̃A�j���[�V��������
        float elapseTime = 0; // �o�ߎ���

        // ���Ԃ��o�߂���܂ŌJ��Ԃ�
        while (elapseTime < time)
        {
            // �o�ߎ��Ԃ��J�E���g
            elapseTime += Time.unscaledDeltaTime;

            // ���Ԃ��o�߂�������(0 �` 1)
            float t = elapseTime / time;

            // �⊮�ŃA�j���[�V����
            Btn.color = Color.Lerp(Btn.color, color, t);

            // 1�t���[���҂�
            yield return null;
        }
    }
}
