using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �Q�[���I�[�o�[��ʂ��Ǘ�
public class GameOver : Singleton<GameOver>
{
    [SerializeField] private ScreenController screenController;           // Inspector��ScreenController���w��
    [SerializeField] private Material gameOverButtonMat;                  // �{�^���̃}�e���A��
    [SerializeField] private PostProcessController postProcessController; // Inspector��PostProcessController���w��
    [SerializeField] private UIController uIController;                   // Inspector��UIController���w��

    private Lerp lerp;

    public float fadeTime = 0.4f; // �t�F�[�h����

    // �Q�[���I�[�o�[����
    public void GameOverProcess()
    {
        // �Q�[���I�[�o�[��ʂɑJ��
        screenController.ScreenNum = 9;
    }

    // �Q�[���I�[�o�[��ʂ̃A�j���[�V����
    void Animation()
    {
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
        yield return new WaitForSecondsRealtime(2.0f);

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
            defaultPos[i] = uIController.gameOverUI.button[i].transform.localPosition;
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
        if (lerp == null)
            lerp = gameObject.AddComponent<Lerp>();

        // �A�j���[�V����
        Animation();
    }
}
