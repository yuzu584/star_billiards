using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �w��̃I�u�W�F�N�g�Ɍ���������
public class LookAt : MonoBehaviour
{
    public GameObject target; // ���̃I�u�W�F�N�g�̕�������

    void Update()
    {
        // �I�u�W�F�N�g�̕�������
        transform.LookAt(target.transform);
    }
}
