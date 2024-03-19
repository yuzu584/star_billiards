using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �v���C���[���Ǘ�
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;          // �v���C���[�̃��W�b�g�{�f�B

    private Initialize init;

    // �v���C���[�Ɋւ��鐔�l��������
    void Init()
    {
        rb.velocity *= 0;
        transform.position = AppConst.PLATER_DEFAULT_POSITION;
    }

    void Start()
    {
        init = Initialize.instance;

        // �f���Q�[�g�ɏ������֐���o�^
        init.init_Stage += Init;
    }
}
