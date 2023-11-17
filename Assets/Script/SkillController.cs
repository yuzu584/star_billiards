using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキルを管理
public class SkillController : MonoBehaviour
{
    [SerializeField] private Shot shot;                           // InspectorでShotを指定
    [SerializeField] private SkillUIController skillUIController; // InspectorでSkillUIControllerを指定
    [SerializeField] private EnergyController energyController;   // InspectorでEnergyControllerを指定
    [SerializeField] private ScreenController screenController;   // InspectorでScreenControllerを指定

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
        new Skill("SuperCharge", 10, 1, 10),
        new Skill("PowerSurge", 30, 1, 10),
        new Skill("Huge", 100, 1, 10),
    };

    void Start()
    {
        skillUIController.DrawSkillUI(
            skill[selectSkill].skillName,
            skill[selectSkill].coolDown,
            skill[selectSkill].effectTime,
            coolDown,
            effectTime
            );
    }

    void Update()
    {
        // ゲーム画面なら
        if (screenController.screenNum == 0)
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
    }

    // スキルのUIを描画する関数を呼び出す
    public void CallSetSkillUI()
    {
        skillUIController.DrawSkillUI(
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

        // エネルギーを消費
        energyController.energy -= skill[selectSkill].energyUsage;

        // 選択しているスキルによって分岐
        switch(selectSkill)
        {
            case 0: // SuperCharge
                StartCoroutine(UseSuperCharge());
                break;
            case 1: // PowerSurge
                StartCoroutine(UsePowerSurge());
                break;
            case 2: // Huge
                StartCoroutine(UseHuge());
                break;
        }
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

    // SuperChargeを使用
    IEnumerator UseSuperCharge()
    {
        // チャージスピードを増加
        shot.chargeSpeed += 1;

        // 効果時間が終わるまで待つ
        yield return new WaitForSeconds(effectTime);

        // チャージスピードを元に戻す
        shot.chargeSpeed -= 1;
    }

    // PowerSurgeを使用
    IEnumerator UsePowerSurge()
    {
        // プレイヤーの質量を増加
        shot.playerBouncePower -= 100;
        shot.planetBouncePower += 100;

        // 効果時間が終わるまで待つ
        yield return new WaitForSeconds(effectTime);

        // プレイヤーの質量を増加を元に戻す
        shot.playerBouncePower += 100;
        shot.planetBouncePower -= 100;
    }

    // Hugeを使用
    IEnumerator UseHuge()
    {
        // 巨大化
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // 効果時間が終わるまで待つ
        yield return new WaitForSeconds(effectTime);

        // 巨大化終了
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    }
}
