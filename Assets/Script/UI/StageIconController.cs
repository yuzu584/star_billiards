using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

// �X�e�[�W�I����ʂ̃A�C�R�����Ǘ�
public class StageIconController : MonoBehaviour
{
    [SerializeField] private StageController stageController; // Inspector��StageController���w��
    [SerializeField] private enum ClickAction                 // �A�C�R���������ꂽ�Ƃ��̋���
    {
        solarSystem,
    }
    [SerializeField] private ClickAction clickAction; // �A�C�R���������ꂽ�Ƃ��̋���

    void Start()
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
