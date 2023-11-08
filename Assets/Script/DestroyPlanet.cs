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
            // �|�b�v�A�b�v�̐����J�E���g
            uIController.popupAmount++;

            // �f�����j�󂳂ꂽ�|��`����|�b�v�A�b�v��`��
            StartCoroutine(uIController.DrawDestroyPlanetPopup(collision.gameObject.name));

            // �f�����폜
            Destroy(collision.gameObject);
        }
    }
}
