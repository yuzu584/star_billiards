using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Const;
using Unity.VisualScripting;

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
        switch (skillCon.skillSlot[skillCon.selectSkill])
        {
            case SkillController.SkillType.SuperCharge: skillName = localize.GetString(StringGroup.SkillName, StringType.SuperCharge); break;
            case SkillController.SkillType.PowerSurge: skillName = localize.GetString(StringGroup.SkillName, StringType.PowerSurge); break;
            case SkillController.SkillType.Huge: skillName = localize.GetString(StringGroup.SkillName, StringType.Huge); break;
            case SkillController.SkillType.GravityWave: skillName = localize.GetString(StringGroup.SkillName, StringType.GravityWave); break;
            case SkillController.SkillType.Frieze: skillName = localize.GetString(StringGroup.SkillName, StringType.Frieze); break;
            case SkillController.SkillType.GrapplingHook: skillName = localize.GetString(StringGroup.SkillName, StringType.GrapplingHook); break;
            case SkillController.SkillType.Slow: skillName = localize.GetString(StringGroup.SkillName, StringType.Slow); break;
            case SkillController.SkillType.InertialControl: skillName = localize.GetString(StringGroup.SkillName, StringType.InertialControl); break;
            case SkillController.SkillType.Blink: skillName = localize.GetString(StringGroup.SkillName, StringType.Blink); break;
            case SkillController.SkillType.TeleportAnchor: skillName = localize.GetString(StringGroup.SkillName, StringType.TeleportAnchor); break;
            default: skillName = "null skill"; break;
        }

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
