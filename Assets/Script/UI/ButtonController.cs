using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// �{�^���̌����ڂ��Ǘ�
public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color OnPointerColor;              // �|�C���^�[����������̐F
    [SerializeField] private Color defaultColor;                // �f�t�H���g�̐F
    [SerializeField] private float fadeTime;                    // �t�F�[�h����
    [SerializeField] private Image Btn;                         // �{�^���̉摜
    [SerializeField] private Image BtnOutline;                  // �{�^���̘g�̉摜
    [SerializeField] private Text BtnText;                      // �{�^���̃e�L�X�g
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��
    [SerializeField] private UIController uIController;         // Inspector��UIController���w��
    [SerializeField] private CursorController cursorController; // Inspector��CursorController���w��
    [SerializeField] private GameObject planetInfo;             // �f�����UI
    [SerializeField] private enum ClickAction                   // �{�^���������ꂽ�Ƃ��̌���
    {
        ReturnToGame,  // �Q�[���ɖ߂�
        Setting,       // �ݒ��ʂ��J��
        ReturnToTitle, // �^�C�g����ʂɖ߂�
        StageSelect,   // �X�e�[�W�I����ʂɖ߂�
    }
    [SerializeField] private ClickAction clickAction; // �{�^�����������Ƃ��̌���

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
            case ClickAction.StageSelect: // �X�e�[�W�I����ʂɖ߂�
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
}
