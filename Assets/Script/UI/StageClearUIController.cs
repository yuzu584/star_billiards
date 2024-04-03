using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �X�e�[�W�N���A��ʂ�UI���Ǘ�
public class StageClearUIController : MonoBehaviour
{
    [SerializeField] private Material stageClearButtonMat;              // �{�^���̃}�e���A��
    public float fadeTime = 0.4f;                                       // �t�F�[�h����

    [SerializeField] private StageData stageData;
    private StageScore stageScore;
    private StageController stageCon;
    private TimeLimit timeLimit;
    private Lerp lerp;

    private int score = 0;                                              // �l���X�R�A
    private int oldScore = 0;                                           // �l���X�R�A�𔽉f���Ă��Ȃ��X�R�A
    private bool newRecord = false;                                     // �n�C�X�R�A���X�V������
    private Color hideColor = new Color(1, 1, 1, 0);                    // ����

    [SerializeField] private GameObject[] btn;                          // ��ʂɑ��݂���{�^��
    [SerializeField] private Text stageClearText;                       // �X�e�[�W�N���A��ʂ̃e�L�X�g
    [SerializeField] private Text[] scoreText;                          // �X�R�A�̍��ږ��̃e�L�X�g
    [SerializeField] private Text[] scoreValueText;                     // �X�R�A�̒l�̃e�L�X�g
    [SerializeField] private Text newRecordText;                        // �n�C�X�R�A�X�V�����Ƃ��̃e�L�X�g


    // �X�e�[�W�N���A��ʂ�UI��`��
    void DrawStageClearUI()
    {
        // �{�^�����\��
        for (int i = 0; i < btn.Length; i++)
            btn[i].SetActive(false);

        // �n�C�X�R�A�X�V�����Ƃ��̃e�L�X�g���\��
        newRecordText.enabled = false;

        // �l���X�R�A���v�Z
        score = (int)((timeLimit.time / stageData.stageList[stageCon.stageNum].timeLimit) * 100000);

        // �X�R�A��ݒ�
        SetScore(score);

        // �X�R�A��UI���\��
        ScoreUIAnim(false);

        // �X�e�[�W�N���A��ʂ𓮂���
        StartCoroutine(MoveStageClearUI());
    }

    // �X�e�[�W�N���A��ʂ𓮂���
    private IEnumerator MoveStageClearUI()
    {
        Vector3[] defaultPos = new Vector3[btn.Length]; // �����ʒu
        Vector3 startPos;   // �J�n�ʒu
        Vector3 endPos;     // �I���ʒu
        Color32 startColor; // �J�n���̐F
        Color32 endColor;   // �I�����̐F

        // �e�L�X�g�𓮂���
        startPos = new Vector3(300.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 0.0f, 0.0f);
        StartCoroutine(lerp.Position_Text(stageClearText, startPos, endPos, fadeTime));

        // ��u�҂�
        yield return new WaitForSecondsRealtime(1.0f);

        // �e�L�X�g�𓮂���
        startPos = new Vector3(0.0f, 0.0f, 0.0f);
        endPos = new Vector3(0.0f, 100.0f, 0.0f);
        StartCoroutine(lerp.Position_Text(stageClearText, startPos, endPos, fadeTime));

        // �X�R�A��UI��\�����ăA�j���[�V����
        ScoreUIAnim(true);

        // ��u�҂�
        yield return new WaitForSecondsRealtime(1.0f);

        // �{�^���̐��J��Ԃ�
        for (int i = 0; i < btn.Length; ++i)
        {
            // �{�^����\��
            btn[i].SetActive(true);

            // �����ʒu����
            defaultPos[i] = btn[i].transform.localPosition;

            // �{�^���ړ�
            startPos = defaultPos[i] + new Vector3(300.0f, 0.0f, 0.0f);
            endPos = defaultPos[i];
            StartCoroutine(lerp.Position_GameObject(btn[i], startPos, endPos, fadeTime));

            // �����x�ω�
            startColor = new Color32(255, 255, 255, 0);
            endColor = new Color32(255, 255, 255, 255);
            StartCoroutine(lerp.Color_Material(stageClearButtonMat, startColor, endColor, fadeTime));
        }
    }

    // �X�R�A��UI�̃A�j���[�V����
    void ScoreUIAnim(bool orDraw)
    {
        // �X�R�A��UI��\��/��\��
        for (int i = 0; i < scoreText.Length; i++)
            scoreText[i].enabled = orDraw;

        for (int i = 0; i < scoreValueText.Length; i++)
            scoreValueText[i].enabled = orDraw;

        // �`�悷��Ȃ�
        if (orDraw)
        {

            // �X�R�A�̃e�L�X�g��ݒ�
            scoreValueText[0].text = score.ToString();

            // �n�C�X�R�A�̃e�L�X�g��ݒ�
            if (oldScore == 0)
                scoreValueText[1].text = "--------";
            else
                scoreValueText[1].text = oldScore.ToString();

            // �X�R�A��UI�̐F����`�⊮�ŕύX
            for (int i = 0; i < scoreText.Length; i++)
                StartCoroutine(lerp.Color_Text(scoreText[i], hideColor, new Color(1, 1, 1, 1), fadeTime));

            for (int i = 0; i < scoreValueText.Length; i++)
                StartCoroutine(lerp.Color_Text(scoreValueText[i], hideColor, new Color(1, 1, 1, 1), fadeTime));
        }

        // �n�C�X�R�A���X�V�������`�悷��Ȃ�
        if ((newRecord) && (orDraw))
        {
            // �n�C�X�R�A�X�V�����Ƃ��̃e�L�X�g��\��
            newRecordText.enabled = true;
            StartCoroutine(lerp.Color_Text(newRecordText, hideColor, new Color(1, 1, 1, 1), fadeTime));
        }
    }

    // �X�R�A��ݒ�
    void SetScore(int s)
    {
        oldScore = stageScore.score[stageCon.stageNum];

        // �l���X�R�A�����̃X�e�[�W�̃n�C�X�R�A���X�V���Ă�����
        if (oldScore < s)
        {
            // �n�C�X�R�A���㏑��
            stageScore.score[stageCon.stageNum] = s;

            newRecord = true;
        }
    }

    private void Start()
    {
        lerp = gameObject.AddComponent<Lerp>();
        stageScore = StageScore.instance;
        stageCon = StageController.instance;
        timeLimit = TimeLimit.instance;

        DrawStageClearUI();
    }
}
