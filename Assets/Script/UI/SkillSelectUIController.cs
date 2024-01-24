using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �X�L���I����ʂ�UI���Ǘ�
public class SkillSelectUIController : MonoBehaviour
{
    [SerializeField] private GameObject skillSlotObj; // �X�L���X���b�g�̃v���n�u
    [SerializeField] private GameObject parentObj;    // �X�L���X���b�g�̃v���n�u�̐e�I�u�W�F�N�g

    void Start()
    {
        // �X�L���I����ʂ̃X�L���X���b�g���쐬
        CreateSkillSlot();
    }

    // �X�L���I����ʂ̃X�L���X���b�g���쐬
    void CreateSkillSlot()
    {
        // �X�L���̐��J��Ԃ�
        for (int i = 0; i < AppConst.SKILL_NUM; i++)
        {
            // �C���X�^���X�𐶐�
            GameObject slotPrefab = Instantiate(skillSlotObj);

            // ���O��ݒ�
            slotPrefab.name = AppConst.SKILL_NAME[i];

            // �ʒu��ݒ�
            slotPrefab.transform.position = new Vector3(-100.0f + (i * 60), 100.0f - ((i / 5) * 60), 0.0f);

            // �e��ݒ�
            slotPrefab.transform.SetParent(parentObj.transform, false);

            // �X�L���ԍ���ݒ�
            SkillSlotController skillSlotController = slotPrefab.GetComponent<SkillSlotController>();
            skillSlotController.skillNum = i;
        }
    }
}