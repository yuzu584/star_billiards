using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �^�[�Q�b�g�̍��W�Ɉړ�����
public class ChaseTarget : MonoBehaviour
{
    public GameObject target; // �^�[�Q�b�g

    // �^�[�Q�b�g�̍��W�Ɉړ�
    public void Chase()
    {
        // �^�[�Q�b�g�̍��W�Ɉړ�
        transform.position = target.transform.position;

        // ���W��������ɂ��炷
        transform.position += new Vector3(0, 0.5f, 0);
    }
}
