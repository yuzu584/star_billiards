using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����p��ύX����
public class FOV : MonoBehaviour
{
    [SerializeField] private GameObject player;                 // ���x���Q�Ƃ���I�u�W�F�N�g
    [SerializeField] private int Fov = 60;                      // ����p

    private Camera cam;   // ���C���J����
    private Rigidbody rb; // ���W�b�h�{�f�B

    void Start()
    {
        // �J�����̃R���|�[�l���g���擾
        cam = GetComponent<Camera>();

        // ���x���Q�Ƃ���I�u�W�F�N�g��rigidbody���擾
        rb = player.GetComponent<Rigidbody>();
    }

    // ����p���ړ����x�ɉ����ĕύX
    public void ChangeFOV()
    {
        // ����p�����炩�ɕύX
        cam.fieldOfView += (Fov + rb.velocity.magnitude - cam.fieldOfView) * Time.deltaTime;

        // ����p��Fov+30�x�ȏ�Ȃ�
        if (cam.fieldOfView >= Fov + 30)
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

    // ����p�������l�Ƀ��Z�b�g
    public void ResetFOV()
    {
        cam.fieldOfView = Fov;
    }
}
