using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Q�[�����̖|��Ɋւ��鏈��
public class Localize : Singleton<Localize>
{
    // ���ꂲ�Ƃ̏�񂪓����� ScriptableObject
    [SerializeField] private LanguageData languageData;

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
        // ��������͓��{��
        Language = LanguageType.Japanese;
    }

    // ��������擾
    public string GetString()
    {
        return "���쒆";
    }

    // ���ꂲ�Ƃ̃t�H���g���擾
    public Font GetFont()
    {
        // ���ꂲ�Ƃ̃t�H���g�̐��J��Ԃ�
        for(int i = 0; i < languageData.fonts.Length; i++)
        {
            // ���݂̌��ꂪ����������
            if (languageData.fonts[i].language == Language)
            {
                // ���݂̌���̃t�H���g��Ԃ�
                return languageData.fonts[i].font;
            }
        }

        return null;
    }
}
