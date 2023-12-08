using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �X�L�����Ǘ�
public class SkillController : MonoBehaviour
{
    [SerializeField] private Shot shot;                           // Inspector��Shot���w��
    [SerializeField] private SkillUIController skillUIController; // Inspector��SkillUIController���w��
    [SerializeField] private EnergyController energyController;   // Inspector��EnergyController���w��
    [SerializeField] private ScreenController screenController;   // Inspector��ScreenController���w��

    public int selectSkill = 0;  // �I�����Ă���X�L���̔ԍ�
    public float coolDown = 0;   // �N�[���_�E�����Ǘ�
    public float effectTime = 0; // ���ʎ��Ԃ��Ǘ�

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
            AppConst.SKILL_NAME[selectSkill],
            AppConst.SKILL_COOLDOWN[selectSkill],
            AppConst.SKILL_EFFECT_TIME[selectSkill],
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
        else if (selectSkill >= AppConst.SKILL_NUM)
            selectSkill = AppConst.SKILL_NUM - 1;
    }

    // �X�L���g�p
    void UseSkill()
    {
        // ���ʎ��ԂƃN�[���_�E����ݒ�
        effectTime = AppConst.SKILL_EFFECT_TIME[selectSkill];
        coolDown = AppConst.SKILL_COOLDOWN[selectSkill];

        // �G�l���M�[������
        energyController.energy -= AppConst.SKILL_ENERGY_USAGE[selectSkill];

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
        shot.chargeSpeed += AppConst.CHARGE_SPEED_INCREASE_AMOUNT;

        // ���ʎ��Ԃ��I���܂ő҂�
        yield return new WaitForSeconds(effectTime);

        // �`���[�W�X�s�[�h�����ɖ߂�
        shot.chargeSpeed -= AppConst.CHARGE_SPEED_INCREASE_AMOUNT;
    }

    // PowerSurge���g�p
    IEnumerator UsePowerSurge()
    {
        // �v���C���[�̎��ʂ𑝉�
        shot.playerBouncePower -= AppConst.MASS_INCREASE_AMOUNT;
        shot.planetBouncePower += AppConst.MASS_INCREASE_AMOUNT;

        // ���ʎ��Ԃ��I���܂ő҂�
        yield return new WaitForSeconds(effectTime);

        // �v���C���[�̎��ʂ𑝉������ɖ߂�
        shot.playerBouncePower += AppConst.MASS_INCREASE_AMOUNT;
        shot.planetBouncePower -= AppConst.MASS_INCREASE_AMOUNT;
    }

    // Huge���g�p
    IEnumerator UseHuge()
    {
        // ���剻
        transform.localScale *= AppConst.SIZE_INCREASE_RATE;

        // ���ʎ��Ԃ��I���܂ő҂�
        yield return new WaitForSeconds(effectTime);

        // ���剻�I��
        transform.localScale /= AppConst.SIZE_INCREASE_RATE;

    }
}
