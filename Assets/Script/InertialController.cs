using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �v���C���[�̊����𐧌�
public class InertialController : MonoBehaviour
{
    public float EaseOfBending = 1.0f; // �O���̋Ȃ��₷��
    Rigidbody rb;                      // �v���C���[��Rigidbody
    float x = 0;                       // ���E�ړ���
    float z = 0;                       // �O��ړ���
    Vector3 vector;                    // �ړ�����

    void Start()
    {
        // rigidbody���擾
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ���E�ړ��ʂ���
        x = Input.GetAxisRaw("Horizontal");

        // �O��ړ��ʂ���
        z = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // �O���͂Ȃ����
        if (z > 0)
        {
            rb.velocity *= 1.05f;
        }
        // �����͂Ȃ猸��
        else if (z < 0)
        {
            rb.velocity *= 0.95f;
        }
    }
}
