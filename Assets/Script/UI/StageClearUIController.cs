using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �X�e�[�W�N���A��ʂ�UI���Ǘ�
public class StageClearUIController : Lerp
{
    [SerializeField] private Material stageClearButtonMat;                // �{�^���̃}�e���A��
    [SerializeField] private PostProcessController postProcessController; // Inspector��PostProcessController���w��
    [SerializeField] private UIController uIController;                   // Inspector��UIController���w��
    [SerializeField] private Lerp lerp;                                   // Inspector��Lerp���w��

    public float fadeTime = 0.4f; // �t�F�[�h����

    // �X�e�[�W�N���A��ʂ�UI��`��
    public void DrawStageClearUI()
    {
        // �{�^�����\��
        for (int i = 0; i < uIController.stageClearUI.button.Length; i++)
            uIController.stageClearUI.button[i].SetActive(false);

        // �X�e�[�W�N���A��ʂ𓮂���
        StartCoroutine(MoveStageClearUI());
    }

    // �X�e�[�W�N���A��ʂ𓮂���
    IEnumerator MoveStageClearUI()
    {
        Vector3[] defaultPos = new Vector3[uIController.stageClearUI.button.Length]; // �����ʒu
        Vector3 startPos;   // �J�n�ʒu
        Vector3 endPos;     // �I���ʒu
        Color32 startColor; // �J�n���̐F
        Color32 endColor;   // �I�����̐F

        // �e�L�X�g�𓮂���
        startPos = new Vector3(300.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 0.0f, 0.0f);
        StartCoroutine(Position_Text(uIController.stageClearUI.stageClearText, startPos, endPos, fadeTime));

        // ��u�҂�
        yield return new WaitForSecondsRealtime(2.0f);

        // �e�L�X�g�𓮂���
        startPos = new Vector3(0.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 100.0f, 0.0f);
        StartCoroutine(Position_Text(uIController.stageClearUI.stageClearText, startPos, endPos, fadeTime));

        // �{�^����\��
        for (int i = 0; i < uIController.stageClearUI.button.Length; i++)
            uIController.stageClearUI.button[i].SetActive(true);

        // �{�^���̃A�j���[�V����
        for (int i = 0; i < defaultPos.Length; ++i)
        {
            defaultPos[i] = uIController.stageClearUI.button[i].transform.localPosition;
        }

        for (int i = 0; i < uIController.stageClearUI.button.Length; ++i)
        {
            // �{�^���ړ�
            startPos = defaultPos[i] + new Vector3(300.0f, 0.0f, 0.0f);
            endPos = defaultPos[i];
            StartCoroutine(Position_GameObject(uIController.stageClearUI.button[i], startPos, endPos, fadeTime));

            // �����x�ω�
            startColor = new Color32(255, 255, 255, 0);
            endColor = new Color32(255, 255, 255, 255);
            StartCoroutine(Color_Material(stageClearButtonMat, startColor, endColor, fadeTime));
        }
    }
}
