using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W�̃��X�g
[CreateAssetMenu(menuName = "MyScriptable/Create StageData")]
public class StageData : ScriptableObject
{
    public List<StageDataContent> stageList = new List<StageDataContent>();
}

// �X�e�[�W�̃��X�g
[System.Serializable]
public class StageDataContent
{
    public string stageName;    // ���O
    public int fixedStarAmount; // �P����
    public int planetAmount;    // �f����
    public int missionNum;      // �~�b�V�����ԍ�
}