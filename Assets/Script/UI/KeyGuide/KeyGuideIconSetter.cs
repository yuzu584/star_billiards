using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// キー操作ガイドの画像をセットする
public class KeyGuideIconSetter : MonoBehaviour
{
    [SerializeField] private Image Image;
    [SerializeField] private KeyGuideIconData keyGuideIconData;
    [SerializeField] private EnumKeyGuide keyGuideType;
    [SerializeField] private Text text;

    private InputController input;

    private void Start()
    {
        input = InputController.instance;

        // LocalizeText の値を設定
        LocalizeText lt = text.AddComponent<LocalizeText>();
        lt.text = text;
        lt.group = StringGroup.KeyGuide;
        lt.type.keyGuide = keyGuideType;

        input.SwitchScheme += SetIcon;

        // アイコンを探して設定
        SetIcon();
    }

    private void OnDestroy()
    {
        input.SwitchScheme -= SetIcon;
    }

    Sprite SearchIcon(EnumKeyGuide type)
    {
        Sprite sprite;

        for (int i = 0; i < keyGuideIconData.keyTypeAndIcons.Length; i++)
        {
            // Type が一致したら
            if (keyGuideIconData.keyTypeAndIcons[i].keyGuideType == type)
            {
                // 現在の Scheme によって分岐
                switch (input.GetNowScheme())
                {
                    case "Keybord&Mouse":
                        sprite = keyGuideIconData.keyTypeAndIcons[i].KeyIcons.KeybordAndMouse;  // 指定の Type のキーボード&マウスの画像を返す
                        return sprite;
                    case "GamePad":
                        sprite = keyGuideIconData.keyTypeAndIcons[i].KeyIcons.GamePad;          // 指定の Type のゲームパッドの画像を返す
                        return sprite;
                    default:
                        return keyGuideIconData.keyTypeAndIcons[0].KeyIcons.KeybordAndMouse;    // 存在しない Scheme なら Z キーの画像を返す
                }
            }
        }

        return keyGuideIconData.keyTypeAndIcons[0].KeyIcons.KeybordAndMouse;                    // なにもヒットしなければ Z キーの画像を返す
    }

    void SetIcon()
    {
        // アイコンを探して設定
        Image.sprite = SearchIcon(keyGuideType);
    }
}
