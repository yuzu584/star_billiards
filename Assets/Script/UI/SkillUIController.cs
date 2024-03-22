using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Const;

// �X�L����UI���Ǘ�
public class SkillUIController : Singleton<SkillUIController>
{
    private UIController uICon;
    private Initialize init;

    // �X�L����UI��`��
    public void DrawSkillUI(string skillName, float coolDown, float effectTime, float nowCoolDown, float nowEffectTime)
    {
        // �e�L�X�g�����݂̃X�L�����ɕύX
        uICon.skillUI.skillName.text = skillName;

        // ���ʎ��Ԃ�`��
        if (nowEffectTime > 0)
            uICon.skillUI.skillGauge.fillAmount = nowEffectTime / effectTime;

        // ���ʎ��Ԃ��o�߂��Ă����Ȃ�N�[���_�E����`��
        else if (nowCoolDown > 0)
            uICon.skillUI.skillGauge.fillAmount = (coolDown - nowCoolDown) / coolDown;

        // �X�L�����g�p�\�����F�ł͂Ȃ��Ȃ物�F�ɂ���
        if((nowCoolDown == 0) && (nowEffectTime == 0) && (uICon.skillUI.skillGauge.color != AppConst.CAN_USE_SKILL_GAUGE_COLOR))
        {
            uICon.skillUI.skillGauge.color = AppConst.CAN_USE_SKILL_GAUGE_COLOR;
        }

        // �X�L�����g�p�s�����F�ł͂Ȃ��Ȃ甒�F�ɂ���
        else if (((nowCoolDown != 0) || (nowEffectTime != 0)) && (uICon.skillUI.skillGauge.color != AppConst.DEFAULT_SKILL_GAUGE_COLOR))
        {
            uICon.skillUI.skillGauge.color = AppConst.DEFAULT_SKILL_GAUGE_COLOR;
        }
    }

    // �X�L����UI��������
    void Init()
    {
        uICon.skillUI.skillGauge.fillAmount = 1;
    }

    void Start()
    {
        uICon = UIController.instance;
        init = Initialize.instance;

        // �f���Q�[�g�ɏ������֐���o�^
        init.init_Stage += Init;
    }
}
