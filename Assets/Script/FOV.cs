using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    // ���C���J����
    private Camera cam;

    // ���W�b�h�{�f�B
    private Rigidbody rb;

    // ���x���Q�Ƃ���I�u�W�F�N�g
    public GameObject player;

    // ����p
    public int Fov = 60;

    void Start()
    {
        // �J�����̃R���|�[�l���g���擾
        cam = GetComponent<Camera>();

        // ���x���Q�Ƃ���I�u�W�F�N�g��rigidbody���擾
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ����p�����炩�ɕύX
        cam.fieldOfView += (Fov + rb.velocity.magnitude - cam.fieldOfView) * Time.deltaTime;

        // ����p��Fov+30�x�ȏ�Ȃ�
        if(cam.fieldOfView >= Fov + 30)
        {
            // ����p��Fov+30�x�ɂ���
            cam.fieldOfView = Fov + 30;
        }

        // ����p��Fov-10�x�ȉ��Ȃ�
        else if (cam.fieldOfView <= Fov - 10)
        {
            // ����p��Fov-10�x�ɂ���
            cam.fieldOfView = Fov - 10;
        }
    }
}