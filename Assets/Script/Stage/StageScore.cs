using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W���Ƃ̃X�R�A���Ǘ�����
public class StageScore : Singleton<StageScore>
{
    [SerializeField] private StageData stageData;

    public int[] score;

    private void Start()
    {
        // �X�R�A�z��̒������X�e�[�W�̐��ɂ���
        score = new int[stageData.stageList.Count];
    }
}
