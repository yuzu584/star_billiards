using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// ���˃{�^���Œe�𔭎˂���
public class Shot : MonoBehaviour
{
    [SerializeField] private PredictionLine predictionLine;       // Inspector��PredictionLine���w��
    [SerializeField] private EnergyController energyController;   // Inspector��EnergyController���w��
    [SerializeField] private ScreenController screenController;   // Inspector��ScreenController���w��
    public float speed = AppConst.PLAYER_DEFAULT_SPEED;           // �ړ����x
    public float charge = 0;                                      // ���̃`���[�W
    public float chargeSpeed = AppConst.DEFAULT_CHARGE_SPEED;     // ���̃`���[�W���x
    public int playerBouncePower = AppConst.DEFAULT_BOUNCE_POWER; // �Փ˂����Ƃ��̃v���C���[�̔�����
    public int planetBouncePower = AppConst.DEFAULT_BOUNCE_POWER; // �Փ˂����Ƃ��̘f���̔�����

    Vector3 direction;  // ����
    Rigidbody rb;       // �v���C���[��Rigidbody
    Rigidbody cRb;      // �Փ˂����I�u�W�F�N�g��Rigidbody
    RaycastHit hit;     // Ray��hit

    void Start()
    {
        // rigidbody���擾
        rb = GetComponent<Rigidbody>();
    }

    // �Փ˂����Ƃ�
    void OnCollisionEnter(Collision collision)
    {
        // ��u���x��0�ɂ���
        rb.velocity *= 0;

        // �Փ˂����I�u�W�F�N�g�̃^�O��Planet�Ȃ�
        if (collision.gameObject.tag == "Planet")
        {
            // �͂�����������
            rb.AddForce(direction * speed * playerBouncePower / 2);

            // �Փ˂����I�u�W�F�N�g��rigidbody���擾
            cRb = collision.gameObject.GetComponent<Rigidbody>();

            // ����������
            cRb.velocity *= planetBouncePower / 50;
        }
        // �^�O��Planet�ȊO�Ȃ�
        else
        {
            // �͂�������
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

    void FixedUpdate()
    {
        // �G�l���M�[�������ԂŔ��˃{�^���������ꂽ��
        if ((Input.GetAxisRaw("Fire1") > 0) && (energyController.energy > 0))
        {
            // ����������
            rb.velocity *= AppConst.SPEED_REDUCTION_RATE;
        }
    }

    void Update()
    {
        // �Q�[����ʂȂ�
        if (screenController.screenNum == 0)
        {
            // �G�l���M�[�������ԂŔ��˃{�^���������ꂽ��
            if ((Input.GetAxisRaw("Fire1") > 0) && (energyController.energy > 0))
            {
                // �p�x��ݒ�
                direction = predictionLine.RayDirection();

                // �`���[�W�𒙂߂�
                charge += (chargeSpeed * Time.deltaTime) * 50;
            }
            // ���˃{�^����������ĂȂ��Ȃ�
            else if ((Input.GetAxisRaw("Fire1") == 0) && (charge > 0))
            {
                // �G�l���M�[������������
                energyController.energy -= charge / 10;

                // �x�N�g�����J�����̌����ɂ���
                Vector3 velocity = Camera.main.transform.forward;

                // �͂�������
                rb.AddForce(velocity * speed * charge);

                // �`���[�W�����Z�b�g
                charge = 0;
            }
        }

        // �Q�[����ʈȊO���`���[�W���Ȃ�
        else if(charge > 0)
        {
            // �`���[�W�����Z�b�g
            charge = 0;
        }
    }
}
