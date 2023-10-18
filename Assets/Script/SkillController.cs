using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキルを管理
public class SkillController : MonoBehaviour
{
    // スキルの構造体
    public struct Skill
    {
        public string skillName; // スキル名
        public int energyUsage;  // エネルギー消費量
        public float coolDown;   // 再使用可能時間
        public float effectTime; // 効果時間

        // 構造体の初期化関数
        public Skill(string sN, int eU, float cD, float eT)
        {
            skillName = sN;
            energyUsage = eU;
            coolDown = cD;
            effectTime = eT;
        }
    }

    // スキルの構造体の配列を宣言
    public Skill[] skill =
    {
        new Skill("SUPER CHARGE", 10, 5, 10),
        new Skill("POWER SURGE", 30, 10, 5),
        new Skill("GRAVITY WAVE", 100, 15, 1),
        new Skill("", -1, -1, -1)
    };
}
