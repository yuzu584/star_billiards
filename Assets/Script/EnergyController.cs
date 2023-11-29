using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �G�l���M�[�̑������Ǘ�
public class EnergyController : MonoBehaviour
{
    public float energy = 1000;     // �v���C���[�̃G�l���M�[
    public float maxEnergy = 1000;  // �ő�G�l���M�[
    private Rigidbody rb;           // ���W�b�h�{�f�B

    void Start()
    {
        // ���x���Q�Ƃ���I�u�W�F�N�g��rigidbody���擾
        rb = this.GetComponent<Rigidbody>();
    }

    // �����ƏՓ˂����Ƃ�
    void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g�̃^�O��Planet�Ȃ�
        if (collision.gameObject.CompareTag("Planet"))
        {
            // �G�l���M�[���Փ˂����Ƃ��̑��x�ɉ����ĉ񕜂�����
            energy += rb.velocity.magnitude / 10;
        }
    }

    void FixedUpdate()
    {
        // �G�l���M�[�̐��l���͈͊O�Ȃ�͈͓��ɖ߂�
        if (energy > maxEnergy)
            energy = maxEnergy;
        else if (energy < 0)
            energy = 0;
    }
}
