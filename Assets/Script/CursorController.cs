using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �}�E�X�J�[�\�����Ǘ�
public class CursorController : MonoBehaviour
{
    void Start()
    {
        // �J�[�\������ʒ����ɌŒ�
        Cursor.lockState = CursorLockMode.Locked;

        // �J�[�\����\��
        Cursor.visible = false;
    }
}
