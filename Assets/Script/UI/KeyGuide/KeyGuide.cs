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
        ui_positive,
        ui_negative,
        move,
    }

    // �L�[����K�C�hUI�̃e�L�X�g�̎��
    public enum KeyGuideTextType
    {
        decision,
        back,
        move_cursol,
        return_to_previous_screen,
    }

    [System.Serializable]
    public struct KeyGuideIconAndTextType
    {
        public KeyGuideTextType text;
        public KeyGuideIconType icon;
    }

    private KeyGuideTextType textType;
    public KeyGuideTextType TextType
    {
        get { return textType; }
        set
        {
            iconSetter.SetText();
            textType = value;
        }
    }

    private KeyGuideIconType iconType;
    public KeyGuideIconType IconType
    {
        get { return iconType; }
        set
        {
            iconSetter.SetIcon();
            iconType = value;
        }
    }
}
