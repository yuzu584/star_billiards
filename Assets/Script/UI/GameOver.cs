using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �Q�[���I�[�o�[��ʂ��Ǘ�
public class GameOver : MonoBehaviour
{
    [SerializeField] private ScreenController screenController;           // Inspector��ScreenController���w��
    [SerializeField] private Material gameOverButtonMat;                  // �{�^���̃}�e���A��
    [SerializeField] private PostProcessController postProcessController; // Inspector��PostProcessController���w��
    [SerializeField] private UIController uIController;                   // Inspector��UIController���w��
    [SerializeField] private Lerp lerp;                                   // Inspector��Lerp���w��

    public float fadeTime = 0.2f; // �t�F�[�h����

    // �Q�[���I�[�o�[����
    public void GameOverProcess()
    {
        // �Q�[���I�[�o�[��ʂɑJ��
        screenController.screenNum = 9;
    }

    // �Q�[���I�[�o�[��ʂ̃A�j���[�V����
    void Animation()
    {
        // ��ʊE�[�x��ON
        postProcessController.DepthOfFieldOnOff(true);

        // �{�^�����\��
        for (int i = 0; i < uIController.gameOverUI.button.Length; i++)
            uIController.gameOverUI.button[i].SetActive(false);

        // UI�𓮂���
        StartCoroutine(Move());
    }

    // UI�𓮂���
    IEnumerator Move()
    {
        Vector3[] defaultPos = new Vector3[uIController.gameOverUI.button.Length]; // �����ʒu
        Vector3 startPos;   // �J�n�ʒu
        Vector3 endPos;     // �I���ʒu
        Color32 startColor; // �J�n���̐F
        Color32 endColor;   // �I�����̐F

        // �e�L�X�g�𓮂���
        startPos = new Vector3(300.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 0.0f, 0.0f);
        StartCoroutine(lerp.Position_Text(uIController.gameOverUI.GameOverText, startPos, endPos, fadeTime));

        // ��u�҂�
        yield return new WaitForSeconds(2.0f);

        // �e�L�X�g�𓮂���
        startPos = new Vector3(0.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 100.0f, 0.0f);
        StartCoroutine(lerp.Position_Text(uIController.gameOverUI.GameOverText, startPos, endPos, fadeTime));

        // �{�^����\��
        for (int i = 0; i < uIController.gameOverUI.button.Length; i++)
            uIController.gameOverUI.button[i].SetActive(true);

        // �{�^���̃A�j���[�V����
        for (int i = 0; i < defaultPos.Length; ++i)
        {
            defaultPos[i] = uIController.gameOverUI.button[i].transform.position;
        }

        for (int i = 0; i < uIController.gameOverUI.button.Length; ++i)
        {
            // �{�^���ړ�
            startPos = defaultPos[i];
            endPos = defaultPos[i];
            StartCoroutine(lerp.Position_GameObject(uIController.gameOverUI.button[i], startPos, endPos, fadeTime));

            // �����x�ω�
            startColor = new Color32(255, 255, 255, 0);
            endColor = new Color32(255, 255, 255, 255);
            StartCoroutine(lerp.Color_Material(gameOverButtonMat, startColor, endColor, fadeTime));
        }
    }

    void OnEnable()
    {
        // �A�j���[�V����
        Animation();
    }
}