using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �P�����Ǘ�
public class FixedStar : MonoBehaviour
{
    private DestroyPlanet destroyPlanet;

    // ���������ƏՓ˂�����
    void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g���f���Ȃ�j��
        destroyPlanet.DestroyPlanetProcess(collision.gameObject);
    }

    void Start()
    {
        destroyPlanet = DestroyPlanet.instance;
    }
}
