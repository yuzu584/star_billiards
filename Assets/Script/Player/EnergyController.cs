using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �G�l���M�[�̑������Ǘ�
public class EnergyController : Singleton<EnergyController>
{
    public ClampedValue<int> energy;        // �G�l���M�[
    public ClampedValue<int> maxEnergy;     // �ő�G�l���M�[

    private ScreenController screenCon;
    private Initialize init;
    private StageController stageCon;

    // ����������
    void Init()
    {
        energy.SetValue(energy.GetMax());
    }

    void Start()
    {
        screenCon = ScreenController.instance;
        init = Initialize.instance;
        stageCon = StageController.instance;

        energy = new ClampedValue<int>(1000, 1000, 0, nameof(energy));
        maxEnergy = new ClampedValue<int>(1000, 10000, 0, nameof(maxEnergy));

        // "�ő�G�l���M�[" �̒l���ω������Ƃ��� "�G�l���M�[" �̍ő�l��ݒ�
        maxEnergy.SetOnValueChanged(() => { energy.SetMax(maxEnergy.GetValue_Int()); });

        // �f���Q�[�g�ɏ������֐���o�^
        init.init_Stage += Init;
    }

    void Update()
    {
        // �Q�[����ʂŃG�l���M�[��0�ɂȂ�����Q�[���I�[�o�[����
        if((screenCon.Screen == ScreenController.ScreenType.InGame) && (energy.GetValue_Float() <= 0))
        {
            stageCon.gameOverDele?.Invoke();
        }
    }
}
