using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// �{�^���̌����ڂ��Ǘ�
public class ButtonGraphic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color color;        // �|�C���^�[����������̐F
    [SerializeField] private Color defaultColor; // �f�t�H���g�̐F
    [SerializeField] private Image Btn;          // �{�^��

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
}
