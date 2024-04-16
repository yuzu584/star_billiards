using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// �L�[����K�C�h�̉摜���Z�b�g����
public class KeyGuideIconSetter : MonoBehaviour
{
    [SerializeField] private Image Image;
    [SerializeField] private KeyGuideIconData keyGuideIconData;
    [SerializeField] private EnumKeyGuide keyGuideType;
    [SerializeField] private Text text;

    private InputController input;

    private LocalizeText lt;

    private void Start()
    {
        input = InputController.instance;

        // LocalizeText �̒l��ݒ�
        SetText();

        input.SwitchScheme += SetIcon;

        // �A�C�R����T���Đݒ�
        SetIcon();
    }

    private void OnDestroy()
    {
        input.SwitchScheme -= SetIcon;
    }

    // �w��̉摜��T���ĕԂ�
    Sprite SearchIcon(EnumKeyGuide type)
    {
        Sprite sprite;

        for (int i = 0; i < keyGuideIconData.keyTypeAndIcons.Length; i++)
        {
            // Type ����v������
            if (keyGuideIconData.keyTypeAndIcons[i].keyGuideType == type)
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
        Image.sprite = SearchIcon(keyGuideType);
    }

    public void SetText()
    {
        lt ??= text.AddComponent<LocalizeText>();
        lt.text = text;
        lt.group = StringGroup.KeyGuide;
        lt.type.keyGuide = keyGuideType;
    }
}
