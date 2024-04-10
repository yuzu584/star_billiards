using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �v���C���[���Ǘ�
public class PlayerController : Singleton<PlayerController>
{
    public Rigidbody rb;          // �v���C���[�̃��W�b�g�{�f�B

    private Initialize init;

    // �ړ����x��`�悷��UI�̃f���Q�[�g
    public delegate void SpeedUIDele();
    public SpeedUIDele speedUIDele;

    // �v���C���[�Ɋւ��鐔�l��������
    void Init()
    {
        rb.velocity *= 0;
        transform.position = Const_Player.PLATER_DEFAULT_POSITION;

        // �ړ�����������
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Start()
    {
        init = Initialize.instance;

        // �f���Q�[�g�ɏ������֐���o�^
        init.init_Stage += Init;
    }

    private void Update()
    {
        speedUIDele?.Invoke();
    }
}
