using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�L�����Ǘ�
public class SkillController : MonoBehaviour
{
    public UIController uIController;

    int selectSkill = 0;  // �I�����Ă���X�L���̔ԍ�
    float coolDown = 0;   // �N�[���_�E�����Ǘ�
    float effectTime = 0; // ���ʎ��Ԃ��Ǘ�

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
        new Skill("SuperCharge", 10, 3, 5),
        new Skill("PowerSurge", 30, 5, 3),
        new Skill("GravityWave", 100, 8, 1),
    };

    void Start()
    {
        // �X�L����UI��`�悷��֐����Ăяo��
    }

    void Update()
    {
        // �}�E�X�z�C�[�����X�N���[������Ă�����
        if ((Input.GetAxisRaw("Mouse ScrollWheel") != 0) && (effectTime <= 0) && (coolDown == 0))
        {
            // �I�����Ă���X�L���ԍ���ς���
            selectSkill += (int)(Input.GetAxisRaw("Mouse ScrollWheel") * 10);

            // �X�L���ԍ����͈͊O�Ȃ�͈͓��Ɏ��߂�
            if (selectSkill < 0)
                selectSkill = 0;
            else if (selectSkill >= skill.Length)
                selectSkill = skill.Length - 1;
        }

        // �X�L���g�p�\�̎��ɃL�[�������ꂽ��
        if ((Input.GetAxisRaw("Skill") != 0) && (effectTime == 0) && (coolDown == 0))
        {
            // ���ʎ��ԂƃN�[���_�E����ݒ�
            effectTime = skill[selectSkill].effectTime;
            coolDown = skill[selectSkill].coolDown;

            EnergyController.energy -= skill[selectSkill].energyUsage;
        }

        // ���ʎ��Ԃ�����������
        if (effectTime > 0)
            effectTime -= Time.deltaTime;
        // ���ʎ��Ԃ��I�����Ă����Ȃ�N�[���_�E��������������
        else if (coolDown > 0)
            coolDown -= Time.deltaTime;

        // ���l��0�����Ȃ�0�ɂ���
        if(effectTime < 0)
            effectTime = 0;
        if (coolDown < 0)
            coolDown = 0;

        // �X�L����UI��`�悷��֐����Ăяo��
        CallSetSkillUI();
    }

    // �X�L����UI��`�悷��֐����Ăяo��
    void CallSetSkillUI()
    {
        uIController.SetSkillUI(
            skill[selectSkill].skillName,
            skill[selectSkill].coolDown,
            skill[selectSkill].effectTime,
            coolDown,
            effectTime);
    }
}
