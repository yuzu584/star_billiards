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

    // アイコンとテキストを設定
    void SetIconAndText()
    {
        // アイコンとテキストを設定
        iconSetter.SetIcon();
        iconSetter.SetText();
    }
}
