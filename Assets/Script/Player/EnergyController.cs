using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AppParam;

// �G�l���M�[�̑������Ǘ�
public class EnergyController : Singleton<EnergyController>
{
    private ScreenController screenCon;
    private Initialize init;
    private StageController stageCon;

    private Rigidbody rb;           // ���W�b�h�{�f�B

    // ����������
    void Init()
    {
        Param_Player.energy.Value = Param_Player.energy.Max;
    }

    void Start()
    {
        screenCon = ScreenController.instance;
        init = Initialize.instance;
        stageCon = StageController.instance;

        // ���x���Q�Ƃ���I�u�W�F�N�g��rigidbody���擾
        rb = this.GetComponent<Rigidbody>();

        // �f���Q�[�g�ɏ������֐���o�^
        init.init_Stage += Init;
    }

    void Update()
    {
        // �Q�[����ʂŃG�l���M�[��0�ɂȂ�����Q�[���I�[�o�[����
        if((screenCon.Screen == ScreenController.ScreenType.InGame) && (Param_Player.energy.Value <= 0))
        {
            stageCon.gameOverDele?.Invoke();
        }
    }
}
