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

    private string keyGuideType;
    public string KeyGuideType
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
        // �A�C�R���ƃe�L�X�g��ݒ�
        iconSetter.SetIcon();
        iconSetter.SetText();
    }
}
