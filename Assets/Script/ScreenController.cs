using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ʂ̎�ނ��Ǘ�
public class ScreenController : MonoBehaviour
{
    // ��ʔԍ�
    // InGame = 0
    // Pause  = 1
    public int screenNum = 0;

    void Update()
    {
        // �Q�[�����ɖ߂�{�^���������ꂽ��
        if(Input.GetButtonDown("Cancel") && screenNum == 0)
        {
            // �|�[�Y��ʂɑJ��
            screenNum = 1;
        }
        // �|�[�Y���ɖ߂�{�^���������ꂽ��
        else if (Input.GetButtonDown("Cancel") && screenNum == 1)
        {
            // �Q�[����ʂɑJ��
            screenNum = 0;
        }
    }
}
