using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W���Ǘ�
public class StageController : MonoBehaviour
{
    [SerializeField] private StageData stageData;         // Inspector��StageData���w��
    [SerializeField] private DestroyPlanet destroyPlanet; // Inspector��DestroyPlanet���w��
    public int stageNum = 0;                              // �X�e�[�W�ԍ�
    public bool stageCrear = false;                       // �X�e�[�W���N���A�������ǂ���

    private int missionNum; // �~�b�V�����ԍ�

    void Update()
    {
        // �~�b�V�����ԍ�����
        if (missionNum != stageData.stageList[stageNum].missionNum)
            missionNum = stageData.stageList[stageNum].missionNum;

        // �~�b�V������"�S�Ă̘f����j��"���N���A������B�������Ȃ�
        if (missionNum == 0 && (destroyPlanet.planetDestroyAmount >= stageData.stageList[stageNum].planet.Length))
        {
            // �X�e�[�W�N���A
            stageCrear = true;
        }
    }
}
