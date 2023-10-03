using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUntilCollision : MonoBehaviour
{
    // transformŒ^‚Ì•Ï”‚ğéŒ¾
    Transform myTransform;

    // ƒ[ƒ‹ƒhÀ•W‚ğŠî€‚ÉA‰ñ“]‚ğæ“¾
    Vector3 worldAngle;

    public float speed = 1f;

    void Update()
    {
        myTransform = this.transform;
        worldAngle = myTransform.eulerAngles;

        worldAngle.y += speed * Time.deltaTime;

        myTransform.eulerAngles = worldAngle; // ‰ñ“]Šp“x‚ğİ’è
    }
}
