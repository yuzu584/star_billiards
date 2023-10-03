using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlanet : MonoBehaviour
{
    // ‚à‚µ‰½‚©‚ÆÕ“Ë‚µ‚½‚ç
    void OnCollisionEnter(Collision collision)
    {
        // ˜f¯‚ÆÕ“Ë‚µ‚½‚ç
        if (collision.gameObject.CompareTag("Planet"))
        {
            // ˜f¯‚ğíœ
            Destroy(collision.gameObject);
        }
    }
}
