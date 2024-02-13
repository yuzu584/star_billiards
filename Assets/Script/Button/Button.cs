using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// �{�^���̐e�N���X
public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [System.Serializable]
    protected struct ImageStruct   // �摜�̍\����
    {
        public Image image;      // �摜
        public Color startColor; // �ω��O�̐F
        public Color endColor;   // �ω���̐F
        public float fadeTime;   // �t�F�[�h����
    }

    [System.Serializable]
    protected struct TextStruct    // �e�L�X�g�̍\����
    {
        public Text text;        // �e�L�X�g
        public Color startColor; // �ω��O�̐F
        public Color endColor;   // �ω���̐F
        public float fadeTime;   // �t�F�[�h����
    }

    [SerializeField] protected ImageStruct[] imageStructs;
    [SerializeField] protected TextStruct[] textStructs;

    // Find�ŒT��GameObject
    protected GameObject ScreenController;
    protected GameObject UIFunctionController;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    protected ScreenController screenController;
    protected Lerp lerp;

    // �}�E�X�|�C���^�[���{�^���̏�ɏ������
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        EnterProcess();
    }

    // �}�E�X�|�C���^�[���{�^���̏ォ�痣�ꂽ��
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        ExitProcess();
    }

    // �{�^�����N���b�N���ꂽ��
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        ClickProcess();
    }

    // �}�E�X�|�C���^�[����������̏���
    protected virtual void EnterProcess()
    {
        Debug.Log("�|�C���^�[����������̏������ݒ肳��Ă��܂���B");
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    protected virtual void ExitProcess()
    {
        Debug.Log("�|�C���^�[�����ꂽ���̏������ݒ肳��Ă��܂���B");
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    protected virtual void ClickProcess()
    {
        Debug.Log("�N���b�N���̏������ݒ肳��Ă��܂���B");
    }

    protected void Start()
    {
        // �I�u�W�F�N�g��T���ăR���|�[�l���g���擾
        ScreenController = GameObject.Find("ScreenController");
        UIFunctionController = GameObject.Find("UIFunctionController");
        screenController = ScreenController.gameObject.GetComponent<ScreenController>();
        lerp = UIFunctionController.GetComponent<Lerp>();
    }
}
