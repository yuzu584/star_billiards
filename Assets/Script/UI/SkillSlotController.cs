using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using Const;

// スキルスロットを管理
public class SkillSlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image image;          // スキルスロットの画像
    [SerializeField] private Image imageOutline;   // スキルスロットの枠画像
    [SerializeField] private Text selectNumText;   // スキルの選択した順を表すテキスト
    [SerializeField] private Color OnPointerColor; // ポインターが乗った時の色
    [SerializeField] private Color defaultColor;   // デフォルトの色
    [SerializeField] private float fadeTime;       // フェード時間

    public int skillNum; // スキル番号

    // Findで探すもの
    private GameObject UIFunctionController;
    private GameObject Player;

    // Findで探したGameObjectのコンポーネント
    private Lerp lerp;
    private SkillController skillController;
    private SkillSelectUIController skillSelectUIController;

    // マウスポインターがボタンの上に乗ったら
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ボタンのアニメーション
        StopAllCoroutines();
        StartCoroutine(lerp.ChangeColor(image, defaultColor, OnPointerColor, fadeTime));

        // スキルの情報を表示するUIを更新
        skillSelectUIController.DrawSkillInfo(skillNum);
    }

    // マウスポインターがボタンの上から離れたら
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ボタンのアニメーション
        StopAllCoroutines();
        StartCoroutine(lerp.ChangeColor(image, OnPointerColor, defaultColor, fadeTime));
    }

    // ボタンがクリックされたら
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (CheckDoubleSelect(skillNum))
        {
            SetSelectSlot(skillNum);
        }
    }

    void Start()
    {
        // GameObjectを探す
        UIFunctionController = GameObject.Find("UIFunctionController");
        Player = GameObject.Find("Player");

        // 探したGameObjectのコンポーネントを取得
        lerp = UIFunctionController.GetComponent<Lerp>();
        skillController = Player.GetComponent<SkillController>();
        skillSelectUIController = UIFunctionController.GetComponent<SkillSelectUIController>();
    }

    void Update()
    {
        // スキルの選択した順を表すテキストを設定
        SetSelectNumText();
    }

    // スキルの選択した順を表すテキストを設定
    void SetSelectNumText()
    {
        for(int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            if (skillController.selectSlot[i] == skillNum)
            {
                selectNumText.enabled = true;
                selectNumText.text = (i + 1).ToString("0");
                return;
            }
        }

        // スキルがセットされていなければテキストを非表示
        selectNumText.enabled = false;
    }

    // 選択しているスキルスロットを設定
    void SetSelectSlot(int num)
    {
        if (skillController.count >= AppConst.SKILL_SLOT_AMOUNT)
        {
            skillController.InitSelectSlot();
        }
        skillController.selectSlot[skillController.count] = num;
        ++skillController.count;
    }

    // 同じスキルを選択していないか検知
    bool CheckDoubleSelect(int num)
    {
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            if (skillController.selectSlot[i] == num) { return false; }
        }
        return true;
    }
}
