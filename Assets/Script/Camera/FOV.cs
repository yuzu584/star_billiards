using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����p��ύX����
public class FOV : Singleton<FOV>
{
    [SerializeField] private GameObject player;     // ���x���Q�Ƃ���I�u�W�F�N�g

    private ClampedValue<int> fov;                  // �J�����̎���p
    private Rigidbody rb;                           // ���W�b�h�{�f�B

    void Start()
    {
        fov = new ClampedValue<int>(60, 120, 10, nameof(fov));

        // ���x���Q�Ƃ���I�u�W�F�N�g��rigidbody���擾
        rb = player.GetComponent<Rigidbody>();
    }

    // ����p���ړ����x�ɉ����ĕύX
    public void ChangeFOV()
    {
        // ����p�����炩�ɕύX
        Camera.main.fieldOfView += (fov.GetValue_Float() + rb.velocity.magnitude - Camera.main.fieldOfView) * Time.deltaTime;

        // ����p�𐳏�Ȕ͈͂ɕۂ�
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, fov.GetValue_Float() - 10.0f, fov.GetValue_Float() + 30.0f);
    }

    // ����p�������l�Ƀ��Z�b�g
    public void ResetFOV()
    {
        Camera.main.fieldOfView = 60;
    }
}
