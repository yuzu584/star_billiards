using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// スキルを管理
public class SkillController : MonoBehaviour
{
    [SerializeField] private Shot shot;                           // InspectorでShotを指定
    [SerializeField] private SkillUIController skillUIController; // InspectorでSkillUIControllerを指定
    [SerializeField] private EnergyController energyController;   // InspectorでEnergyControllerを指定
    [SerializeField] private ScreenController screenController;   // InspectorでScreenControllerを指定
    [SerializeField] private ParticleSystem GravityWaveParticle;  // GravityWaveのパーティクル

    public int selectSkill = 0;  // 選択しているスキルの番号
    public float coolDown = 0;   // クールダウンを管理
    public float effectTime = 0; // 効果時間を管理

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
            AppConst.SKILL_NAME[selectSkill],
            AppConst.SKILL_COOLDOWN[selectSkill],
            AppConst.SKILL_EFFECT_TIME[selectSkill],
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
        else if (selectSkill >= AppConst.SKILL_NUM)
            selectSkill = AppConst.SKILL_NUM - 1;
    }

    // スキル使用
    void UseSkill()
    {
        // 効果時間とクールダウンを設定
        effectTime = AppConst.SKILL_EFFECT_TIME[selectSkill];
        coolDown = AppConst.SKILL_COOLDOWN[selectSkill];

        // エネルギーを消費
        energyController.energy -= AppConst.SKILL_ENERGY_USAGE[selectSkill];

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
            case 3: // GravityWave
                UseGravityWave();
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
        shot.chargeSpeed = AppConst.DEFAULT_CHARGE_SPEED + AppConst.CHARGE_SPEED_INCREASE_AMOUNT;

        // 効果時間が終わるまで待つ
        yield return new WaitForSeconds(effectTime);

        // チャージスピードを元に戻す
        InitSkillEffect();
    }

    // PowerSurgeを使用
    IEnumerator UsePowerSurge()
    {
        // プレイヤーの質量を増加
        shot.playerBouncePower = AppConst.DEFAULT_BOUNCE_POWER - AppConst.BOUNCE_POWER_INCREASE_AMOUNT;
        shot.planetBouncePower = AppConst.DEFAULT_BOUNCE_POWER + AppConst.BOUNCE_POWER_INCREASE_AMOUNT;

        // 効果時間が終わるまで待つ
        yield return new WaitForSeconds(effectTime);

        // プレイヤーの質量を増加を元に戻す
        InitSkillEffect();
    }

    // Hugeを使用
    IEnumerator UseHuge()
    {
        // 巨大化
        transform.localScale = AppConst.PLAYER_DEFAULT_SCALE * AppConst.SIZE_INCREASE_RATE;

        // 効果時間が終わるまで待つ
        yield return new WaitForSeconds(effectTime);

        // 巨大化終了
        InitSkillEffect();

    }

    // GravityWaveを使用
    void UseGravityWave()
    {
        // パーティクルを生成
        ParticleSystem newParticle = Instantiate(GravityWaveParticle);
        newParticle.transform.position = this.gameObject.transform.position;
        newParticle.Play();
        Destroy(newParticle.gameObject, 2.0f);

        // 指定した半径の当たり判定を生成
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            1000.0f,
            Vector3.forward);

        // hitしたオブジェクトの数繰り返す
        foreach (var hit in hits)
        {
            // 当たったオブジェクトのRigidBodyを取得
            Rigidbody hitObj = hit.collider.gameObject.GetComponent<Rigidbody>();

            // RigidBodyが取得できたかつ取得したオブジェクトが惑星なら
            if ((hitObj != null) && (hit.collider.gameObject.tag == "Planet"))
            {
                // 力を加えるベクトルを設定(最後に30000を掛けて力を調整)
                Vector3 direction = (this.gameObject.transform.position - hitObj.position) * 30000;

                // オブジェクトとの距離が近いほど強い力を加える
                float distance = Vector3.Distance(this.gameObject.transform.position, hitObj.position);
                hitObj.AddForce(-direction / distance);
            }
        }
    }

    // 初期化
    public void Init()
    {
        StopAllCoroutines();
        InitSkillEffect();
        selectSkill = 0;
        coolDown = 0;
        effectTime = 0;
    }

    // スキルの効果を消し去る
    void InitSkillEffect()
    {
        shot.chargeSpeed = AppConst.DEFAULT_CHARGE_SPEED;
        shot.playerBouncePower = AppConst.DEFAULT_BOUNCE_POWER;
        shot.planetBouncePower = AppConst.DEFAULT_BOUNCE_POWER;
        transform.localScale = AppConst.PLAYER_DEFAULT_SCALE;
    }
}
