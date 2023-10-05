using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���˃{�^���Œe�𔭎˂���
public class Shot : MonoBehaviour
{
    [SerializeField] CreateRay Cr;  // Ray��Line�����֐��̌^
    public GameObject camera;       // �J����
    public float speed = 1.0f;      // �ړ����x
    public static float charge = 0; // ���̃`���[�W
    public float chargeSpeed = 10;  // ���̃`���[�W���x
    public int bouncePower = 100;   // �Փ˂����Ƃ��̔�����

    Vector3 direction;              // ����
    Rigidbody rb;                   // �v���C���[��Rigidbody
    RaycastHit hit;                 // Ray��hit

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
        // Ray�𐶐�
        Ray ray = new Ray(transform.position, rb.velocity.normalized);

        // ���̂�Ray�𐶐�
        if (Physics.SphereCast(ray, 0.5f, out hit))
        {
            // �v���C���[�̈ړ�������Ray�����������ʒu�̖@���x�N�g������p�x���v�Z
            direction = Vector3.Reflect(rb.velocity.normalized, hit.normal);
        }
    }

    void FixedUpdate()
    {
        // �G�l���M�[�������ԂŔ��˃{�^���������ꂽ��
        if (Input.GetAxisRaw("Fire1") > 0 && EnergyController.energy > 0)
        {
            // ����������
            rb.velocity *= 0.995f;
        }
    }

    void Update()
    {
        // �G�l���M�[�������ԂŔ��˃{�^���������ꂽ��
        if(Input.GetAxisRaw("Fire1") > 0 && EnergyController.energy > 0)
        {
            // �p�x��ݒ�
            direction = Cr.RayDirection();

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
