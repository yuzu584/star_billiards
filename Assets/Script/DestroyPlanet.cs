using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ©•ª‚ÉÕ“Ë‚µ‚½˜f¯‚ğíœ
public class DestroyPlanet : MonoBehaviour
{
    [SerializeField] UIController uIController;

    // ‚à‚µ‰½‚©‚ÆÕ“Ë‚µ‚½‚ç
    void OnCollisionEnter(Collision collision)
    {
        // ˜f¯‚ÆÕ“Ë‚µ‚½‚ç
        if (collision.gameObject.CompareTag("Planet"))
        {
            // ˜f¯‚ª”j‰ó‚³‚ê‚½|‚ğ“`‚¦‚éƒ|ƒbƒvƒAƒbƒv‚ğ•`‰æ
            uIController.DrawDestroyPlanetPopup(collision.gameObject.name);

            // ˜f¯‚ğíœ
            Destroy(collision.gameObject);
        }
    }
}
