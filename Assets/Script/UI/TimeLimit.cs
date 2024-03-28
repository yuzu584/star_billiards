using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �������Ԃ��Ǘ�
public class TimeLimit : MonoBehaviour
{
    [SerializeField] private StageData stageData;           // Inspector��StageData���w��
    [SerializeField] private Text value;                    // �������Ԃ̐��l�̃e�L�X�g
    [SerializeField] private Image gauge;                   // �������Ԃ̃Q�[�W

    private StageController stageCon;

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
        value.text = time.ToString("0.0");
        gauge.rectTransform.sizeDelta = new Vector2(imageSize.x * (time / stageData.stageList[stageCon.stageNum].timeLimit), imageSize.y);
        time -= Time.deltaTime;
    }

    void Update()
    {
        // ���Ԑ؂�Ȃ玞�Ԃ����������ăQ�[���I�[�o�[����
        if (time < 0)
        {
            SetTimeLimit();
            stageCon.gameOverDele?.Invoke();
        }

        Render();
    }

    void Start()
    {
        stageCon = StageController.instance;

        // �������Ԃ�ݒ�
        SetTimeLimit();

        // �Q�[�W�̉摜�̃T�C�Y�̏����l��ݒ�
        imageSize = gauge.rectTransform.sizeDelta;
    }
}
