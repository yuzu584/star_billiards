using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڕW�̍��W�Ɉړ�����
public class ChaseTarget : MonoBehaviour
{
    public GameObject target;  // �ǂ�������Ώ�

    void Update()
    {
        // ���W��Ώۂ̈ʒu�Ɉړ�
        transform.position = target.transform.position;

        // ���W��������ɂ��炷
        transform.position += new Vector3(0, 0.5f, 0);
    }
}
