using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
    // �v���C���[�̃G�l���M�[
    public static float energy = 1000;

    // �ő�G�l���M�[
    public static float maxEnergy = 1000;

    // ���W�b�h�{�f�B
    private Rigidbody rb;

    void Start()
    {
        // ���x���Q�Ƃ���I�u�W�F�N�g��rigidbody���擾
        rb = this.GetComponent<Rigidbody>();
    }

    // �����ƏՓ˂����Ƃ�
    void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g�̃^�O��Planet�Ȃ�
        if (collision.gameObject.tag == "Planet")
        {
            // �G�l���M�[���Փ˂����Ƃ��̑��x�ɉ����ĉ񕜂�����
            energy += rb.velocity.magnitude / 10;
        }
    }

    void FixedUpdate()
    {
        // �����G�l���M�[���ő�l�𒴂��Ă���
        if(energy > maxEnergy)
        {
            // �ő�l�ɖ߂�
            energy = maxEnergy;
        }
        // �����G�l���M�[��0�����Ȃ�
        else if(energy < 0)
        {
            // �G�l���M�[��0�ɂ���
            energy = 0;
        }
    }
}
