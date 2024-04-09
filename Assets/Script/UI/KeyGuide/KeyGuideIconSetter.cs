using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �L�[����K�C�h�̉摜���Z�b�g����
public class KeyGuideIconSetter : MonoBehaviour
{
    [SerializeField] private Image Image;
    [SerializeField] private KeyGuideIconData keyGuideIconData;
    [SerializeField] private KeyType keyType;

    private InputController input;

    private void Start()
    {
        input = InputController.instance;

        input.SwitchScheme += SetIcon;

        // �A�C�R����T���Đݒ�
        SetIcon();
    }

    private void OnDestroy()
    {
        input.SwitchScheme -= SetIcon;
    }

    Sprite SearchIcon(KeyType type)
    {
        Sprite sprite;

        for (int i = 0; i < keyGuideIconData.keyTypeAndIcons.Length; i++)
        {
            // Type ����v������
            if (keyGuideIconData.keyTypeAndIcons[i].KeyType == type)
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
                    default:
                        return keyGuideIconData.keyTypeAndIcons[0].KeyIcons.KeybordAndMouse;    // ���݂��Ȃ� Scheme �Ȃ� Z �L�[�̉摜��Ԃ�
                }
            }
        }

        return keyGuideIconData.keyTypeAndIcons[0].KeyIcons.KeybordAndMouse;                    // �Ȃɂ��q�b�g���Ȃ���� Z �L�[�̉摜��Ԃ�
    }

    void SetIcon()
    {
        // �A�C�R����T���Đݒ�
        Image.sprite = SearchIcon(keyType);
    }
}
