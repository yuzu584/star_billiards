using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static KeyGuide;

// �L�[����K�C�h�̉摜���Z�b�g����
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

        // LocalizeText �̒l��ݒ�
        SetText();

        input.SwitchScheme += SetIcon;

        // �A�C�R����T���Đݒ�
        SetIcon();
    }

    private void OnDestroy()
    {
        input ??= InputController.instance;
        input.SwitchScheme -= SetIcon;
    }

    // �w��̉摜��T���ĕԂ�
    Sprite SearchIcon(KeyGuideIconType type)
    {
        Sprite sprite;

        input ??= InputController.instance;

        for (int i = 0; i < keyGuideIconData.keyTypeAndIcons.Length; i++)
        {
            // Type ����v������
            if (keyGuideIconData.keyTypeAndIcons[i].iconType == type)
            {
                // ���݂� Scheme �ɂ���ĕ���
                switch (input.GetNowScheme())
                {
                    case "Keybord&Mouse":
                        sprite = keyGuideIconData.keyTypeAndIcons[i].KeyIcons.KeybordAndMouse;  // �w��� Type �̃L�[�{�[�h&�}�E�X�̉摜��Ԃ�
                        return sprite;
                    case "GamePad":
                        sprite = keyGuideIconData.keyTypeAndIcons[i].KeyIcons.GamePad;          // �w��� Type �̃Q�[���p�b�h�̉摜��Ԃ�
                        return sprite;
                    case "Joystick":
                        sprite = keyGuideIconData.keyTypeAndIcons[i].KeyIcons.GamePad;          // �w��� Type �̃W���C�X�e�B�b�N(�Q�[���p�b�h�Ɠ���)�̉摜��Ԃ�
                        return sprite;
                    default:
                        return keyGuideIconData.keyTypeAndIcons[0].KeyIcons.KeybordAndMouse;    // ���݂��Ȃ� Scheme �Ȃ� Z �L�[�̉摜��Ԃ�
                }
            }
        }

        return keyGuideIconData.keyTypeAndIcons[0].KeyIcons.KeybordAndMouse;                    // �Ȃɂ��q�b�g���Ȃ���� Z �L�[�̉摜��Ԃ�
    }

    public void SetIcon()
    {
        // �A�C�R����T���Đݒ�
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
