using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �G�l���M�[�̑������Ǘ�
public class EnergyController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��
    [SerializeField] private GameOver gameOver;                 // Inspector��GameOver���w��
    [SerializeField] private Initialize initialize;             // Inspector��Initialize���w��

    public float energy = 1000;     // �v���C���[�̃G�l���M�[
    public float maxEnergy = 1000;  // �ő�G�l���M�[
    private Rigidbody rb;           // ���W�b�h�{�f�B

    // ����������
    void Init()
    {
        energy = maxEnergy;
    }

    void Start()
    {
        // ���x���Q�Ƃ���I�u�W�F�N�g��rigidbody���擾
        rb = this.GetComponent<Rigidbody>();

        // �f���Q�[�g�ɏ������֐���o�^
        initialize.init_Stage += Init;
    }

    void Update()
    {
        // �Q�[����ʂŃG�l���M�[��0�ɂȂ�����Q�[���I�[�o�[����
        if((screenController.ScreenNum == 5) && (energy <= 0))
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
