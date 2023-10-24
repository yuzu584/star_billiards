using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキルを管理
public class SkillController : MonoBehaviour
{
    public UIController uIController;

    int selectSkill = 0;  // 選択しているスキルの番号
    float coolDown = 0;   // クールダウンを管理
    float effectTime = 0; // 効果時間を管理

    // スキルの構造体
    public struct Skill
    {
        public string skillName; // スキル名
        public int energyUsage;  // エネルギー消費量
        public float coolDown;   // クールダウン
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
        new Skill("SuperCharge", 10, 3, 5),
        new Skill("PowerSurge", 30, 5, 3),
        new Skill("GravityWave", 100, 8, 1),
    };

    void Update()
    {
        // マウスホイールがスクロールされていたら
        if ((Input.GetAxisRaw("Mouse ScrollWheel") != 0) && (effectTime <= 0) && (coolDown == 0))
        {
            // スキルを変更
            ChangeSkill();
        }

        // スキル使用可能の時にキーが押されたら
        if ((Input.GetAxisRaw("Skill") != 0) && (effectTime == 0) && (coolDown == 0))
        {
            // スキル使用
            UseSkill();
        }

        // スキルの効果時間とクールダウンを減少
        DecreaseEFAndCD();

        // スキルのUIを描画する関数を呼び出す
        CallSetSkillUI();
    }

    // スキルのUIを描画する関数を呼び出す
    void CallSetSkillUI()
    {
        uIController.SetSkillUI(
            skill[selectSkill].skillName,
            skill[selectSkill].coolDown,
            skill[selectSkill].effectTime,
            coolDown,
            effectTime
            );
    }

    // スキルを変更
    void ChangeSkill()
    {
        // 選択しているスキル番号を変える
        selectSkill += (int)(Input.GetAxisRaw("Mouse ScrollWheel") * 10);

        // スキル番号が範囲外なら範囲内に収める
        if (selectSkill < 0)
            selectSkill = 0;
        else if (selectSkill >= skill.Length)
            selectSkill = skill.Length - 1;
    }

    // スキル使用
    void UseSkill()
    {
        // 効果時間とクールダウンを設定
        effectTime = skill[selectSkill].effectTime;
        coolDown = skill[selectSkill].coolDown;

        EnergyController.energy -= skill[selectSkill].energyUsage;
    }

    // スキルの効果時間とクールダウンを減少
    void DecreaseEFAndCD()
    {
        // 効果時間を減少させる
        if (effectTime > 0)
            effectTime -= Time.deltaTime;
        // 効果時間が終了していたならクールダウンを減少させる
        else if (coolDown > 0)
            coolDown -= Time.deltaTime;

        // 数値が0未満なら0にする
        if (effectTime < 0)
            effectTime = 0;
        if (coolDown < 0)
            coolDown = 0;
    }
}
