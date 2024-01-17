using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// �X�L���X���b�g���Ǘ�
public class SkillSlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image image;          // �X�L���X���b�g�̉摜
    [SerializeField] private Image imageOutline;   // �X�L���X���b�g�̘g�摜
    [SerializeField] private Color OnPointerColor; // �|�C���^�[����������̐F
    [SerializeField] private Color defaultColor;   // �f�t�H���g�̐F
    [SerializeField] private float fadeTime;       // �t�F�[�h����

    // Find�ŒT������
    private GameObject UIFunctionController;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    private Lerp lerp;

    // �}�E�X�|�C���^�[���{�^���̏�ɏ������
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // �{�^���̃A�j���[�V����
        StopAllCoroutines();
        StartCoroutine(lerp.ChangeColor(image, defaultColor, OnPointerColor, fadeTime));
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

    }

    void Start()
    {
        // GameObject��T��
        UIFunctionController = GameObject.Find("UIFunctionController");

        // �T����GameObject�̃R���|�[�l���g���擾
        lerp = UIFunctionController.GetComponent<Lerp>();
    }
}
