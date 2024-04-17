using Illogic.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Q�[�����̖|��Ɋւ��鏈��
public class Localize : Singleton<Localize>
{
    [SerializeField] private Font font;

    // �|��e�L�X�g���܂Ƃ߂�ODS�t�@�C��
    private ODSReader reader;

    public enum LanguageType
    {
        English,
        Japanese,
    }

    private LanguageType language;
    public LanguageType Language
    {
        get { return language; }
        set
        {
            language = value;
            switchLanguageDele?.Invoke();
        }
    }

    public event System.Action switchLanguageDele;

    private void Start()
    {
        // �|��e�L�X�g���܂Ƃ߂�ODS�t�@�C����ǂݍ���
        reader = new ODSReader("localize_string_data.ods");

        // ��������͓��{��
        Language = LanguageType.Japanese;
    }

    // ��������擾
    public string GetString(string seet, string name)
    {
        if (seet == "") return NotFindText();
        if (name == "") return NotFindText();

        // �V�[�g�����w��
        var seetData = new DataTable(reader, seet);

        // ���o�������f�[�^����id(����ԍ�)���w��
        var s = seetData[(int)Language][name];

        return s;
    }

    // �t�H���g���擾
    public Font GetFont()
    {
        return font;
    }

    // �w��̃e�L�X�g��������Ȃ������Ƃ�
    public string NotFindText()
    {
        return "Could not find the text";
    }
}
