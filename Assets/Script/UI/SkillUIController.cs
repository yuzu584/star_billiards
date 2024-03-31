using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;

using Const;

// �X�L����UI���Ǘ�
public class SkillUIController : MonoBehaviour
{
    [SerializeField] private Text skillNameText;
    [SerializeField] private Image skillGauge;

    private Initialize init;
    private SkillController skillCon;
    private Localize localize;

    // �X�L����UI��`��
    void DrawSkillUI()
    {
        string skillName;
        skillName = localize.GetString_SkillName((EnumSkillName)Enum.ToObject(typeof(EnumSkillName),skillCon.selectSkill));

        float effectTime = AppConst.SKILL_EFFECT_TIME[(int)skillCon.skillSlot[skillCon.selectSkill]];
        float coolDown = AppConst.SKILL_COOLDOWN[(int)skillCon.skillSlot[skillCon.selectSkill]];

        // �e�L�X�g�����݂̃X�L�����ɕύX
        skillNameText.text = skillName;

        // ���ʎ��Ԃ�`��
        if (skillCon.effectTime > 0)
            skillGauge.fillAmount = skillCon.effectTime / effectTime;

        // ���ʎ��Ԃ��o�߂��Ă����Ȃ�N�[���_�E����`��
        else if (skillCon.coolDown > 0)
            skillGauge.fillAmount = (coolDown - skillCon.coolDown) / coolDown;

        // �X�L�����g�p�\�����F�ł͂Ȃ��Ȃ物�F�ɂ���
        if((skillCon.coolDown == 0) && (skillCon.effectTime == 0) && (skillGauge.color != AppConst.CAN_USE_SKILL_GAUGE_COLOR))
        {
            skillGauge.color = AppConst.CAN_USE_SKILL_GAUGE_COLOR;
        }

        // �X�L�����g�p�s�����F�ł͂Ȃ��Ȃ甒�F�ɂ���
        else if (((skillCon.coolDown != 0) || (skillCon.effectTime != 0)) && (skillGauge.color != AppConst.DEFAULT_SKILL_GAUGE_COLOR))
        {
            skillGauge.color = AppConst.DEFAULT_SKILL_GAUGE_COLOR;
        }
    }

    // �X�L����UI��������
    void Init()
    {
        skillGauge.fillAmount = 1;
    }

    void Start()
    {
        init = Initialize.instance;
        skillCon = SkillController.instance;
        localize = Localize.instance;

        // �t�H���g��ݒ�
        skillNameText.font = localize.GetFont();

        // �f���Q�[�g�ɏ������֐���o�^
        init.init_Stage += Init;
    }

    private void Update()
    {
        DrawSkillUI();
    }

    private void OnDestroy()
    {
        init.init_Stage -= Init;
    }
}
