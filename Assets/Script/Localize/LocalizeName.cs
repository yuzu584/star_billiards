using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizeName : MonoBehaviour
{
    [SerializeField] private StringGroup group;
    [SerializeField] private StringEnumStruct type;

    private Localize localize;

    private void Start()
    {
        localize = Localize.instance;

        SetName();
    }

    void SetName()
    {
        // �I�u�W�F�N�g�̖��O��ݒ�
        gameObject.name = localize.GetString(group, type);
    }
}
