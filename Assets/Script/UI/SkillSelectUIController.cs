using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;
using UnityEngine.UI;

// �X�L���I����ʂ�UI���Ǘ�
public class SkillSelectUIController : MonoBehaviour
{
    [SerializeField] private Text skillName, cost, effectTime, coolDown, effectDetails;

    private SkillSelect skillSelect;

    void Start()
    {
        skillSelect = SkillSelect.instance;

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
    void DrawSkillInfo(int skillNum)
    {
        skillName.text = AppConst.SKILL_NAME[skillNum];
        cost.text = AppConst.SKILL_ENERGY_USAGE[skillNum].ToString("0");
        effectTime.text = AppConst.SKILL_EFFECT_TIME[skillNum].ToString("0") + "s";
        coolDown.text = AppConst.SKILL_COOLDOWN[skillNum].ToString("0") + "s";
        effectDetails.text = AppConst.SKILL_DETAILS[skillNum];
    }
}