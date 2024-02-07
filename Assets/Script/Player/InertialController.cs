using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

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
            rb.velocity *= AppConst.SPEED_MAINTENANCE_RATE;
        }
        // �����͂Ȃ猸��
        else if (z < 0)
        {
            rb.velocity *= AppConst.SPEED_REDUCTION_RATE;
        }
        // �O����͂���Ă��Ȃ���Ԃō��E���͂Ȃ�O�������E�ɋȂ���
        else if (x != 0)
        {
            rb.AddForce(Camera.main.transform.right * (rb.velocity.magnitude / 10) * x * EaseOfBending);
        }

        // ���x�����̒l�ȉ��Ȃ�0�ɂ���
        if (rb.velocity.magnitude < AppConst.SPEED_THRESHOLD)
        {
            rb.velocity *= 0;
        }
    }
}
