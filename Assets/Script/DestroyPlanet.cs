using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �f���������ɏՓ˂�����f�����폜
public class DestroyPlanet : MonoBehaviour
{
    // ���������ƏՓ˂�����
    void OnCollisionEnter(Collision collision)
    {
        // �f���ƏՓ˂�����
        if (collision.gameObject.CompareTag("Planet"))
        {
            // �f�����폜
            Destroy(collision.gameObject);
        }
    }
}
