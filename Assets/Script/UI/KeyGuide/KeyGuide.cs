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
        ui_positive,
        ui_negative,
        move,
    }

    // キー操作ガイドUIのテキストの種類
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
