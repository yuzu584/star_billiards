using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// �{�^���̌����ڂ��Ǘ�
public class ButtonGraphic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color color;                       // �|�C���^�[����������̐F
    [SerializeField] private Color defaultColor;                // �f�t�H���g�̐F
    [SerializeField] private Image Btn;                         // �{�^��
    [SerializeField] private ScreenController screenController; // ScreenController�^
    [SerializeField] private enum ClickAction                   // �{�^���������ꂽ�Ƃ��̌���
    {
        ReturnToGame,  // �Q�[���ɖ߂�
        ConfigMenu,    // �ݒ��ʂ��J��
        ReturnToTitle, // �^�C�g����ʂɖ߂�
    }

    [SerializeField] ClickAction clickAction; // �{�^�����������Ƃ��̌���

    // �}�E�X�|�C���^�[���{�^���̏�ɏ������
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �{�^���̐F��ύX
        Btn.color = color;
    }

    // �}�E�X�|�C���^�[���{�^���̏ォ�痣�ꂽ��
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �{�^���̐F�����ɖ߂�
        Btn.color = defaultColor;
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
            case ClickAction.ConfigMenu:    // �ݒ��ʂ��J��
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
        screenController.screenNum = 0;
    }
}
