using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using Const;

// �X�L���X���b�g���Ǘ�
public class SkillSlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image image;          // �X�L���X���b�g�̉摜
    [SerializeField] private Image imageOutline;   // �X�L���X���b�g�̘g�摜
    [SerializeField] private Text selectNumText;   // �X�L���̑I����������\���e�L�X�g
    [SerializeField] private Color OnPointerColor; // �|�C���^�[����������̐F
    [SerializeField] private Color defaultColor;   // �f�t�H���g�̐F
    [SerializeField] private float fadeTime;       // �t�F�[�h����

    public int skillNum; // �X�L���ԍ�

    // Find�ŒT������
    private GameObject UIFunctionController;
    private GameObject Player;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    private Lerp lerp;
    private SkillController skillController;
    private SkillSelectUIController skillSelectUIController;

    // �}�E�X�|�C���^�[���{�^���̏�ɏ������
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �{�^���̃A�j���[�V����
        StopAllCoroutines();
        StartCoroutine(lerp.ChangeColor(image, defaultColor, OnPointerColor, fadeTime));

        // �X�L���̏���\������UI���X�V
        skillSelectUIController.DrawSkillInfo(skillNum);
    }

    // �}�E�X�|�C���^�[���{�^���̏ォ�痣�ꂽ��
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // �{�^���̃A�j���[�V����
        StopAllCoroutines();
        StartCoroutine(lerp.ChangeColor(image, OnPointerColor, defaultColor, fadeTime));
    }

    // �{�^�����N���b�N���ꂽ��
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (CheckDoubleSelect(skillNum))
        {
            SetSelectSlot(skillNum);
        }
    }

    void Start()
    {
        // GameObject��T��
        UIFunctionController = GameObject.Find("UIFunctionController");
        Player = GameObject.Find("Player");

        // �T����GameObject�̃R���|�[�l���g���擾
        lerp = UIFunctionController.GetComponent<Lerp>();
        skillController = Player.GetComponent<SkillController>();
        skillSelectUIController = UIFunctionController.GetComponent<SkillSelectUIController>();
    }

    void Update()
    {
        // �X�L���̑I����������\���e�L�X�g��ݒ�
        SetSelectNumText();
    }

    // �X�L���̑I����������\���e�L�X�g��ݒ�
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

        // �X�L�����Z�b�g����Ă��Ȃ���΃e�L�X�g���\��
        selectNumText.enabled = false;
    }

    // �I�����Ă���X�L���X���b�g��ݒ�
    void SetSelectSlot(int num)
    {
        if (skillController.count >= AppConst.SKILL_SLOT_AMOUNT)
        {
            skillController.InitSelectSlot();
        }
        skillController.selectSlot[skillController.count] = num;
        ++skillController.count;
    }

    // �����X�L����I�����Ă��Ȃ������m
    bool CheckDoubleSelect(int num)
    {
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            if (skillController.selectSlot[i] == num) { return false; }
        }
        return true;
    }
}
