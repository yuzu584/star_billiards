using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �X�L����UI���Ǘ�
public class SkillUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController; // Inspector��UIController���w��

    // �X�L����UI��`��
    public void DrawSkillUI(string skillName, float coolDown, float effectTime, float nowCoolDown, float nowEffectTime)
    {
        // �e�L�X�g�����݂̃X�L�����ɕύX
        uIController.skillUI.skillName.text = skillName;

        // ���ʎ��Ԃ�`��
        if (nowEffectTime > 0)
            uIController.skillUI.skillGauge.fillAmount = nowEffectTime / effectTime;
        // ���ʎ��Ԃ��o�߂��Ă����Ȃ�N�[���_�E����`��
        else if (nowCoolDown > 0)
            uIController.skillUI.skillGauge.fillAmount = (coolDown - nowCoolDown) / coolDown;
    }
}
