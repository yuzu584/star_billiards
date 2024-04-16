using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// キー操作のガイドを管理
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

    // アイコンとテキストを設定
    void SetIconAndText()
    {
        var iconSetter = GetComponent<KeyGuideIconSetter>();
        if (iconSetter != null)
        {
            // アイコンとテキストを設定
            iconSetter.SetIcon();
            iconSetter.SetText();
        }
    }
}
