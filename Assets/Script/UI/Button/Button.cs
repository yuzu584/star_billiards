using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// �{�^���̐e�N���X
public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Find�ŒT��GameObject
    [System.NonSerialized] public GameObject ScreenController;
    [System.NonSerialized] public GameObject UIFunctionController;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    [System.NonSerialized] public ScreenController screenController;
    [System.NonSerialized] public Lerp lerp;

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
    public virtual void EnterProcess()
    {
        Debug.Log("�|�C���^�[����������̏������ݒ肳��Ă��܂���B");
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    public virtual void ExitProcess()
    {
        Debug.Log("�|�C���^�[�����ꂽ���̏������ݒ肳��Ă��܂���B");
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    public virtual void ClickProcess()
    {
        Debug.Log("�N���b�N���̏������ݒ肳��Ă��܂���B");
    }

    void Start()
    {
        // GameObject��T��
        ScreenController = GameObject.Find("ScreenController");
        UIFunctionController = GameObject.Find("UIFunctionController");

        // �T����GameObject�̃R���|�[�l���g���擾
        screenController = ScreenController.gameObject.GetComponent<ScreenController>();
        lerp = UIFunctionController.GetComponent<Lerp>();
    }
}
