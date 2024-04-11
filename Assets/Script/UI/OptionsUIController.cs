using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ݒ��ʂ�UI���Ǘ�
public class OptionsUIController : MonoBehaviour
{
    public BackButton buckBtn;
    public GameObject[] lootObj;    // �K�w���Ƃ̃Q�[���I�u�W�F�N�g

    private OptionsController opCon;
    private ScreenController scrCon;

    private void Start()
    {
        scrCon ??= ScreenController.instance;

        opCon.opUICon = this;
        opCon.SetBuckButtonAction();
    }

    private void OnDestroy()
    {
        scrCon.changeLoot -= SwitchLoot;
        opCon.opUICon = null;
    }

    // �\������K�w��؂�ւ�
    private void SwitchLoot()
    {
        scrCon ??= ScreenController.instance;

        // �K�w���Ƃ̃I�u�W�F�N�g�̐��J��Ԃ�
        for (int i = 0; i < lootObj.Length; ++i)
        {
            // �\������K�w�Ȃ�\��
            if ((i == scrCon.ScreenLoot) && (lootObj[i]))
                lootObj[i].SetActive(true);
            // ��\��
            else if (lootObj[i])
                lootObj[i].SetActive(false);
        }
    }

    void OnEnable()
    {
        opCon = OptionsController.instance;

        // �ŏ��� Top ��\��
        SwitchLoot();
    }

    void Update()
    {
        // �K�w���ς�������ʂ�؂�ւ���
        scrCon.changeLoot += SwitchLoot;
    }
}
