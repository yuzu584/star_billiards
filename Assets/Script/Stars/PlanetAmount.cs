using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �f���̗ʂ��Ǘ�
public class PlanetAmount : Singleton<PlanetAmount>
{
    private Initialize init;

    public int planetDestroyAmount = 0; // �f����j�󂵂���

    // ����������
    void Init()
    {
        planetDestroyAmount = 0;
    }

    void Start()
    {
        init = Initialize.instance;

        // �f���Q�[�g�ɏ������֐���o�^
        init.init_Stage += Init;
    }
}
