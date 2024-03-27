using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �V���b�g�{�^���Œe�𔭎˂���
public class Shot : Singleton<Shot>
{

    public float speed = AppConst.PLAYER_DEFAULT_SPEED;           // �ړ����x
    public float charge = 0;                                      // ���̃`���[�W
    public float chargeSpeed = AppConst.DEFAULT_CHARGE_SPEED;     // ���̃`���[�W���x
    public int playerBouncePower = AppConst.DEFAULT_BOUNCE_POWER; // �Փ˂����Ƃ��̃v���C���[�̔�����
    public int planetBouncePower = AppConst.DEFAULT_BOUNCE_POWER; // �Փ˂����Ƃ��̘f���̔�����

    private PredictionLine pLine;
    private EnergyController eneCon;
    private ScreenController scrCon;
    private JustShot jShot;
    private InputController input;

    private Vector3 direction;                       // ����
    private Rigidbody rb;                            // �v���C���[��Rigidbody
    private Rigidbody cRb;                           // �Փ˂����I�u�W�F�N�g��Rigidbody
    private RaycastHit hit;                          // Ray��hit
    private int power = 0;                           // �Փˎ��̃p���[
    private Vector3 colObjVelocity;                  // �Փ˂����I�u�W�F�N�g��velocity��ۑ�
    private Coroutine coroutine;                     // �W���X�g�V���b�g�̗P�\���Ԃ��J�E���g����R���[�`��
    private float inputValue = 0;                    // ���͂̐��l
    private float oldInputValue = 0;                 // 1�t���[���O�̓��͂̐��l
    private bool nowInput = false;                   // ���͂��s��ꂽ�u�Ԃ��ǂ���

    // �v���C���[�ƃI�u�W�F�N�g�ɗ͂�������
    void AddPower()
    {
        if (cRb != null)
        {
            // �ړ�����������
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            cRb.constraints = RigidbodyConstraints.None;

            // �v���C���[�ƃI�u�W�F�N�g�ɗ͂�������
            rb.AddForce(direction * speed * playerBouncePower / (2 * power));
            cRb.velocity = colObjVelocity;
            cRb.velocity *= planetBouncePower / (50 / power);
        }
    }

    void Start()
    {
        pLine = PredictionLine.instance;
        eneCon = EnergyController.instance;
        scrCon = ScreenController.instance;
        jShot = JustShot.instance;
        input = InputController.instance;

        // rigidbody���擾
        rb = GetComponent<Rigidbody>();

        input.game_OnShotDele += ShotProcess;
    }

    void Update()
    {
        // �V���b�g���͂����ꂽ�u�ԂȂ�
        if ((inputValue != oldInputValue) && (!nowInput))
        {
            oldInputValue = inputValue;
            nowInput = true;
        }
        // �V���b�g���͂����ꂽ�u�ԂłȂ��Ȃ�
        else if ((inputValue == oldInputValue) && (nowInput))
        {
            nowInput = false;
        }
    }

    void FixedUpdate()
    {
        // �G�l���M�[�������ԂŃV���b�g�{�^���������ꂽ�猸��
        if ((inputValue > 0) && (eneCon.energy > 0))
            rb.velocity *= AppConst.SPEED_REDUCTION_RATE;
    }

    // �Փ˂����Ƃ�
    void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g�̃^�O��Planet�Ȃ�
        if (collision.gameObject.tag == "Planet")
        {
            // �Փ˂����I�u�W�F�N�g��rigidbody���擾
            cRb = collision.gameObject.GetComponent<Rigidbody>();

            // �W���X�g�V���b�g�̗P�\���ԓ��Ȃ狭���͂Ŕ�΂�
            if (jShot.time > 0.0f)
            {
                power = 3;
                StopAllCoroutines();
                StartCoroutine(jShot.UIAnimation());
            }

            // �W���X�g�V���b�g�̗P�\���ԊO�Ȃ�ʏ�̗͂Ŕ�΂�
            else power = 1;

            // �q�b�g�X�g�b�v����(�Փ˂����I�u�W�F�N�g�̑��x�ɉ����ăq�b�g�X�g�b�v���Ԃ��ω�)
            rb.velocity *= 0;
            colObjVelocity = cRb.velocity;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            cRb.constraints = RigidbodyConstraints.FreezePosition;
            Invoke("AddPower", Mathf.Clamp(colObjVelocity.magnitude / (10000 / power), 0.1f, 0.5f));
        }

        // �Փ˂����I�u�W�F�N�g�̃^�O��FixedStar�Ȃ�
        else if (collision.gameObject.tag == "FixedStar")
        {
            // �G�l���M�[��0�ɂ���(�Q�[���I�[�o�[)
            eneCon.energy = 0;
        }

        // �^�O��Planet��FixedStar�ȊO�Ȃ�
        else
        {
            // �v���C���[������������
            rb.velocity *= 0;
            rb.AddForce(direction * speed * playerBouncePower);
        }
    }

    // �Փˌ�
    void OnCollisionExit()
    {
        // Ray�𐶐�
        Ray ray = new Ray(transform.position, rb.velocity.normalized);

        // ���̂�Ray�𐶐�
        if (Physics.SphereCast(ray, transform.localScale.x, out hit))
        {
            // �v���C���[�̈ړ�������Ray�����������ʒu�̖@���x�N�g������p�x���v�Z
            direction = Vector3.Reflect(rb.velocity.normalized, hit.normal);
        }
    }

    // �V���b�g�̏���
    void ShotProcess(float value)
    {
        inputValue = value;

        // �G�l���M�[�������ԂŃV���b�g�{�^����������Ă�����(��������)
        if ((inputValue > 0) && (eneCon.energy > 0))
        {
            // ������ݒ肵�ă`���[�W����
            direction = pLine.RayDirection();
            charge += (chargeSpeed * Time.deltaTime) * 50;
        }
        // �V���b�g�{�^����������ĂȂ����`���[�W�ς݂Ȃ�
        else if ((inputValue == 0) && (charge > 0))
        {
            // �G�l���M�[������Ĕ���
            eneCon.energy -= charge / 10;
            Vector3 velocity = Camera.main.transform.forward;
            rb.AddForce(velocity * speed * charge);
            charge = 0;
        }

        // �G�l���M�[�������ԂŃV���b�g�{�^���������ꂽ��(�������u�Ԃ���)
        if ((inputValue > 0) && (eneCon.energy > 0) && (nowInput))
        {
            // �W���X�g�V���b�g�̗P�\���Ԃ��J�E���g
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(jShot.JustShotCount());
        }
    }
}
