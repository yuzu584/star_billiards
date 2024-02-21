using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �X�e�[�W�I����ʂ�UI���Ǘ�
public class StageSelectUIController : Lerp
{
    [SerializeField] private StageData stageData;             // Inspector��StageData���w��
    [SerializeField] private StageController stageController; // Inspector��StageController���w��
    [SerializeField] private UIController uIController;       // Inspector��UIController���w��

    [SerializeField] private float fadeTime; // �t�F�[�h����
    private bool buttonMoved = false;        // �{�^�����ړ��ς݂�

    // �X�e�[�W���UI��`��
    public void DrawStageInfo()
    {
        // �X�e�[�W���UI��\��
        uIController.stageSelectUI.stageInfoUI.SetActive(true);

        // �X�e�[�W����ݒ�
        uIController.stageSelectUI.name.text = stageData.stageList[stageController.stageNum].stageName;

        // �~�b�V��������ݒ�
        switch (stageData.stageList[stageController.stageNum].missionNum)
        {
            case 0: // �S�Ă̘f����j��
                uIController.stageSelectUI.mission.text = "Destroy all planets";
                break;
            case 1: // ���ԓ��ɃS�[���ɂ��ǂ蒅��
                uIController.stageSelectUI.mission.text = "Reach the goal";
                break;
        }

        // �ړ��ς݂łȂ���΃{�^�����ړ�
        if (!buttonMoved)
        {
            buttonMoved = true;
            StartCoroutine(Position_GameObject(uIController.stageSelectUI.buttons, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(200.0f, 0.0f, 0.0f), fadeTime));
        }
    }

    // �X�e�[�W���UI���\��
    public void HideStageInfo()
    {
        // �ړ��ς݂Ȃ�{�^����߂�
        if (buttonMoved)
        {
            buttonMoved = false;
            StartCoroutine(Position_GameObject(uIController.stageSelectUI.buttons, new Vector3(200.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), fadeTime));
        }

        // �X�e�[�W���UI���\��
        uIController.stageSelectUI.stageInfoUI.SetActive(false);
    }

    void OnEnable()
    {
        // �X�e�[�W���UI���\��
        HideStageInfo();
    }
}
