using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ɏՓ˂����f�����폜
public class DestroyPlanet : MonoBehaviour
{
    [SerializeField] UIController uIController;

    // ���������ƏՓ˂�����
    void OnCollisionEnter(Collision collision)
    {
        // �f���ƏՓ˂�����
        if (collision.gameObject.CompareTag("Planet"))
        {
            // �f�����j�󂳂ꂽ�|��`����|�b�v�A�b�v��`��
            uIController.DrawDestroyPlanetPopup(collision.gameObject.name);

            // �f�����폜
            Destroy(collision.gameObject);
        }
    }
}
