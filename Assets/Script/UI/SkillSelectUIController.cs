using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �X�L���I����ʂ�UI���Ǘ�
public class SkillSelectUIController : Singleton<SkillSelectUIController>
{
    private UIController uICon;

    void Start()
    {
        uICon = UIController.instance;

        // �X�L���̏���`��
        DrawSkillInfo(0);
    }

    // �X�L���̏���`��
    public void DrawSkillInfo(int skillNum)
    {
        uICon.skillSelectUI.name.text = AppConst.SKILL_NAME[skillNum];
        uICon.skillSelectUI.cost.text = AppConst.SKILL_ENERGY_USAGE[skillNum].ToString("0");
        uICon.skillSelectUI.effectTime.text = AppConst.SKILL_EFFECT_TIME[skillNum].ToString("0") + "s";
        uICon.skillSelectUI.coolDown.text = AppConst.SKILL_COOLDOWN[skillNum].ToString("0") + "s";
        uICon.skillSelectUI.effectDetails.text = AppConst.SKILL_DETAILS[skillNum];
    }
}