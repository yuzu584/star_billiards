using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �X�e�[�W�N���A��ʂ�UI���Ǘ�
public class StageClearUIController : MonoBehaviour
{
    [SerializeField] private Material stageClearButtonMat;                // �{�^���̃}�e���A��
    [SerializeField] private PostProcessController postProcessController; // Inspector��PostProcessController���w��
    [SerializeField] private UIController uIController;                   // Inspector��UIController���w��

    // �X�e�[�W�N���A��ʂ�UI��`��
    public void DrawStageClearUI(bool draw, GameObject allStageClearUI, GameObject[] button, Text stageClearText)
    {
        // �X�e�[�W�N���A��ʂ�\��/��\��
        allStageClearUI.SetActive(draw);

        // �Q�[����ʂ�UI��\��/��\��
        uIController.inGameUI.allInGameUI.SetActive(!draw);

        // ��ʊE�[�x��ON/OFF
        postProcessController.DepthOfFieldOnOff(draw);

        // �{�^�����\��
        for (int i = 0; i < button.Length; i++)
            button[i].SetActive(false);

        // �X�e�[�W�N���A��ʂ𓮂���
        StartCoroutine(MoveStageClearUI(
            stageClearText,
            button));
    }

    // �X�e�[�W�N���A��ʂ𓮂���
    IEnumerator MoveStageClearUI(
        Text stageClearText,
        GameObject[] button)
    {
        // �e�L�X�g�𓮂���
        StartCoroutine(MoveStageClearText(
            stageClearText,
            new Vector3(300.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 0.0f, 0.0f),
            0.2f,
            true));

        // ��u�҂�
        yield return new WaitForSeconds(2.0f);

        // �e�L�X�g�𓮂���
        StartCoroutine(MoveStageClearText(
            stageClearText,
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 100.0f, 0.0f),
            0.3f,
            false));

        // �{�^���𓮂���
        StartCoroutine(MoveStageClearButton(
            button,
            0.3f));
    }

    // �X�e�[�W�N���A��ʂ̃e�L�X�g�𓮂���
    IEnumerator MoveStageClearText(
        Text stageClearText,
        Vector3 startPos,
        Vector3 endPos,
        float fadeTime,
        bool fadeColor)
    {
        float time = 0;        // �o�ߎ���

        // �w�肵�����Ԃ��o�߂���܂ŌJ��Ԃ�
        while (time < fadeTime)
        {
            // ���Ԃ��J�E���g
            time += Time.unscaledDeltaTime;

            // �i�݋���v�Z
            float t = time / fadeTime;

            // �e�L�X�g�ړ�
            stageClearText.transform.localPosition = Vector3.Lerp(startPos, endPos, t);

            // �J���[�t�F�[�h���L���Ȃ瓧���x�ω�
            if ( fadeColor )
                stageClearText.color = Color32.Lerp(new Color32(255, 255, 255, 0), new Color32(255, 255, 255, 255), t);

            // 1�t���[���҂�
            yield return null;
        }
    }

    // �X�e�[�W�N���A��ʂ̃{�^���𓮂���
    IEnumerator MoveStageClearButton(
        GameObject[] button,
        float fadeTime)
    {
        float time = 0;        // �o�ߎ���

        // �����ʒu��ۑ�
        Vector3[] defaultPosition = new Vector3[button.Length];
        for (int i = 0; i < button.Length; i++)
            defaultPosition[i] = button[i].transform.position;

        // �{�^����\��
        for (int i = 0; i < button.Length; i++)
            button[i].SetActive(true);

        // �w�肵�����Ԃ��o�߂���܂ŌJ��Ԃ�
        while (time < fadeTime)
        {
            // ���Ԃ��J�E���g
            time += Time.unscaledDeltaTime;

            // �i�݋���v�Z
            float t = time / fadeTime;

            // �{�^���ړ��E�����x�ω�
            for (int i = 0; i < button.Length; i++)
            {
                button[i].transform.position = Vector3.Lerp(defaultPosition[i] + new Vector3(300.0f, 0.0f, 0.0f), defaultPosition[i], t);
                stageClearButtonMat.color = Color32.Lerp(new Color32(255, 255, 255, 0), new Color32(255, 255, 255, 255), t);
            }

            // 1�t���[���҂�
            yield return null;
        }
    }
}