using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �e�L�X�g�̕����������ɉ����ĕς���
public class LocalizeText : MonoBehaviour
{
    public string seet;
    public string dataName;
    public Text text;

    private Localize localize;

    private void Start()
    {
        localize ??= Localize.instance;

        SetText();

        localize.switchLanguageDele += SetText;
    }

    private void OnDestroy()
    {
        if(localize != null)
            localize.switchLanguageDele -= SetText;
    }

    public void SetText()
    {
        localize ??= Localize.instance;

        // �e�L�X�g��ݒ�
        text.text = localize.GetString(seet, dataName);

        // �t�H���g��ݒ�
        Font f = localize.GetFont();
        if(f != null)
            text.font = f;
    }
}
