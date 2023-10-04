using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTorque : MonoBehaviour
{
    // ‰ñ“]‘¬“x
    public float speed = 1f;

    // ‰ñ“]•ûŒü
    public Vector3 torque = new Vector3(0, 1, 0);

    void Start()
    {
        // Rigidbody‚ðŽæ“¾
        Rigidbody rb = GetComponent<Rigidbody>();

        torque *= speed;

        // Rigidbody‚É—Í‚ð‰Á‚¦‚é
        rb.AddTorque(torque, ForceMode.Acceleration);
    }
}
