using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : MonoBehaviour
{
    // �ǂ�������Ώ�
    public GameObject target;

    void Update()
    {
        // ���W��Ώۂ̈ʒu�Ɉړ�
        transform.position = target.transform.position;

        // ���W��������ɂ��炷
        transform.position += new Vector3(0, 0.5f, 0);
    }
}
