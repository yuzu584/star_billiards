using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �Q�[����ʂ̃|�b�v�A�b�v1
public class InGamePopup1 : PopupParent
{
    protected override void Start()
    {
        base.Start();

        scrCon.changeScreen += () =>
        {
            // �Q�[����ʂȂ�\���A����ȊO�Ȃ��\��
            for (int i = 0; i < popupMana.popupContent.Length; i++)
            {
                // �C���X�^���X����������Ă����
                if (popupMana.popupContent[(int)popupType].instance[i] != null)
                    popupMana.popupContent[(int)popupType].instance[i].SetActive(scrCon.Screen == ScreenController.ScreenType.InGame);
            }
        };
    }

    private void OnDestroy()
    {
        scrCon.changeScreen -= () =>
        {
            // �Q�[����ʂȂ�\���A����ȊO�Ȃ��\��
            for (int i = 0; i < popupMana.popupContent.Length; i++)
            {
                // �C���X�^���X����������Ă����
                if (popupMana.popupContent[(int)popupType].instance[i] != null)
                    popupMana.popupContent[(int)popupType].instance[i].SetActive(scrCon.Screen == ScreenController.ScreenType.InGame);
            }
        };
    }

    // �|�b�v�A�b�v�̏���
    public override IEnumerator Process(string text, Transform parentT, int num)
    {
        float destroyTime = 10.0f;   // �f����j�󂷂�܂ł̎���
        float fadeTime = 1.0f;       // �t�F�[�h����
        float moveDistance = 300.0f; // �ړ�����
        Vector3 defaultPosition;     // �f�t�H���g�̈ʒu

        index = num;

        popupMana ??= PopupManager.instance;

        // �e��ݒ�
        popupMana.popupContent[(int)popupType].instance[index].transform.SetParent(parentT, false);

        // �ʒu��ݒ�
        popupMana.popupContent[(int)popupType].instance[index].transform.localPosition += new Vector3(-moveDistance, index * -20.0f, 0.0f);

        // �v���n�u�̃e�L�X�g���擾
        Text popupText = popupMana.popupContent[(int)popupType].instance[index].transform.GetChild(1).GetComponent<Text>();

        // �v���n�u�̃e�L�X�g��ݒ�
        popupText.text = text;

        // �f�t�H���g�ʒu��ݒ�
        defaultPosition = popupMana.popupContent[(int)popupType].instance[index].transform.localPosition;

        lerp ??= gameObject.AddComponent<Lerp>();

        // �|�b�v�A�b�v�𓮂���
        yield return StartCoroutine(lerp.Position_GameObject(popupMana.popupContent[(int)popupType].instance[index], defaultPosition, defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), fadeTime));

        // �|�b�v�A�b�v�����Ԃ��o�߂���܂ő҂�
        yield return new WaitForSeconds(destroyTime);

        // �|�b�v�A�b�v�𓮂���
        if (popupMana.popupContent[(int)popupType].instance[index] != null)
        yield return StartCoroutine(lerp.Position_GameObject(popupMana.popupContent[(int)popupType].instance[index], defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), defaultPosition, fadeTime));

        // �|�b�v�A�b�v���폜
        Destroy();
    }
}
