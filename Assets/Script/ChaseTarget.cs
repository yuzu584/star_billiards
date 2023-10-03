using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : MonoBehaviour
{
    // ’Ç‚¢‚©‚¯‚é‘ÎÛ
    public GameObject target;

    void Update()
    {
        // À•W‚ğ‘ÎÛ‚ÌˆÊ’u‚ÉˆÚ“®
        transform.position = target.transform.position;

        // À•W‚ğ­‚µã‚É‚¸‚ç‚·
        transform.position += new Vector3(0, 0.5f, 0);
    }
}
