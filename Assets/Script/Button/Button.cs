using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

// �{�^���̐e�N���X
public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
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
        // GameObject��T��
        ScreenController = GameObject.Find("ScreenController");
        UIFunctionController = GameObject.Find("UIFunctionController");

        // �T����GameObject�̃R���|�[�l���g���擾
        screenController = ScreenController.gameObject.GetComponent<ScreenController>();
        lerp = UIFunctionController.GetComponent<Lerp>();
    }
}
