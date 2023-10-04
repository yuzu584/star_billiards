using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRay : MonoBehaviour
{
    // Ray���o���I�u�W�F�N�g
    public GameObject target;

    // Ray�̌��������߂�I�u�W�F�N�g
    public GameObject directionTarget;

    // Rigidbody�^�̕ϐ�
    Rigidbody rb;

    Vector3 origin;    // ���_
    Vector3 direction; // X��������\���x�N�g��
    RaycastHit hit;    // Ray��hit

    // ���˃x�N�g��
    public static Vector3 reflectionDirection;

    // ���˃x�N�g���i���x�j
    Vector3 inDirection;

    // �@���x�N�g��
    Vector3 inNormal;

    // linerenderer�̕ϐ�
    public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer.startWidth = 0.1f;        // �J�n�_�̑�����0.1�ɂ���
        lineRenderer.endWidth = 1f;            // �I���_�̑�����0.1�ɂ���

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
