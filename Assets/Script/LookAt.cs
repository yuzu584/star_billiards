using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �w��̃I�u�W�F�N�g�Ɍ���������
public class LookAt : MonoBehaviour
{
    public GameObject target; // ���̃I�u�W�F�N�g�̕�������

    void Update()
    {
        // �^�[�Q�b�g�����݂��Ȃ��Ȃ玩�����폜/���݂���Ȃ�^�[�Q�b�g�̕�������
        if (target == null)
            Destroy(this.gameObject);
        else
            transform.LookAt(target.transform);
    }
}
