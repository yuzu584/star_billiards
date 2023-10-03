using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    // �J����
    public GameObject camera;

    // �ړ����x
    public float speed = 1.0f;

    // ���̃`���[�W
    public static float charge = 0;

    // ���̃`���[�W���x
    public float chargeSpeed = 10;

    // Ray��Line�����֐��̌^
    public CreateRay createRay;

    public int bouncePower = 100;

    // ����
    Vector3 direction;

    // �v���C���[��Rigidbody
    Rigidbody rb;

    void Start()
    {
        // rigidbody���擾
        rb = GetComponent<Rigidbody>();
    }

    // �Փ˂�����direction�̌����ɗ͂�������
    void OnCollisionEnter(Collision collision)
    {
        // ��u���x��0�ɂ���
        rb.velocity *= 0;

        // �͂�������
        rb.AddForce(direction * speed * bouncePower);
    }

    // �Փˌ�Ɏ��̊p�x��ݒ�
    void OnCollisionExit()
    {
        direction = createRay.RayDirection();
    }

    void Update()
    {
        // �G�l���M�[�������ԂŔ��˃{�^���������ꂽ��
        if(Input.GetAxisRaw("Fire1") > 0 && EnergyController.energy > 0)
        {
            // �p�x��ݒ�
            direction = createRay.RayDirection();

            // �`���[�W�𒙂߂�
            charge += (chargeSpeed * Time.deltaTime) * 50;

            // ����������
            rb.velocity *= 0.996f;
        }
        // ���˃{�^����������ĂȂ��Ȃ�
        else if (Input.GetAxisRaw("Fire1") == 0 && charge > 0)
        {
            // �G�l���M�[������������
            EnergyController.energy -= charge / 10;

            // �x�N�g�����J�����̌����ɂ���
            Vector3 velocity = camera.transform.forward;

            // �͂�������
            rb.AddForce(velocity * speed * charge);

            // �`���[�W�����Z�b�g
            charge = 0;
        }
    }
}
