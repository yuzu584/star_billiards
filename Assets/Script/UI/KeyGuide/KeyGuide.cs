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

    public enum KeyGuideType
    {
        positive,
        negative,
        move_cursol,
    }

    private KeyGuideType type;
    public KeyGuideType Type
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
