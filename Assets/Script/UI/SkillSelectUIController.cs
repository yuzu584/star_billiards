using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �X�L���I����ʂ�UI���Ǘ�
public class SkillSelectUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController; // Inspector��UIController���w��

    void Start()
    {
        // �X�L���̏���`��
        DrawSkillInfo(0);
    }

    // �X�L���̏���`��
    public void DrawSkillInfo(int skillNum)
    {
        uIController.skillSelectUI.name.text = AppConst.SKILL_NAME[skillNum];
        uIController.skillSelectUI.cost.text = AppConst.SKILL_ENERGY_USAGE[skillNum].ToString("0");
        uIController.skillSelectUI.effectTime.text = AppConst.SKILL_EFFECT_TIME[skillNum].ToString("0") + "s";
        uIController.skillSelectUI.coolDown.text = AppConst.SKILL_COOLDOWN[skillNum].ToString("0") + "s";
        uIController.skillSelectUI.effectDetails.text = AppConst.SKILL_DETAILS[skillNum];
    }
}