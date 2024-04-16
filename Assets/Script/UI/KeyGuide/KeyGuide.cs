using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �L�[����̃K�C�h���Ǘ�
public class KeyGuide : MonoBehaviour
{
    [SerializeField] private Text text;

    private EnumKeyGuide keyGuideType;
    public EnumKeyGuide EnumKeyGuide
    {
        get { return keyGuideType; }
        set
        {
            SetIconAndText();
            keyGuideType = value;
        }
    }

    // �A�C�R���ƃe�L�X�g��ݒ�
    void SetIconAndText()
    {
        var iconSetter = GetComponent<KeyGuideIconSetter>();
        if (iconSetter != null)
        {
            // �A�C�R���ƃe�L�X�g��ݒ�
            iconSetter.SetIcon();
            iconSetter.SetText();
        }
    }
}
