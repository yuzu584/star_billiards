using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���˂���Ray��Line�𐶐�
public class PredictionLine : MonoBehaviour
{
    public GameObject target;           // Ray���o���I�u�W�F�N�g
    public GameObject directionTarget;  // Ray�̌��������߂�I�u�W�F�N�g
    public LineRenderer lineRenderer;   // linerenderer�̕ϐ�

    Rigidbody rb;                // Rigidbody�^�̕ϐ�
    Vector3 origin;              // ���_
    Vector3 direction;           // X��������\���x�N�g��
    RaycastHit hit;              // Ray��hit
    Vector3 inDirection;         // ���˃x�N�g���i���x�j
    Vector3 inNormal;            // �@���x�N�g��
    Vector3 reflectionDirection; // ���˃x�N�g��

    void Start()
    {
        // �J�n�_�̑�����0.1�ɂ���
        lineRenderer.startWidth = 0.1f;

        // �I���_�̑�����1�ɂ���
        lineRenderer.endWidth = 1f;

        // lineRenderer�̐��̐�
        lineRenderer.positionCount = 3;

        // rigidbody���擾
        rb = target.GetComponent<Rigidbody>();
    }

    // Ray��Line�̌��������߂�֐�
    public Vector3 RayDirection()
    {
        // ���_��target�̈ʒu�ɂ���
        origin = target.transform.position;

        // �x�N�g����directionTarget�̌����ɂ���
        direction = directionTarget.transform.forward;

        // Ray�𐶐�
        Ray ray = new Ray(origin, direction);

        // ���̂�Ray�𐶐�
        if (Physics.SphereCast(ray, 0.5f, out hit))
        {
            // Ray�ɉ�����Line��`��
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, hit.point);
        }

        // Ray�����������ʂ̖@���x�N�g������
        inNormal = hit.normal;

        // �v���C���[�̑O�����̃x�N�g������
        direction = directionTarget.transform.forward;

        // ���˃x�N�g�����v�Z
        reflectionDirection = Vector3.Reflect(direction, inNormal);

        // Ray�𐶐�
        ray = new Ray(hit.point, reflectionDirection);

        // ���̂�Ray�𐶐�
        if (Physics.SphereCast(ray, 0.5f, out hit))
        {
            // ���ˌ��Ray�ɉ�����Line��`��
            lineRenderer.SetPosition(2, hit.point);
        }

        // ���˕�����Ԃ�
        return reflectionDirection;
    }

    void Update()
    {
        // �������˃{�^��1��������Ă�����
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            // ����\��
            lineRenderer.enabled = true;
        }
        // �������˃{�^��1��������Ă��Ȃ����
        else if (Input.GetAxisRaw("Fire1") == 0)
        {
            // �����\��
            lineRenderer.enabled = false;
        }
    }
}
