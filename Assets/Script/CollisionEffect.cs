using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle; // ����������p�[�e�B�N��

    // �����Փ˂�����
    private void OnCollisionEnter(Collision collision)
    {
        // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�
        ParticleSystem newParticle = Instantiate(particle);

        // ���������p�[�e�B�N�����v���C���[�̍��W�Ɉړ�
        newParticle.transform.position = collision.contacts[0].point;

        // �p�[�e�B�N���̌������v���C���[�Ɍ�����
        newParticle.transform.LookAt(gameObject.transform);

        // �p�[�e�B�N���𔭐�
        newParticle.Play();

        // �p�[�e�B�N�����폜
        Destroy(newParticle.gameObject, 5.0f);
    }
}
