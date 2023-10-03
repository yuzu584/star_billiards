using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUntilCollision : MonoBehaviour
{
    // transform�^�̕ϐ���錾
    Transform myTransform;

    // ���[���h���W����ɁA��]���擾
    Vector3 worldAngle;

    public float speed = 1f;

    void Update()
    {
        myTransform = this.transform;
        worldAngle = myTransform.eulerAngles;

        worldAngle.y += speed * Time.deltaTime;

        myTransform.eulerAngles = worldAngle; // ��]�p�x��ݒ�
    }
}
