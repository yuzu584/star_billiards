using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ݒ��ʂ�UI���Ǘ�
public class OptionsUIController : MonoBehaviour
{
    public BackButton backBtn;
    public GameObject[] lootObj;        // �K�w���Ƃ̃v���n�u
    public GameObject lootObjIns;       // �K�w���Ƃ̃C���X�^���X
    public GameObject lootParent;       // �K�w�̐e�I�u�W�F�N�g

    private OptionsController opCon;
    private ScreenController scrCon;

    private void Start()
    {
        scrCon ??= ScreenController.instance;

        opCon.opUICon = this;
        opCon.SetBuckButtonAction(backBtn);

        // �K�w���ς�������ʂ�؂�ւ���
        scrCon.changeLoot += SwitchLoot;
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

        // �C���X�^���X�����ς݂Ȃ�폜
        if (lootObjIns)
        {
            Destroy(lootObjIns);
            lootObjIns = null;
        }

        // �K�w�̃C���X�^���X�𐶐�
        lootObjIns = Instantiate(lootObj[scrCon.ScreenLoot]);
        lootObjIns.transform.SetParent(lootParent.transform, false);
    }

    void OnEnable()
    {
        opCon = OptionsController.instance;
        
        // �ŏ��� Top ��\��
        SwitchLoot();
    }
}
