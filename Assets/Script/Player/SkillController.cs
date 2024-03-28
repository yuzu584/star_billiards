using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �X�L�����Ǘ�
public class SkillController : Singleton<SkillController>
{
    [SerializeField] private Transform playerT;                     // �v���C���[�̍��W
    [SerializeField] private ParticleSystem GravityWaveParticle;    // GravityWave�̃p�[�e�B�N��

    private Shot shot;
    private EnergyController eneCon;
    private ScreenController screenCon;
    private Initialize init;
    private SphereRay sphereRay;
    private InputController input;

    public int selectSkill = 0;  // �I�����Ă���X�L���̔ԍ�
    public float coolDown = 0;   // �N�[���_�E�����Ǘ�
    public float effectTime = 0; // ���ʎ��Ԃ��Ǘ�

    public enum SkillType
    {
        SuperCharge,
        PowerSurge,
        Huge,
        GravityWave,
        Frieze,
        GrapplingHook,
        Slow,
        InertialControl,
        Blink,
        TeleportAnchor,
        None,
    }

    public SkillType[] skillSlot = new SkillType[AppConst.SKILL_SLOT_AMOUNT];  // �X�L���X���b�g

    void Start()
    {
        shot = Shot.instance;
        eneCon = EnergyController.instance;
        screenCon = ScreenController.instance;
        init = Initialize.instance;
        sphereRay = SphereRay.instance;
        input = InputController.instance;

        // �X�L���X���b�g��������
        skillSlot[0] = SkillType.SuperCharge;
        skillSlot[1] = SkillType.PowerSurge;
        skillSlot[2] = SkillType.Huge;

        // �f���Q�[�g�ɏ������֐���o�^
        init.init_Stage += Init;

        input.game_OnUseSkillDele += UseSkill;
        input.game_OnChangeSkillDele += ChangeSkill;
    }

    void Update()
    {
        // �Q�[����ʂȂ�
        if (screenCon.Screen == ScreenController.ScreenType.InGame)
        {
            // �X�L���̌��ʎ��ԂƃN�[���_�E��������
            DecreaseEFAndCD();
        }
    }

    // �X�L���g�p
    void UseSkill(float value)
    {
        // �X�L���g�p�\�̎��ɃL�[�������ꂽ��
        if ((value != 0) && (effectTime == 0) && (coolDown == 0))
        {
            // ���ʎ��ԂƃN�[���_�E����ݒ�
            effectTime = AppConst.SKILL_EFFECT_TIME[(int)skillSlot[selectSkill]];
            coolDown = AppConst.SKILL_COOLDOWN[(int)skillSlot[selectSkill]];

            // �G�l���M�[������
            eneCon.energy -= AppConst.SKILL_ENERGY_USAGE[(int)skillSlot[selectSkill]];

            // �I�����Ă���X�L���ɂ���ĕ���
            switch (skillSlot[selectSkill])
            {
                case SkillType.SuperCharge: StartCoroutine(UseSuperCharge());   break;
                case SkillType.PowerSurge:  StartCoroutine(UsePowerSurge());    break;
                case SkillType.Huge:        StartCoroutine(UseHuge());          break;
                case SkillType.GravityWave: UseGravityWave();                   break;
                case SkillType.Frieze:      UseFrieze();                        break;
            }
        }
    }

    // �X�L����ύX
    void ChangeSkill(float value)
    {
        // �X�L���ύX�{�^����������Ă�����
        if ((value != 0) && (effectTime <= 0) && (coolDown == 0))
        {
            // �I�����Ă���X�L���ԍ���ς���
            selectSkill += (int)value;

            // �X�L���ԍ����͈͊O�Ȃ�͈͓��Ɏ��߂�
            if (selectSkill < 0)
                selectSkill = 0;
            else if (selectSkill > AppConst.SKILL_SLOT_AMOUNT - 1)
                selectSkill = AppConst.SKILL_SLOT_AMOUNT - 1;
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
        newParticle.transform.position = playerT.position;
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
                Vector3 direction = (playerT.position - hitObj.position) * 30000;

                // �I�u�W�F�N�g�Ƃ̋������߂��قǋ����͂�������
                float distance = Vector3.Distance(playerT.position, hitObj.position);
                hitObj.AddForce(-direction / distance);
            }
        }

        // �p�[�e�B�N�����폜
        Destroy(newParticle.gameObject, 2.0f);
    }

    // Frieze���g�p
    void UseFrieze()
    {
        // Ray�ɓ��������I�u�W�F�N�g��RigidBody���擾
        Rigidbody hitRb = sphereRay.hit.collider.gameObject.GetComponent<Rigidbody>();

        // �Ώۂ��f���Ȃ瓮�����~�߂�
        if (sphereRay.hitObjectTag == "Planet")
            hitRb.velocity *= 0;
    }

    // ������
    void Init()
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
