using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Q�[�����̖|��Ɋւ��鏈��
public class Localize : Singleton<Localize>
{
    [SerializeField] private StringTextData stringTextData;     // �e����̃e�L�X�g�������� ScriptableObject

    [SerializeField] private LanguageType language;

    // ��������擾
    public string GetString(StringGroup group, StringType type)
    {
        // ������̃O���[�v�̐��J��Ԃ�
        for (int i = 0; i < stringTextData.strings.Length; ++i)
        {
            // �O���[�v������������
            if (stringTextData.strings[i].group == group)
            {
                // �O���[�v���Ƃ̃e�L�X�g�̐��J��Ԃ�
                for (int j = 0; j < stringTextData.strings[i].stringData.Length; j++)
                {
                    // �w��� StringType �ƈ�v������
                    if (stringTextData.strings[i].stringData[j].type == type)
                    {
                        // �w��� StringType �ƈ�v�����e�L�X�g��Ԃ�
                        return stringTextData.strings[i].stringData[j].text[(int)language];
                    }
                }
            }
        }

        // ������Ȃ������� null �𕶎���ŕԂ�
        return "null";
    }

    // ���ꂲ�Ƃ̃t�H���g���擾
    public Font GetFont()
    {
        // ���ꂲ�Ƃ̃t�H���g�̐��J��Ԃ�
        for(int i = 0; i < stringTextData.fonts.Length; i++)
        {
            // ���݂̌��ꂪ����������
            if (stringTextData.fonts[i].language == language)
            {
                // ���݂̌���̃t�H���g��Ԃ�
                return stringTextData.fonts[i].font;
            }
        }

        return null;
    }
}
