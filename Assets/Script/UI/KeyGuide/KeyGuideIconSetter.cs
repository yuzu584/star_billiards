using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static KeyGuide;

// キー操作ガイドの画像をセットする
public class KeyGuideIconSetter : MonoBehaviour
{
    [SerializeField] private KeyGuideIconData keyGuideIconData;
    [SerializeField] private Text text;

    private InputController input;

    private LocalizeText lt;
    private KeyGuide keyGuide;

    private void Start()
    {
        input ??= InputController.instance;
        keyGuide ??= GetComponent<KeyGuide>();

        // LocalizeText の値を設定
        SetText();

        input.SwitchScheme += SetIcon;

        // アイコンを探して設定
        SetIcon();
    }

    private void OnDestroy()
    {
        input ??= InputController.instance;
        input.SwitchScheme -= SetIcon;
    }

    // 指定の画像を探して返す
    Sprite SearchIcon(KeyGuideIconType type)
    {
        Sprite sprite;

        input ??= InputController.instance;

        for (int i = 0; i < keyGuideIconData.keyTypeAndIcons.Length; i++)
        {
            // Type が一致したら
            if (keyGuideIconData.keyTypeAndIcons[i].iconType == type)
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
                    case "Joystick":
                        sprite = keyGuideIconData.keyTypeAndIcons[i].KeyIcons.GamePad;          // 指定の Type のジョイスティック(ゲームパッドと同じ)の画像を返す
                        return sprite;
                    default:
                        return keyGuideIconData.keyTypeAndIcons[0].KeyIcons.KeybordAndMouse;    // 存在しない Scheme なら Z キーの画像を返す
                }
            }
        }

        return keyGuideIconData.keyTypeAndIcons[0].KeyIcons.KeybordAndMouse;                    // なにもヒットしなければ Z キーの画像を返す
    }

    public void SetIcon()
    {
        // アイコンを探して設定
        keyGuide ??= GetComponent<KeyGuide>();

        if (keyGuide.image.Length < 1) return;

        for(int i = 0; i < keyGuide.image.Length; i++)
        {
            keyGuide.image[i].sprite = SearchIcon(keyGuide.IconAndText.icon[i]);
        }
    }

    public void SetText()
    {
        keyGuide ??= GetComponent<KeyGuide>();
        lt ??= text.AddComponent<LocalizeText>();
        lt.text = text;
        lt.seet = "key_guide";
        lt.dataName = keyGuide.IconAndText.text.ToString();
    }
}
