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
        // オブジェクトの名前を設定
        gameObject.name = localize.GetString(group, type);
    }
}
