using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;
using UnityEngine.UI;
using System;

// �X�L���I����ʂ�UI���Ǘ�
public class SkillSelectUIController : MonoBehaviour
{
    [SerializeField] private Text skillName, cost, effectTime, coolDown, effectDetails;

    private SkillSelect skillSelect;
    private Localize localize;

    void Start()
    {
        skillSelect = SkillSelect.instance;
        localize = Localize.instance;

        // �t�H���g��ݒ�
        skillName.font = localize.GetFont();
        effectDetails.font = localize.GetFont();

        // �X�L�����UI���X�V����f���Q�[�g�ɓo�^
        skillSelect.DSIdele += DrawSkillInfo;

        // �X�L���̏���`��
        DrawSkillInfo(0);
    }

    private void OnDestroy()
    {
        skillSelect.DSIdele -= DrawSkillInfo;
    }

    // �X�L���̏���`��
    void DrawSkillInfo(SkillController.SkillType type)
    {
        // �X�L�����̃e�L�X�g���X�V
        skillName.text = localize.GetString_SkillName((EnumSkillName)Enum.ToObject(typeof(EnumSkillName), (int)type));

        // �X�L���̌��ʂ̃e�L�X�g���X�V
        effectDetails.text = localize.GetString_SkillDetails((EnumSkillDetails)Enum.ToObject(typeof(EnumSkillDetails), (int)type));

        // �X�L���̃R�X�g�̃e�L�X�g���X�V
        cost.text = AppConst.SKILL_ENERGY_USAGE[(int)type].ToString("0");

        // �X�L���̌��ʎ��Ԃ̃e�L�X�g���X�V
        effectTime.text = AppConst.SKILL_EFFECT_TIME[(int)type].ToString("0") + "s";

        // �X�L���̃N�[���_�E���̃e�L�X�g���X�V
        coolDown.text = AppConst.SKILL_COOLDOWN[(int)type].ToString("0") + "s";
    }
}