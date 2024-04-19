using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �L�[����̃K�C�h���Ǘ�
public class KeyGuide : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text text;
    [SerializeField] private KeyGuideIconSetter iconSetter;

    // �L�[����K�C�hUI�̃A�C�R���̎��
    public enum KeyGuideIconType
    {
        positive,
        negative,
        move,
    }

    // �L�[����K�C�hUI�̃e�L�X�g�̎��
    public enum KeyGuideTextType
    {
        positive,
        negative,
        move_cursol,
        return_to_previous_screen,
    }

    private KeyGuideTextType type;
    public KeyGuideTextType Type
    {
        get { return type; }
        set
        {
            SetIconAndText();
            type = value;
        }
    }

    // �A�C�R���ƃe�L�X�g��ݒ�
    void SetIconAndText()
    {
        // �A�C�R���ƃe�L�X�g��ݒ�
        iconSetter.SetIcon();
        iconSetter.SetText();
    }
}
