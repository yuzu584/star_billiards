using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// ���˂���Ray��Line�𐶐�
public class PredictionLine : Singleton<PredictionLine>
{
    [SerializeField] private GameObject target;                 // Ray���o���I�u�W�F�N�g
    [SerializeField] private GameObject directionTarget;        // Ray�̌��������߂�I�u�W�F�N�g
    [SerializeField] private LineRenderer lineRenderer;         // Inspector��linerenderer���w��

    private EnergyController eneCon;
    private InputController input;

    private Rigidbody rb;                   // Inspector��Rigidbody���w��
    private Vector3 origin;                 // ���_
    private Vector3 direction;              // X��������\���x�N�g��
    private Vector3 inDirection;            // ���˃x�N�g���i���x�j
    private Vector3 inNormal;               // �@���x�N�g��
    private Vector3 reflectionDirection;    // ���˃x�N�g��

    public RaycastHit hit1, hit2;           // Ray��hit

    void Start()
    {
        eneCon = EnergyController.instance;
        input = InputController.instance;

        // �n�_�̑������w��
        lineRenderer.startWidth = AppConst.PREDICTION_LINE_START_WIDTH;

        // �I�_�̑������w��
        lineRenderer.endWidth = AppConst.PREDICTION_LINE_END_WIDTH;

        // lineRenderer�̐��̐����w��
        lineRenderer.positionCount = 2;

        // �����\��
        lineRenderer.enabled = false;

        // rigidbody���擾
        rb = target.GetComponent<Rigidbody>();

        input.game_OnShotDele += RenderProcess;
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

        // �v���C���[�Ɠ�������Ray�𐶐�
        if (Physics.SphereCast(ray, target.transform.localScale.x, out hit1))
        {
            // Ray�����������ʂ̖@���x�N�g������
            inNormal = hit1.normal;
        }

        // �v���C���[�̑O�����̃x�N�g������
        direction = directionTarget.transform.forward;

        // ���˃x�N�g�����v�Z
        reflectionDirection = Vector3.Reflect(direction, inNormal);

        // Ray�𐶐�
        ray = new Ray(hit1.point, reflectionDirection);

        // �v���C���[�Ɠ�������Ray�𐶐�
        if (Physics.SphereCast(ray, target.transform.localScale.x, out hit2))
        {
            // ���ˌ��Ray�ɉ�����Line��`��
            lineRenderer.SetPosition(0, hit1.point);
            lineRenderer.SetPosition(1, hit2.point);
        }

        // ���˕�����Ԃ�
        return reflectionDirection;
    }

    // ����\��/��\���ɂ��鏈��
    void RenderProcess(float value)
    {
        // �G�l���M�[�������ԂŃV���b�g�{�^����������Ă�����
        if ((value > 0) && (eneCon.energy > 0))
        {
            // ����\��
            lineRenderer.enabled = true;
        }
        // �����\������Ă�����
        else if (lineRenderer.enabled == true)
        {
            // �����\��
            lineRenderer.enabled = false;
        }
    }
}
