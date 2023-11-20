using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

// �X�e�[�W�I����ʂ̃A�C�R�����Ǘ�
public class StageIconController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private StageController stageController; // Inspector��StageController���w��
    [SerializeField] private enum ClickAction                 // �A�C�R���������ꂽ�Ƃ��̋���
    {
        solarSystem,
    }
    [SerializeField] private ClickAction clickAction; // �A�C�R���������ꂽ�Ƃ��̋���

    // �}�E�X�|�C���^�[���A�C�R���̏�ɏ������
    public void OnPointerEnter(PointerEventData pointerEventData)
    {

    }

    // �}�E�X�|�C���^�[���A�C�R���̏ォ�痣�ꂽ��
    public void OnPointerExit(PointerEventData pointerEventData)
    {

    }

    // �A�C�R�����N���b�N���ꂽ��
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // �A�C�R���̎�ނɂ���ĕ���
        switch (clickAction)
        {
            case ClickAction.solarSystem: // ���z�n

                // �X�e�[�W�ԍ��𑾗z�n�ɐݒ�
                stageController.stageNum = 0;
                break;
        }
    }
}
