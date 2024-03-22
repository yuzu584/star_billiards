using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �u���b�N�z�[�����Ǘ�
public class BrackHole : MonoBehaviour
{
    private ScreenController scrCont;
    private DestroyPlanet destroyPlanet;

    // ���͂̃I�u�W�F�N�g�ɏd�͂̉e����^����
    void Gravity(float radius, string tag, float power)
    {
        // �w�肵�����a�̓����蔻��𐶐�
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            radius,
            Vector3.forward);

        // �����蔻��ɐG�ꂽ�I�u�W�F�N�g�̐��J��Ԃ�
        foreach (var hit in hits)
        {
            // �v���C���[��RigidBody���擾
            Rigidbody hitObj = null;
            if (hit.collider.gameObject.tag == tag)
                hitObj = hit.collider.gameObject.GetComponent<Rigidbody>();

            // RigidBody���擾�ł����Ȃ�
            if (hitObj != null)
            {
                // �͂�������x�N�g����ݒ�(�X�P�[���ɂ���ė͂��ς��)
                Vector3 direction = (gameObject.transform.position - hitObj.position) * transform.localScale.x * power;

                // �I�u�W�F�N�g�Ƃ̋������߂��قǋ����͂�������
                float distance = Vector3.Distance(gameObject.transform.position, hitObj.position);
                hitObj.AddForce(direction / distance);
            }
        }
    }

    // ���������ƏՓ˂�����
    void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g���f���Ȃ�j��
        destroyPlanet.DestroyPlanetProcess(collision.gameObject);
    }

    void Start()
    {
        scrCont = ScreenController.instance;
        destroyPlanet = DestroyPlanet.instance;
    }

    void FixedUpdate()
    {
        // �Q�[����ʂȂ���͂̃I�u�W�F�N�g�ɏd�͂̉e����^����
        if(scrCont.ScreenNum == 5)
        {
            Gravity(1000.0f, "Player", 0.5f);
            Gravity(100.0f, "Player", 1.0f);
            Gravity(100.0f, "Planet", 1.0f);
        }
    }
}
