using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �P�����Ǘ�
public class FixedStar : MonoBehaviour
{
    // Find�ŒT��GameObject
    private GameObject stageController;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    private DestroyPlanet _destroyPlanet;

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
}
