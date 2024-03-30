using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �|�b�v�A�b�v�̐e�N���X
public class PopupParent : MonoBehaviour
{
    [SerializeField] protected PopupManager.PopupType popupType;    // �|�b�v�A�b�v�̎��

    protected PopupManager popupMana;
    protected ScreenController scrCon;
    protected InputController input;
    protected Lerp lerp;
    protected Localize localize;

    protected int index = -1;                                       // PopupManager ���Ǘ�����|�b�v�A�b�v�̔z��̉��Ԗڂ̃|�b�v�A�b�v��
    [SerializeField] protected bool onChangeScreenDestroy = false;  // ��ʑJ�ڎ��Ƀ|�b�v�A�b�v���폜���邩
    [SerializeField] protected bool onChangeLootDestroy = false;    // �K�w�J�ڎ��Ƀ|�b�v�A�b�v���폜���邩
    [SerializeField] protected Text popupText;                      // �|�b�v�A�b�v�̃e�L�X�g

    protected virtual void Start()
    {
        popupMana = PopupManager.instance;
        scrCon = ScreenController.instance;
        input = InputController.instance;
        localize = Localize.instance;

        // Lerp ���A�^�b�`
        lerp ??= gameObject.AddComponent<Lerp>();

        // �t�H���g��ݒ�
        popupText.font = localize.GetFont();

        // ��ʑJ�ڎ��Ƀ|�b�v�A�b�v���폜
        if (onChangeScreenDestroy)
            scrCon.changeScreen += Destroy;

        // �K�w�J�ڎ��Ƀ|�b�v�A�b�v���폜
        if (onChangeLootDestroy)
            scrCon.changeLoot += Destroy;
    }

    protected virtual void OnDestroy()
    {
        if (onChangeScreenDestroy)
            scrCon.changeScreen -= Destroy;

        if (onChangeLootDestroy)
            scrCon.changeLoot -= Destroy;
    }

    // �|�b�v�A�b�v�̏���
    public virtual IEnumerator Process(string text, Transform parentT, int num)
    {
        Debug.Log("�|�b�v�A�b�v�̏������ݒ肳��Ă��܂���");
        yield return null;
    }

    // �|�b�v�A�b�v���폜
    public void Destroy()
    {
        // �C���X�^���X�����݂���΍폜
        if (popupMana.popupContent[(int)popupType].instance[index] != null)
            Destroy(popupMana.popupContent[(int)popupType].instance[index]);

        // �C���X�^���X���i�[����z��� null ����
        popupMana.popupContent[(int)popupType].instance[index] = null;

        // �擾���� PopupParent �N���X���i�[����z��� null ����
        popupMana.popupContent[(int)popupType].component[index] = null;

        // �R���[�`�������݂���Β�~����
        if (popupMana.popupContent[(int)popupType].coroutines[index] != null)
            popupMana.StopCoroutineOfPopupContent(popupMana.popupContent[(int)popupType], index);

        // �R���[�`�����i�[����z��� null ��������
        popupMana.popupContent[(int)popupType].coroutines[index] = null;
    }
}
