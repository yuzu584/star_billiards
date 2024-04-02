using Const;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �X�e�[�W�I����ʂ�UI���Ǘ�
public class StageSelectUIController : MonoBehaviour
{
    [SerializeField] private StageData stageData;               // Inspector��StageData���w��
    [SerializeField] private GameObject stageInfoObj;           // �X�e�[�W�̏���\������I�u�W�F�N�g
    [SerializeField] private Text stageName;                    // �X�e�[�W���̃e�L�X�g
    [SerializeField] private Text missionText;                  // �X�e�[�W�̃~�b�V�������̃e�L�X�g
    [SerializeField] private Text timeLimitText;                // �X�e�[�W�̐������Ԃ̃e�L�X�g
    [SerializeField] private Text scoreText;                    // �X�e�[�W�̃X�R�A�̃e�L�X�g

    private GameObject oldButton;                               // �擾�����X�e�[�W�{�^��
    private Vector3 oldPos;                                     // �擾�����X�e�[�W�{�^���̍��W
    private StageButton oldStageBtn;                            // �擾����StageButton
    private float fadeTime = 0.4f;                              // �t�F�[�h����
    private StageButton sBtn;                                   // StageButton��������ϐ�

    private StageController stageCon;
    private Localize localize;
    private StageScore stageScore;
    private Lerp lerp;

    void Start()
    {
        stageCon ??= StageController.instance;

        stageCon.DSIdele += DrawStageInfo;
        
        localize = Localize.instance;
        stageScore = StageScore.instance;
        lerp ??= gameObject.AddComponent<Lerp>();

        // �e�L�X�g�̃t�H���g��ݒ�
        stageName.font = localize.GetFont();
        missionText.font = localize.GetFont();
    }

    private void OnDestroy()
    {
        stageCon.DSIdele -= DrawStageInfo;
    }

    // �X�e�[�W���UI��`��
    void DrawStageInfo(Vector3 pos, GameObject button, StageButton stageButton)
    {
        // �X�e�[�W�{�^���̌����ڂ��ς����Ă����猩���ڂ����ɖ߂�
        ResetDetail(false);

        sBtn = stageButton;
        oldPos = pos;
        oldButton = button;
        oldStageBtn = sBtn;

        // �A�j���[�V�������ɂ���
        sBtn.anim = true;

        // �X�e�[�W���UI�̍��W��ݒ�
        Vector3 newPos = oldPos;
        newPos.x = Mathf.Clamp(newPos.x, -270.0f, 270.0f);
        newPos.y = Mathf.Clamp(newPos.y, -50.0f, 50.0f);
        stageInfoObj.transform.localPosition = newPos;

        stageCon ??= StageController.instance;

        // �X�e�[�W����ݒ�
        stageName.text = localize.GetString_StageName((EnumStageName)Enum.ToObject(typeof(EnumStageName), stageCon.stageNum));

        // �~�b�V��������ݒ�
        missionText.text = localize.GetString_Mission((EnumMission)Enum.ToObject(typeof(EnumMission), stageData.stageList[stageCon.stageNum].missionNum));

        // �������Ԃ̃e�L�X�g��ݒ�
        timeLimitText.text = stageData.stageList[stageCon.stageNum].timeLimit.ToString() + "s";

        // �X�R�A�̃e�L�X�g��ݒ�
        if(stageScore.score[stageCon.stageNum] == 0)
            scoreText.text = "--------";
        else
            scoreText.text = stageScore.score[stageCon.stageNum].ToString();

        // �X�e�[�W�{�^���𓮂���
        StartCoroutine(lerp.Position_GameObject(oldButton, oldButton.transform.localPosition, newPos + new Vector3(-73.0f, 95.0f, 0.0f), fadeTime));
        StartCoroutine(lerp.Scale_GameObject(oldButton, oldButton.transform.localScale, new Vector2(0.8f, 0.8f), fadeTime));

        // �X�e�[�W���UI��\��
        stageInfoObj.SetActive(true);
        StartCoroutine(lerp.Scale_GameObject(stageInfoObj, new Vector2(0.0f, 0.0f), new Vector2(1.0f, 1.0f), fadeTime));
    }

    // �X�e�[�W�{�^���̌����ڂ����ɖ߂�
    void ResetDetail(bool orFast)
    {
        lerp ??= gameObject.AddComponent<Lerp>();

        if (oldButton != null)
        {
            // �����������ōs���Ȃ���`�⊮���s�킸�ɒ��ڒl��ς���
            if(orFast)
            {
                lerp.StopAll();
                oldButton.transform.localPosition = oldPos;
                oldButton.transform.localScale = new Vector2(1.0f, 1.0f);
            }
            // �����ŏ������s��Ȃ��Ȃ���`�⊮���g�p���Ēl��ς���
            else
            {
                lerp.StopAll();
                StartCoroutine(lerp.Position_GameObject(oldButton, oldButton.transform.localPosition, oldPos, fadeTime));
                StartCoroutine(lerp.Scale_GameObject(oldButton, oldButton.transform.localScale, new Vector2(1.0f, 1.0f), fadeTime));
            }

            // �{�^�����A�j���[�V�������ł͂Ȃ�����
            oldStageBtn.anim = false;
        }
    }
}
