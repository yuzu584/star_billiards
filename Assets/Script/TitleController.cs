using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �^�C�g����ʂ��Ǘ�
public class TitleController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��

    void Update()
    {
        if (Input.anyKey)
        {
            screenController.screenNum = 1;
        }
    }
}
