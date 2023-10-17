using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ƏՓ˂���܂Ō��]�Ǝ��]������
public class RotateUntilCollision : MonoBehaviour
{
    [SerializeField] GameObject target; // ���]�̒��S�Ƃ���I�u�W�F�N�g
    [SerializeField] int speed = 1;     // ���x

    Vector3 targetPosition; // ���]�̒��S�Ƃ���I�u�W�F�N�g�̍��W

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        // �^�[�Q�b�g�̍��W�擾
        targetPosition = target.transform.position;

        // ���]������
        transform.RotateAround(targetPosition, Vector3.up, speed * Time.deltaTime);
    }
}
