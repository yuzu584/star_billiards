using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �u���b�N�z�[�����Ǘ�
public class BrackHole : MonoBehaviour
{
    // Find�ŒT��GameObject
    private GameObject stageController;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    private DestroyPlanet _destroyPlanet;

    // ���͂̃I�u�W�F�N�g�ɏd�͂̉e����^����
    void Gravity()
    {
        // �w�肵�����a�̓����蔻��𐶐�
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            1000.0f,
            Vector3.forward);

        // �����蔻��ɐG�ꂽ�I�u�W�F�N�g�̐��J��Ԃ�
        foreach (var hit in hits)
        {
            // ���������I�u�W�F�N�g��RigidBody���擾
            Rigidbody hitObj = hit.collider.gameObject.GetComponent<Rigidbody>();

            // RigidBody���擾�ł����Ȃ�
            if (hitObj != null)
            {
                // �͂�������x�N�g����ݒ�(�X�P�[���ɂ���ė͂��ς��)
                Vector3 direction = (this.gameObject.transform.position - hitObj.position) * this.transform.localScale.x / 2;

                // �I�u�W�F�N�g�Ƃ̋������߂��قǋ����͂�������
                float distance = Vector3.Distance(this.gameObject.transform.position, hitObj.position);
                hitObj.AddForce(direction / distance);
            }
        }
    }

    // ���������ƏՓ˂�����
    void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g���f���Ȃ�j��
        _destroyPlanet.DestroyPlanetPrpcess(collision.gameObject);
    }

    void Start()
    {
        // GameObject��T��
        stageController = GameObject.Find("StageController");

        // �T����GameObject�̃R���|�[�l���g���擾
        _destroyPlanet = stageController.gameObject.GetComponent<DestroyPlanet>();
    }

    void Update()
    {
        // ���͂̃I�u�W�F�N�g�ɏd�͂̉e����^����
        Gravity();
    }
}
