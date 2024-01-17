using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Const;

// ���C�����j���[��UI���Ǘ�
public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject btn;       // �{�^���̃v���n�u
    [SerializeField] private GameObject parentObj; // �{�^���̃v���n�u�̐e�I�u�W�F�N�g

    void Start()
    {
        // �{�^���̃C���X�^���X�𐶐����ď����ݒ���s��
        for (int i = 0; i < AppConst.MAINMENU_BTN_TEXT.Length; i++)
        {
            // �C���X�^���X�𐶐�
            GameObject btnPrefab = Instantiate(btn);

            // ���O��ݒ�
            btnPrefab.name = AppConst.MAINMENU_BTN_TEXT[i];

            // �ʒu��ݒ�
            btnPrefab.transform.position = new Vector3(-300.0f, 0.0f + (i * -20), 0.0f);

            // �e��ݒ�
            btnPrefab.transform.SetParent(parentObj.transform, false);

            // �e�L�X�g���擾
            Text btnText = btnPrefab.transform.GetChild(1).GetComponent<Text>();

            // �e�L�X�g��ݒ�
            btnText.text = AppConst.MAINMENU_BTN_TEXT[i];

            // �{�^�����������Ƃ��̌��ʂ�ݒ�
            ButtonController buttonController = btnPrefab.transform.GetChild(0).GetComponent<ButtonController>();
            switch (AppConst.MAINMENU_BTN_TEXT[i])
            {
                case "StageSelect":
                    buttonController.clickAction = ButtonController.ClickAction.StageSelect;
                    break;
                case "Setting":
                    buttonController.clickAction = ButtonController.ClickAction.Setting;
                    break;
                case "SkillSelect":
                    buttonController.clickAction = ButtonController.ClickAction.SkillSelect;
                    break;
                default:
                    buttonController.clickAction = ButtonController.ClickAction.None;
                    break;
            }
        }
    }

    // ���C�����j���[��\��/��\��
    public void DrawMainMenu(bool draw, GameObject allMainMenuUI)
    {
        // �\��/ ��\���؂�ւ�
        if(allMainMenuUI.activeSelf != draw)
            allMainMenuUI.SetActive(draw);
    }
}
