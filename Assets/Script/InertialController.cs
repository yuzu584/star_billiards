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
        // �O���͂Ȃ猸�����ɂ₩��
        if (z > 0)
        {
            rb.velocity *= 1.008f;
        }
        // �����͂Ȃ猸��
        else if (z < 0)
        {
            rb.velocity *= 0.95f;
        }
        // �O����͂���Ă��Ȃ���Ԃō��E���͂Ȃ�O�������E�ɋȂ���
        else if (x != 0)
        {
            rb.AddForce(Camera.main.transform.right * rb.velocity.magnitude / 10 * x * EaseOfBending);
        }

        // ���x�����̒l�ȉ��Ȃ�0�ɂ���
        if (rb.velocity.magnitude < 0.01f)
        {
            rb.velocity *= 0;
        }
    }
}