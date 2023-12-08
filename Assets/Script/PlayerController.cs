using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーを管理
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb; // プレイヤーのリジットボディ

    // プレイヤーに関する数値を初期化
    public void Init(EnergyController energyController, EnergyUIController energyUIController, SkillController skillController, SkillUIController skillUIController)
    {
        rb.velocity *= 0;
        energyController.energy = energyController.maxEnergy;
        energyUIController.InitEnergyUI();
        skillController.Init();
        skillUIController.InitSkillUI();
    }
}
