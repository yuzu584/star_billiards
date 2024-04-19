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

    // キー操作ガイドUIのアイコンの種類
    public enum KeyGuideIconType
    {
        positive,
        negative,
        move,
    }

    // キー操作ガイドUIのテキストの種類
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

    // アイコンとテキストを設定
    void SetIconAndText()
    {
        // アイコンとテキストを設定
        iconSetter.SetIcon();
        iconSetter.SetText();
    }
}
