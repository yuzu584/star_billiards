using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �f���̗ʂ��Ǘ�
public class PlanetAmount : MonoBehaviour
{
    [SerializeField] private Initialize initialize; // Inspector��Initialize���w��

    public int planetDestroyAmount = 0; // �f����j�󂵂���

    // ����������
    void Init()
    {
        planetDestroyAmount = 0;
    }

    void Start()
    {
        // �f���Q�[�g�ɏ������֐���o�^
        initialize.init_Stage += Init;
    }
}
