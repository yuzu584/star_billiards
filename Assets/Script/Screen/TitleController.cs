using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �^�C�g����ʂ��Ǘ�
public class TitleController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��

    void Update()
    {
        // �^�C�g����ʂŉ�������̓��͂��s��ꂽ�烁�C�����j���[�ɑJ��
        if ((Input.anyKey) && (screenController.ScreenNum == 0))
        {
            screenController.ScreenNum = 1;
        }
    }
}
