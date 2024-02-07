using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����p��ύX����
public class FOV : MonoBehaviour
{
    [SerializeField] private GameObject player; // ���x���Q�Ƃ���I�u�W�F�N�g
    [SerializeField] private int Fov = 60;      // ����p
    [SerializeField] private int maxFov = 90;   // �ő压��p
    [SerializeField] private int minFov = 60;   // �ŏ�����p

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
        Camera.main.fieldOfView += (Fov + rb.velocity.magnitude - Camera.main.fieldOfView) * Time.deltaTime;

        // ����p�𐳏�Ȕ͈͂ɕۂ�
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minFov, maxFov);
    }

    // ����p�������l�Ƀ��Z�b�g
    public void ResetFOV()
    {
        Camera.main.fieldOfView = Fov;
    }
}
