using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
