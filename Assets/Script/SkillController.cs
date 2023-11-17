using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�L�����Ǘ�
public class SkillController : MonoBehaviour
{
    [SerializeField] private Shot shot;                           // Inspector��Shot���w��
    [SerializeField] private SkillUIController skillUIController; // Inspector��SkillUIController���w��
    [SerializeField] private EnergyController energyController;   // Inspector��EnergyController���w��
    [SerializeField] private ScreenController screenController;   // Inspector��ScreenController���w��

    int selectSkill = 0;  // �I�����Ă���X�L���̔ԍ�
    float coolDown = 0;   // �N�[���_�E�����Ǘ�
    float effectTime = 0; // ���ʎ��Ԃ��Ǘ�

    // �X�L���̍\����
    public struct Skill
    {
        public string skillName; // �X�L����
        public int energyUsage;  // �G�l���M�[�����
        public float coolDown;   // �N�[���_�E��
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
        new Skill("SuperCharge", 10, 1, 10),
        new Skill("PowerSurge", 30, 1, 10),
        new Skill("Huge", 100, 1, 10),
    };

    void Start()
    {
        skillUIController.DrawSkillUI(
            skill[selectSkill].skillName,
            skill[selectSkill].coolDown,
            skill[selectSkill].effectTime,
            coolDown,
            effectTime
            );
    }

    void Update()
    {
        // �Q�[����ʂȂ�
        if (screenController.screenNum == 0)
        {
            // �}�E�X�z�C�[�����X�N���[������Ă�����
            if ((Input.GetAxisRaw("Mouse ScrollWheel") != 0) && (effectTime <= 0) && (coolDown == 0))
            {
                // �X�L����ύX
                ChangeSkill();
            }

            // �X�L���g�p�\�̎��ɃL�[�������ꂽ��
            if ((Input.GetAxisRaw("Skill") != 0) && (effectTime == 0) && (coolDown == 0))
            {
                // �X�L���g�p
                UseSkill();
            }

            // �X�L���̌��ʎ��ԂƃN�[���_�E��������
            DecreaseEFAndCD();

            // �X�L����UI��`�悷��֐����Ăяo��
            CallSetSkillUI();
        }
    }

    // �X�L����UI��`�悷��֐����Ăяo��
    public void CallSetSkillUI()
    {
        skillUIController.DrawSkillUI(
            skill[selectSkill].skillName,
            skill[selectSkill].coolDown,
            skill[selectSkill].effectTime,
            coolDown,
            effectTime
            );
    }

    // �X�L����ύX
    void ChangeSkill()
    {
        // �I�����Ă���X�L���ԍ���ς���
        selectSkill += (int)(Input.GetAxisRaw("Mouse ScrollWheel") * 10);

        // �X�L���ԍ����͈͊O�Ȃ�͈͓��Ɏ��߂�
        if (selectSkill < 0)
            selectSkill = 0;
        else if (selectSkill >= skill.Length)
            selectSkill = skill.Length - 1;
    }

    // �X�L���g�p
    void UseSkill()
    {
        // ���ʎ��ԂƃN�[���_�E����ݒ�
        effectTime = skill[selectSkill].effectTime;
        coolDown = skill[selectSkill].coolDown;

        // �G�l���M�[������
        energyController.energy -= skill[selectSkill].energyUsage;

        // �I�����Ă���X�L���ɂ���ĕ���
        switch(selectSkill)
        {
            case 0: // SuperCharge
                StartCoroutine(UseSuperCharge());
                break;
            case 1: // PowerSurge
                StartCoroutine(UsePowerSurge());
                break;
            case 2: // Huge
                StartCoroutine(UseHuge());
                break;
        }
    }

    // �X�L���̌��ʎ��ԂƃN�[���_�E��������
    void DecreaseEFAndCD()
    {
        // ���ʎ��Ԃ�����������
        if (effectTime > 0)
            effectTime -= Time.deltaTime;
        // ���ʎ��Ԃ��I�����Ă����Ȃ�N�[���_�E��������������
        else if (coolDown > 0)
            coolDown -= Time.deltaTime;

        // ���l��0�����Ȃ�0�ɂ���
        if (effectTime < 0)
            effectTime = 0;
        if (coolDown < 0)
            coolDown = 0;
    }

    // SuperCharge���g�p
    IEnumerator UseSuperCharge()
    {
        // �`���[�W�X�s�[�h�𑝉�
        shot.chargeSpeed += 1;

        // ���ʎ��Ԃ��I���܂ő҂�
        yield return new WaitForSeconds(effectTime);

        // �`���[�W�X�s�[�h�����ɖ߂�
        shot.chargeSpeed -= 1;
    }

    // PowerSurge���g�p
    IEnumerator UsePowerSurge()
    {
        // �v���C���[�̎��ʂ𑝉�
        shot.playerBouncePower -= 100;
        shot.planetBouncePower += 100;

        // ���ʎ��Ԃ��I���܂ő҂�
        yield return new WaitForSeconds(effectTime);

        // �v���C���[�̎��ʂ𑝉������ɖ߂�
        shot.playerBouncePower += 100;
        shot.planetBouncePower -= 100;
    }

    // Huge���g�p
    IEnumerator UseHuge()
    {
        // ���剻
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // ���ʎ��Ԃ��I���܂ő҂�
        yield return new WaitForSeconds(effectTime);

        // ���剻�I��
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    }
}
