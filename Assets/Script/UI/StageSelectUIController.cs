using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �X�e�[�W�I����ʂ�UI���Ǘ�
public class StageSelectUIController : Singleton<StageSelectUIController>
{
    [SerializeField] private StageData stageData;               // Inspector��StageData���w��

    private StageController stageCon;
    private ScreenController scrCon;
    private UIController uICon;

    private GameObject oldButton;                               // �擾�����X�e�[�W�{�^��
    private Vector3 oldPos;                                     // �擾�����X�e�[�W�{�^���̍��W
    private StageButton oldStageBtn;                            // �擾����StageButton
    private float fadeTime = 0.4f;                              // �t�F�[�h����
    private StageButton sBtn;                                   // StageButton��������ϐ�
    private Lerp lerp;

    void Start()
    {
        stageCon = StageController.instance;
        scrCon = ScreenController.instance;
        uICon ??= UIController.instance;
        
        lerp = gameObject.AddComponent<Lerp>();
    }

    void Update()
    {
        if (sBtn != null)
        {
            if ((sBtn.anim) && (scrCon.ScreenLoot == 0))
                HideStageInfo(false);
        }
    }

    // �X�e�[�W���UI��`��
    public void DrawStageInfo(Vector3 pos, GameObject button, StageButton stageButton)
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
        uICon.stageSelectUI.stageInfoUI.transform.localPosition = newPos;

        // �X�e�[�W����ݒ�
        uICon.stageSelectUI.name.text = stageData.stageList[stageCon.stageNum].stageName;

        // �~�b�V��������ݒ�
        switch (stageData.stageList[stageCon.stageNum].missionNum)
        {
            case 0: // �S�Ă̘f����j��
                uICon.stageSelectUI.mission.text = "Destroy all planets";
                break;
            case 1: // ���ԓ��ɃS�[���ɂ��ǂ蒅��
                uICon.stageSelectUI.mission.text = "Reach the goal";
                break;
        }

        // �X�e�[�W�{�^���𓮂���
        StartCoroutine(lerp.Position_GameObject(oldButton, oldButton.transform.localPosition, newPos + new Vector3(-85.0f, 20.0f, 0.0f), fadeTime));
        StartCoroutine(lerp.Scale_GameObject(oldButton, oldButton.transform.localScale, new Vector2(0.4f, 0.4f), fadeTime));

        // �X�e�[�W���UI��\��
        uICon.stageSelectUI.stageInfoUI.SetActive(true);
        StartCoroutine(lerp.Scale_GameObject(uICon.stageSelectUI.stageInfoUI, new Vector2(0.0f, 0.0f), new Vector2(1.0f, 1.0f), fadeTime));
    }

    // �X�e�[�W�{�^���̌����ڂ����ɖ߂�
    void ResetDetail(bool orFast)
    {
        if (oldButton != null)
        {
            // �����������ōs���Ȃ���`�⊮���s�킸�ɒ��ڒl��ς���
            if(orFast)
            {
                oldButton.transform.localPosition = oldPos;
                oldButton.transform.localScale = new Vector2(1.0f, 1.0f);
            }
            // �����ŏ������s��Ȃ��Ȃ���`�⊮���g�p���Ēl��ς���
            else
            {
                StartCoroutine(lerp.Position_GameObject(oldButton, oldButton.transform.localPosition, oldPos, fadeTime));
                StartCoroutine(lerp.Scale_GameObject(oldButton, oldButton.transform.localScale, new Vector2(1.0f, 1.0f), fadeTime));
            }

            // �{�^�����A�j���[�V�������ł͂Ȃ�����
            oldStageBtn.anim = false;
        }
    }

    // �X�e�[�W���UI���\��
    public void HideStageInfo(bool orFast)
    {
        // �X�e�[�W���UI���\��
        uICon.stageSelectUI.stageInfoUI.SetActive(false);

        // �X�e�[�W�{�^�������݂��Ă����猩���ڂ����ɖ߂�
        ResetDetail(orFast);
    }

    void OnEnable()
    {
        uICon ??= UIController.instance;

        // �X�e�[�W���UI���\��
        HideStageInfo(true);
    }
}
