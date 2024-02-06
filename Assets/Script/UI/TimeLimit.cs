using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������Ԃ��Ǘ�
public class TimeLimit : MonoBehaviour
{
    [SerializeField] private StageData stageData;             // Inspector��StageData���w��
    [SerializeField] private StageController stageController; // Inspector��StageController���w��
    [SerializeField] private UIController uIController;       // Inspector��StageController���w��
    [SerializeField] private GameOver gameOver;               // Inspector��GameOver���w��
    [SerializeField] private Initialize initialize;           // Inspector��Initialize���w��

    private float time = 0;         // ��������
    private Vector2 imageSize;      // �Q�[�W�̉摜�̃T�C�Y�̏����l

    // �������Ԃ�ݒ�
    void SetTimeLimit()
    {
        if (time != stageData.stageList[stageController.stageNum].timeLimit)
            time = stageData.stageList[stageController.stageNum].timeLimit;
    }

    // �������Ԃ�UI��`��
    void Render()
    {
        uIController.timeLimitUI.value.text = time.ToString("0.0");
        uIController.timeLimitUI.gauge.rectTransform.sizeDelta = new Vector2(imageSize.x * (time / stageData.stageList[stageController.stageNum].timeLimit), imageSize.y);
        time -= Time.deltaTime;
    }

    void Update()
    {
        // ���Ԑ؂�Ȃ玞�Ԃ����������ăQ�[���I�[�o�[����
        if (time < 0)
        {
            SetTimeLimit();
            gameOver.GameOverProcess();
        }
    }

    void Start()
    {
        // �f���Q�[�g�ɕ`��֐���o�^
        uIController.timeLimitUI.renderTimeLimit += Render;

        // �f���Q�[�g�ɏ������֐���o�^
        initialize.init_Stage += SetTimeLimit;

        // �������Ԃ�ݒ�
        SetTimeLimit();

        // �Q�[�W�̉摜�̃T�C�Y�̏����l��ݒ�
        imageSize = uIController.timeLimitUI.gauge.rectTransform.sizeDelta;
    }
}
