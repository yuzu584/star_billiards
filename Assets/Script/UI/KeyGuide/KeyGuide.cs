using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// キー操作のガイドを管理
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

    // アイコンとテキストを設定
    void SetIconAndText()
    {
        // アイコンとテキストを設定
        iconSetter.SetIcon();
        iconSetter.SetText();
    }
}
