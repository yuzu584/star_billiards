using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// f―ΜΚπΗ
public class PlanetAmount : MonoBehaviour
{
    [SerializeField] private Initialize initialize; // InspectorΕInitializeπwθ

    public int planetDestroyAmount = 0; // f―πjσ΅½

    // ϊ»
    void Init()
    {
        planetDestroyAmount = 0;
    }

    void Start()
    {
        // fQ[gΙϊ»Φπo^
        initialize.init_Stage += Init;
    }
}
