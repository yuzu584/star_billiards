using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �e�L�X�g�̕����������ɉ����ĕς���
public class LocalizeText : MonoBehaviour
{
    [SerializeField] private StringGroup group;
    [SerializeField] private StringEnumStruct type;
    [SerializeField] private Text text;

    private Localize localize;

    private void Start()
    {
        localize = Localize.instance;

        SetText();
    }

    void SetText()
    {
        // �e�L�X�g��ݒ�
        text.text = localize.GetString(group, type);

        // �t�H���g��ݒ�
        Font f = localize.GetFont();
        if(f != null)
            text.font = f;
    }
}
