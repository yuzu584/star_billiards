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
        // オブジェクトの名前を設定
        gameObject.name = localize.GetString(seet, dataName);
    }
}
