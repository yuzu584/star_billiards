using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AppParam;

// ����p��ύX����
public class FOV : Singleton<FOV>
{
    [SerializeField] private GameObject player; // ���x���Q�Ƃ���I�u�W�F�N�g

    private Rigidbody rb; // ���W�b�h�{�f�B

    void Start()
    {
        // ���x���Q�Ƃ���I�u�W�F�N�g��rigidbody���擾
        rb = player.GetComponent<Rigidbody>();
    }

    // ����p���ړ����x�ɉ����ĕύX
    public void ChangeFOV()
    {
        // ����p�����炩�ɕύX
        Camera.main.fieldOfView += ((float)Param_Camera.fov.Value + rb.velocity.magnitude - Camera.main.fieldOfView) * Time.deltaTime;

        // ����p�𐳏�Ȕ͈͂ɕۂ�
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, Param_Camera.fov.Min, Param_Camera.fov.Max);
    }

    // ����p�������l�Ƀ��Z�b�g
    public void ResetFOV()
    {
        Camera.main.fieldOfView = Param_Camera.fov.Value;
    }
}
