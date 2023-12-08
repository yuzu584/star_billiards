using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �v���C���[���Ǘ�
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb; // �v���C���[�̃��W�b�g�{�f�B

    // �v���C���[�Ɋւ��鐔�l��������
    public void Init(EnergyController energyController, EnergyUIController energyUIController, SkillController skillController, SkillUIController skillUIController)
    {
        rb.velocity *= 0;
        transform.position = AppConst.PLATER_DEFAULT_POSITION;
        energyController.energy = energyController.maxEnergy;
        energyUIController.InitEnergyUI();
        skillController.Init();
        skillUIController.InitSkillUI();
    }
}
