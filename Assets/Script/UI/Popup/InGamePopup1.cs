using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �Q�[����ʂ̃|�b�v�A�b�v1
public class InGamePopup1 : PopupParent
{
    private ScreenController scrCon;
    private PopupManager popupMana;

    protected override void Start()
    {
        scrCon = ScreenController.instance;
        popupMana ??= PopupManager.instance;
    }

    void Update()
    {
        // �|�b�v�A�b�v�̌��J��Ԃ�
        for (int i = 0; i < popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance.Length; i++)
        {
            // �C���X�^���X����������Ă����
            if (popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[i] != null)
            {
                // �Q�[����ʂ���\���Ȃ�
                if ((scrCon.Screen == ScreenController.ScreenType.InGame) && (!popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[i].activeSelf))

                    // �\������
                    popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[i].SetActive(true);

                // �Q�[����ʈȊO���\������Ă���Ȃ�
                else if ((scrCon.Screen != ScreenController.ScreenType.InGame) && (popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[i].activeSelf))

                    // ��\���ɂ���
                    popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[i].SetActive(false);
            }
        }
    }

    // �|�b�v�A�b�v�̏���
    public override IEnumerator Process(string text, Transform parentT, int num)
    {
        float destroyTime = 10.0f;   // �f����j�󂷂�܂ł̎���
        float fadeTime = 1.0f;       // �t�F�[�h����
        float moveDistance = 300.0f; // �ړ�����
        Vector3 defaultPosition;     // �f�t�H���g�̈ʒu

        popupMana ??= PopupManager.instance;

        // �e��ݒ�
        popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num].transform.SetParent(parentT, false);

        // �ʒu��ݒ�
        popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num].transform.localPosition += new Vector3(-moveDistance, num * -20.0f, 0.0f);

        // �v���n�u�̃e�L�X�g���擾
        Text popupText = popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num].transform.GetChild(1).GetComponent<Text>();

        // �v���n�u�̃e�L�X�g��ݒ�
        popupText.text = text;

        // �f�t�H���g�ʒu��ݒ�
        defaultPosition = popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num].transform.localPosition;

        lerp ??= gameObject.AddComponent<Lerp>();

        // �|�b�v�A�b�v�𓮂���
        yield return StartCoroutine(lerp.Position_GameObject(popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num], defaultPosition, defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), fadeTime));

        // �|�b�v�A�b�v�����Ԃ��o�߂���܂ő҂�
        yield return new WaitForSeconds(destroyTime);

        // �|�b�v�A�b�v�𓮂���
        if (popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num] != null)
        yield return StartCoroutine(lerp.Position_GameObject(popupMana.popupContent[(int)PopupManager.PopupType.InGamePopup1].instance[num], defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), defaultPosition, fadeTime));

        // �|�b�v�A�b�v���폜
        if(gameObject)
            Destroy(gameObject);
    }
}
