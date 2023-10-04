using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTorque : MonoBehaviour
{
    public float speed = 1f;                       // ‰ñ“]‘¬“x
    public Vector3 torque = new Vector3(0, 1, 0);  // ‰ñ“]•ûŒü

    void Start()
    {
        // Rigidbody‚ðŽæ“¾
        Rigidbody rb = GetComponent<Rigidbody>();

        // ‰ñ“]•ûŒü‚É‰Á‚¦‚é‘¬“x‚ðŒvŽZ
        torque *= speed;

        // Rigidbody‚É—Í‚ð‰Á‚¦‚é
        rb.AddTorque(torque, ForceMode.Acceleration);
    }
}
