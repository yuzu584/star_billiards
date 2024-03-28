using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// スキルを管理
public class SkillController : Singleton<SkillController>
{
    [SerializeField] private Transform playerT;                     // プレイヤーの座標
    [SerializeField] private ParticleSystem GravityWaveParticle;    // GravityWaveのパーティクル

    private Shot shot;
    private EnergyController eneCon;
    private ScreenController screenCon;
    private Initialize init;
    private SphereRay sphereRay;
    private InputController input;

    public int selectSkill = 0;  // 選択しているスキルの番号
    public float coolDown = 0;   // クールダウンを管理
    public float effectTime = 0; // 効果時間を管理

    public enum SkillType
    {
        SuperCharge,
        PowerSurge,
        Huge,
        GravityWave,
        Frieze,
        GrapplingHook,
        Slow,
        InertialControl,
        Blink,
        TeleportAnchor,
        None,
    }

    public SkillType[] skillSlot = new SkillType[AppConst.SKILL_SLOT_AMOUNT];  // スキルスロット

    void Start()
    {
        shot = Shot.instance;
        eneCon = EnergyController.instance;
        screenCon = ScreenController.instance;
        init = Initialize.instance;
        sphereRay = SphereRay.instance;
        input = InputController.instance;

        // スキルスロットを初期化
        skillSlot[0] = SkillType.SuperCharge;
        skillSlot[1] = SkillType.PowerSurge;
        skillSlot[2] = SkillType.Huge;

        // デリゲートに初期化関数を登録
        init.init_Stage += Init;

        input.game_OnUseSkillDele += UseSkill;
        input.game_OnChangeSkillDele += ChangeSkill;
    }

    void Update()
    {
        // ゲーム画面なら
        if (screenCon.Screen == ScreenController.ScreenType.InGame)
        {
            // スキルの効果時間とクールダウンを減少
            DecreaseEFAndCD();
        }
    }

    // スキル使用
    void UseSkill(float value)
    {
        // スキル使用可能の時にキーが押されたら
        if ((value != 0) && (effectTime == 0) && (coolDown == 0))
        {
            // 効果時間とクールダウンを設定
            effectTime = AppConst.SKILL_EFFECT_TIME[(int)skillSlot[selectSkill]];
            coolDown = AppConst.SKILL_COOLDOWN[(int)skillSlot[selectSkill]];

            // エネルギーを消費
            eneCon.energy -= AppConst.SKILL_ENERGY_USAGE[(int)skillSlot[selectSkill]];

            // 選択しているスキルによって分岐
            switch (skillSlot[selectSkill])
            {
                case SkillType.SuperCharge: StartCoroutine(UseSuperCharge());   break;
                case SkillType.PowerSurge:  StartCoroutine(UsePowerSurge());    break;
                case SkillType.Huge:        StartCoroutine(UseHuge());          break;
                case SkillType.GravityWave: UseGravityWave();                   break;
                case SkillType.Frieze:      UseFrieze();                        break;
            }
        }
    }

    // スキルを変更
    void ChangeSkill(float value)
    {
        // スキル変更ボタンが押されていたら
        if ((value != 0) && (effectTime <= 0) && (coolDown == 0))
        {
            // 選択しているスキル番号を変える
            selectSkill += (int)value;

            // スキル番号が範囲外なら範囲内に収める
            if (selectSkill < 0)
                selectSkill = 0;
            else if (selectSkill > AppConst.SKILL_SLOT_AMOUNT - 1)
                selectSkill = AppConst.SKILL_SLOT_AMOUNT - 1;
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
        // パーティクルを生成->再生
        ParticleSystem newParticle = Instantiate(GravityWaveParticle);
        newParticle.transform.position = playerT.position;
        newParticle.Play();

        // 指定した半径の当たり判定を生成
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            1000.0f,
            Vector3.forward);

        // 当たり判定に触れたオブジェクトの数繰り返す
        foreach (var hit in hits)
        {
            // 当たったオブジェクトのRigidBodyを取得
            Rigidbody hitObj = hit.collider.gameObject.GetComponent<Rigidbody>();

            // RigidBodyが取得できたかつ取得したオブジェクトが惑星なら
            if ((hitObj != null) && (hit.collider.gameObject.tag == "Planet"))
            {
                // 力を加えるベクトルを設定(最後に30000を掛けて力を調整)
                Vector3 direction = (playerT.position - hitObj.position) * 30000;

                // オブジェクトとの距離が近いほど強い力を加える
                float distance = Vector3.Distance(playerT.position, hitObj.position);
                hitObj.AddForce(-direction / distance);
            }
        }

        // パーティクルを削除
        Destroy(newParticle.gameObject, 2.0f);
    }

    // Friezeを使用
    void UseFrieze()
    {
        // Rayに当たったオブジェクトのRigidBodyを取得
        Rigidbody hitRb = sphereRay.hit.collider.gameObject.GetComponent<Rigidbody>();

        // 対象が惑星なら動きを止める
        if (sphereRay.hitObjectTag == "Planet")
            hitRb.velocity *= 0;
    }

    // 初期化
    void Init()
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
