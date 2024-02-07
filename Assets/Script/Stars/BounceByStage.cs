using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W�ƏՓ˂����Ƃ���������
public class BounceByStage : MonoBehaviour
{
    public int bouncePower = 100;  // �Փ˂����Ƃ��̔�����

    Rigidbody rb;  // ������Rigidbody

    void Start()
    {
        // rigidbody���擾
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // �����͂�������
        rb.AddForce(collision.contacts[0].normal * bouncePower * 100);
    }
}
