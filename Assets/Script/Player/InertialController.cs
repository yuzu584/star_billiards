using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �v���C���[�̊����𐧌�
public class InertialController : MonoBehaviour
{
    [SerializeField] private InputController input; // Inspector��InputController���w��

    public float EaseOfBending = 1.0f; // �O���̋Ȃ��₷��
    Rigidbody rb;                      // �v���C���[��Rigidbody
    Vector2 mVec;                      // �O�㍶�E�̈ړ��x�N�g��
    Vector3 vector;                    // �ړ�����

    void Start()
    {
        // rigidbody���擾
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �ړ��ʂ���
        mVec = input.Game_Move;
    }

    void FixedUpdate()
    {
        // �O���͂Ȃ猸�����ɂ₩��
        if (mVec.y > 0)
        {
            rb.velocity *= AppConst.SPEED_MAINTENANCE_RATE;
        }
        // �����͂Ȃ猸��
        else if (mVec.y < 0)
        {
            rb.velocity *= AppConst.SPEED_REDUCTION_RATE;
        }
        // �O����͂���Ă��Ȃ���Ԃō��E���͂Ȃ�O�������E�ɋȂ���
        else if (mVec.x != 0)
        {
            rb.AddForce(Camera.main.transform.right * (rb.velocity.magnitude / 10) * mVec.x * EaseOfBending);
        }

        // ���x�����̒l�ȉ��Ȃ�0�ɂ���
        if (rb.velocity.magnitude < AppConst.SPEED_THRESHOLD)
        {
            rb.velocity *= 0;
        }
    }
}
