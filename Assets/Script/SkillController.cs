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
    [SerializeField] private ParticleSystem GravityWaveParticle;  // GravityWave�̃p�[�e�B�N��

    public int selectSkill = 0;  // �I�����Ă���X�L���̔ԍ�
    public float coolDown = 0;   // �N�[���_�E�����Ǘ�
    public float effectTime = 0; // ���ʎ��Ԃ��Ǘ�

    private int[] skillSlot = new int[AppConst.SKILL_SLOT_AMOUNT]; // �X�L���X���b�g

    void Start()
    {
        // �X�L���X���b�g��������
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            skillSlot[i] = i;
        }
    }

    void Update()
    {
        // �Q�[����ʂȂ�
        if (screenController.screenNum == 5)
        {
            // �}�E�X�z�C�[�����X�N���[������Ă�����
            if ((Input.GetAxisRaw("Mouse ScrollWheel") != 0) && (effectTime <= 0) && (coolDown == 0))
            {
                // �X�L����ύX
                ChangeSkill(Input.GetAxisRaw("Mouse ScrollWheel"));
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
            AppConst.SKILL_NAME[skillSlot[selectSkill]],
            AppConst.SKILL_COOLDOWN[skillSlot[selectSkill]],
            AppConst.SKILL_EFFECT_TIME[skillSlot[selectSkill]],
            coolDown,
            effectTime
            );
    }

    // �X�L����ύX
    void ChangeSkill(float scroolAmount)
    {
        // �I�����Ă���X�L���ԍ���ς���
        selectSkill += (int)(scroolAmount * 10);

        // �X�L���ԍ����͈͊O�Ȃ�͈͓��Ɏ��߂�
        if (selectSkill < 0)
            selectSkill = 0;
        else if (selectSkill > AppConst.SKILL_SLOT_AMOUNT - 1)
            selectSkill = AppConst.SKILL_SLOT_AMOUNT - 1;
    }

    // �X�L���g�p
    void UseSkill()
    {
        // ���ʎ��ԂƃN�[���_�E����ݒ�
        effectTime = AppConst.SKILL_EFFECT_TIME[skillSlot[selectSkill]];
        coolDown = AppConst.SKILL_COOLDOWN[skillSlot[selectSkill]];

        // �G�l���M�[������
        energyController.energy -= AppConst.SKILL_ENERGY_USAGE[skillSlot[selectSkill]];

        // �I�����Ă���X�L���ɂ���ĕ���
        switch(skillSlot[selectSkill])
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
            case 3: // GravityWave
                UseGravityWave();
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
        shot.chargeSpeed = AppConst.DEFAULT_CHARGE_SPEED + AppConst.CHARGE_SPEED_INCREASE_AMOUNT;

        // ���ʎ��Ԃ��I���܂ő҂�
        yield return new WaitForSeconds(effectTime);

        // �`���[�W�X�s�[�h�����ɖ߂�
        InitSkillEffect();
    }

    // PowerSurge���g�p
    IEnumerator UsePowerSurge()
    {
        // �v���C���[�̎��ʂ𑝉�
        shot.playerBouncePower = AppConst.DEFAULT_BOUNCE_POWER - AppConst.BOUNCE_POWER_INCREASE_AMOUNT;
        shot.planetBouncePower = AppConst.DEFAULT_BOUNCE_POWER + AppConst.BOUNCE_POWER_INCREASE_AMOUNT;

        // ���ʎ��Ԃ��I���܂ő҂�
        yield return new WaitForSeconds(effectTime);

        // �v���C���[�̎��ʂ𑝉������ɖ߂�
        InitSkillEffect();
    }

    // Huge���g�p
    IEnumerator UseHuge()
    {
        // ���剻
        transform.localScale = AppConst.PLAYER_DEFAULT_SCALE * AppConst.SIZE_INCREASE_RATE;

        // ���ʎ��Ԃ��I���܂ő҂�
        yield return new WaitForSeconds(effectTime);

        // ���剻�I��
        InitSkillEffect();

    }

    // GravityWave���g�p
    void UseGravityWave()
    {
        // �p�[�e�B�N���𐶐�->�Đ�
        ParticleSystem newParticle = Instantiate(GravityWaveParticle);
        newParticle.transform.position = this.gameObject.transform.position;
        newParticle.Play();

        // �w�肵�����a�̓����蔻��𐶐�
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            1000.0f,
            Vector3.forward);

        // �����蔻��ɐG�ꂽ�I�u�W�F�N�g�̐��J��Ԃ�
        foreach (var hit in hits)
        {
            // ���������I�u�W�F�N�g��RigidBody���擾
            Rigidbody hitObj = hit.collider.gameObject.GetComponent<Rigidbody>();

            // RigidBody���擾�ł������擾�����I�u�W�F�N�g���f���Ȃ�
            if ((hitObj != null) && (hit.collider.gameObject.tag == "Planet"))
            {
                // �͂�������x�N�g����ݒ�(�Ō��30000���|���ė͂𒲐�)
                Vector3 direction = (this.gameObject.transform.position - hitObj.position) * 30000;

                // �I�u�W�F�N�g�Ƃ̋������߂��قǋ����͂�������
                float distance = Vector3.Distance(this.gameObject.transform.position, hitObj.position);
                hitObj.AddForce(-direction / distance);
            }
        }

        // �p�[�e�B�N�����폜
        Destroy(newParticle.gameObject, 2.0f);
    }

    // ������
    public void Init()
    {
        StopAllCoroutines();
        InitSkillEffect();
        selectSkill = 0;
        coolDown = 0;
        effectTime = 0;
    }

    // �X�L���̌��ʂ���������
    void InitSkillEffect()
    {
        shot.chargeSpeed = AppConst.DEFAULT_CHARGE_SPEED;
        shot.playerBouncePower = AppConst.DEFAULT_BOUNCE_POWER;
        shot.planetBouncePower = AppConst.DEFAULT_BOUNCE_POWER;
        transform.localScale = AppConst.PLAYER_DEFAULT_SCALE;
    }
}
