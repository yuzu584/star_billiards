using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �e�L�X�g�̕����������ɉ����ĕς���
public class LocalizeText : MonoBehaviour
{
    public StringGroup group;
    public StringEnumStruct type;
    public Text text;

    private Localize localize;

    private void Start()
    {
        localize = Localize.instance;

        SetText();

        localize.switchLanguageDele += SetText;
    }

    private void OnDestroy()
    {
        if(localize != null)
            localize.switchLanguageDele -= SetText;
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
