using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �G�l���M�[�̑������Ǘ�
public class EnergyController : Singleton<EnergyController>
{
    private ScreenController screenCon;
    private GameOver gameOver;
    private Initialize init;

    private Rigidbody rb;           // ���W�b�h�{�f�B

    public float energy = 1000;     // �v���C���[�̃G�l���M�[
    public float maxEnergy = 1000;  // �ő�G�l���M�[

    // ����������
    void Init()
    {
        energy = maxEnergy;
    }

    void Start()
    {
        screenCon = ScreenController.instance;
        gameOver = GameOver.instance;
        init = Initialize.instance;

        // ���x���Q�Ƃ���I�u�W�F�N�g��rigidbody���擾
        rb = this.GetComponent<Rigidbody>();

        // �f���Q�[�g�ɏ������֐���o�^
        init.init_Stage += Init;
    }

    void Update()
    {
        // �Q�[����ʂŃG�l���M�[��0�ɂȂ�����Q�[���I�[�o�[����
        if((screenCon.Screen == ScreenController.ScreenType.InGame) && (energy <= 0))
        {
            gameOver.GameOverProcess();
        }

        // �G�l���M�[�̐��l���͈͊O�Ȃ�͈͓��ɖ߂�
        if (energy > maxEnergy)
            energy = maxEnergy;
        else if (energy < 0)
            energy = 0;
    }
}
