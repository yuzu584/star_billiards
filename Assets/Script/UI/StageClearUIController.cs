using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �X�e�[�W�N���A��ʂ�UI���Ǘ�
public class StageClearUIController : Singleton<StageClearUIController>
{
    [SerializeField] private Material stageClearButtonMat;              // �{�^���̃}�e���A��
    public float fadeTime = 0.4f;                                       // �t�F�[�h����

    private UIController uICon;
    private ScreenController scrCon;
    private Lerp lerp;

    private int buttonNum;                                              // �{�^���̐�


    // �X�e�[�W�N���A��ʂ�UI��`��
    void DrawStageClearUI()
    {
        // �{�^�����\��
        for (int i = 0; i < buttonNum; i++)
            uICon.stageClearUI.button[i].SetActive(false);

        // �X�e�[�W�N���A��ʂ𓮂���
        StartCoroutine(MoveStageClearUI());
    }

    // �X�e�[�W�N���A��ʂ𓮂���
    IEnumerator MoveStageClearUI()
    {
        Vector3[] defaultPos = new Vector3[buttonNum]; // �����ʒu
        Vector3 startPos;   // �J�n�ʒu
        Vector3 endPos;     // �I���ʒu
        Color32 startColor; // �J�n���̐F
        Color32 endColor;   // �I�����̐F

        // �e�L�X�g�𓮂���
        startPos = new Vector3(300.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 0.0f, 0.0f);
        StartCoroutine(lerp.Position_Text(uICon.stageClearUI.stageClearText, startPos, endPos, fadeTime));

        // ��u�҂�
        yield return new WaitForSecondsRealtime(2.0f);

        // �e�L�X�g�𓮂���
        startPos = new Vector3(0.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 100.0f, 0.0f);
        StartCoroutine(lerp.Position_Text(uICon.stageClearUI.stageClearText, startPos, endPos, fadeTime));

        for (int i = 0; i < buttonNum; ++i)
        {
            // �{�^����\��
            uICon.stageClearUI.button[i].SetActive(true);

            // �����ʒu����
            defaultPos[i] = uICon.stageClearUI.button[i].transform.localPosition;

            // �{�^���ړ�
            startPos = defaultPos[i] + new Vector3(300.0f, 0.0f, 0.0f);
            endPos = defaultPos[i];
            StartCoroutine(lerp.Position_GameObject(uICon.stageClearUI.button[i], startPos, endPos, fadeTime));

            // �����x�ω�
            startColor = new Color32(255, 255, 255, 0);
            endColor = new Color32(255, 255, 255, 255);
            StartCoroutine(lerp.Color_Material(stageClearButtonMat, startColor, endColor, fadeTime));
        }
    }

    private void Start()
    {
        lerp = gameObject.AddComponent<Lerp>();
        uICon = UIController.instance;
        scrCon = ScreenController.instance;

        // �X�e�[�W�N���A��ʂɑJ�ڂ����Ȃ�X�e�[�W�N���A��ʂ̕`�揈�����s��
        scrCon.changeScreen += () =>
        {
            if (scrCon.ScreenNum == 8)
            {
                DrawStageClearUI();
            }
        };

        buttonNum = uICon.stageClearUI.button.Length;
    }
}
