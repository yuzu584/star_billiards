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
    [SerializeField] private Text missionName;                  // �X�e�[�W�̃~�b�V�������̃e�L�X�g

    private StageController stageCon;

    private GameObject oldButton;                               // �擾�����X�e�[�W�{�^��
    private Vector3 oldPos;                                     // �擾�����X�e�[�W�{�^���̍��W
    private StageButton oldStageBtn;                            // �擾����StageButton
    private float fadeTime = 0.4f;                              // �t�F�[�h����
    private StageButton sBtn;                                   // StageButton��������ϐ�

    private Localize localize;
    private Lerp lerp;

    void Start()
    {
        stageCon ??= StageController.instance;

        stageCon.DSIdele += DrawStageInfo;
        
        localize = Localize.instance;
        lerp ??= gameObject.AddComponent<Lerp>();

        // �e�L�X�g�̃t�H���g��ݒ�
        stageName.font = localize.GetFont();
        missionName.font = localize.GetFont();
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
        newPos.x = Mathf.Clamp(newPos.x, -200.0f, 200.0f);
        newPos.y = Mathf.Clamp(newPos.y, -100.0f, 100.0f);
        stageInfoObj.transform.localPosition = newPos;

        stageCon ??= StageController.instance;

        // �X�e�[�W����ݒ�
        switch (stageCon.stageNum)
        {
            case 0: stageName.text = localize.GetString(StringGroup.StageName, StringType.Stage1); break;
            case 1: stageName.text = localize.GetString(StringGroup.StageName, StringType.Stage2); break;
            case 2: stageName.text = localize.GetString(StringGroup.StageName, StringType.Stage3); break;
            case 3: stageName.text = localize.GetString(StringGroup.StageName, StringType.Stage4); break;
            case 4: stageName.text = localize.GetString(StringGroup.StageName, StringType.Stage5); break;
            default:break;
        }

        // �~�b�V��������ݒ�
        switch (stageData.stageList[stageCon.stageNum].missionNum)
        {
            case 0: // �f����j�󂵂�
                missionName.text = localize.GetString(StringGroup.Mission, StringType.DestroyPlanet);
                break;
            case 1: // �S�[���ɂ��ǂ蒅��
                missionName.text = localize.GetString(StringGroup.Mission, StringType.ReachTheGoal);
                break;
        }

        // �X�e�[�W�{�^���𓮂���
        StartCoroutine(lerp.Position_GameObject(oldButton, oldButton.transform.localPosition, newPos + new Vector3(-85.0f, 20.0f, 0.0f), fadeTime));
        StartCoroutine(lerp.Scale_GameObject(oldButton, oldButton.transform.localScale, new Vector2(0.4f, 0.4f), fadeTime));

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
