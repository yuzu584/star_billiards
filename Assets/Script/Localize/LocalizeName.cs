using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizeName : MonoBehaviour
{
    [SerializeField] private string seet;
    [SerializeField] private string dataName;

    private Localize localize;

    private void Start()
    {
        localize = Localize.instance;

        SetName();
    }

    void SetName()
    {
        // �I�u�W�F�N�g�̖��O��ݒ�
        gameObject.name = localize.GetString(seet, dataName);
    }
}
