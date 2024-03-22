using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������Ԃ��Ǘ�
public class TimeLimit : MonoBehaviour
{
    [SerializeField] private StageData stageData;           // Inspector��StageData���w��

    private StageController stageCon;
    private UIController uICon;
    private GameOver gameOver;
    private Initialize init;

    private float time = 0;                                 // ��������
    private Vector2 imageSize;                              // �Q�[�W�̉摜�̃T�C�Y�̏����l

    // �������Ԃ�ݒ�
    void SetTimeLimit()
    {
        if (time != stageData.stageList[stageCon.stageNum].timeLimit)
            time = stageData.stageList[stageCon.stageNum].timeLimit;
    }

    // �������Ԃ�UI��`��
    void Render()
    {
        uICon.timeLimitUI.value.text = time.ToString("0.0");
        uICon.timeLimitUI.gauge.rectTransform.sizeDelta = new Vector2(imageSize.x * (time / stageData.stageList[stageCon.stageNum].timeLimit), imageSize.y);
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
        stageCon = StageController.instance;
        uICon = UIController.instance;
        gameOver = GameOver.instance;
        init = Initialize.instance;

        // �f���Q�[�g�ɕ`��֐���o�^
        uICon.timeLimitUI.renderTimeLimit += Render;

        // �f���Q�[�g�ɏ������֐���o�^
        init.init_Stage += SetTimeLimit;

        // �������Ԃ�ݒ�
        SetTimeLimit();

        // �Q�[�W�̉摜�̃T�C�Y�̏����l��ݒ�
        imageSize = uICon.timeLimitUI.gauge.rectTransform.sizeDelta;
    }
}
