using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�L�����Ǘ�
public class SkillController : MonoBehaviour
{
    // �X�L���̍\����
    public struct Skill
    {
        public string skillName; // �X�L����
        public int energyUsage;  // �G�l���M�[�����
        public float coolDown;   // �Ďg�p�\����
        public float effectTime; // ���ʎ���

        // �\���̂̏������֐�
        public Skill(string sN, int eU, float cD, float eT)
        {
            skillName = sN;
            energyUsage = eU;
            coolDown = cD;
            effectTime = eT;
        }
    }

    // �X�L���̍\���̂̔z���錾
    public Skill[] skill =
    {
        new Skill("SUPER CHARGE", 10, 5, 10),
        new Skill("POWER SURGE", 30, 10, 5),
        new Skill("GRAVITY WAVE", 100, 15, 1),
        new Skill("", -1, -1, -1)
    };
}
