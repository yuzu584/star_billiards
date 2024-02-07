using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �f���̃X�e�[�^�X�̃��X�g
[CreateAssetMenu(menuName = "MyScriptable/Create planetData")]
public class planetData : ScriptableObject
{
    public List<planetDataContent> planetList = new List<planetDataContent>();
}

// �f���̃X�e�[�^�X�̓��e
[System.Serializable]
public class planetDataContent
{
    [SerializeField] string planetName; // ���O
    [SerializeField] int scale;         // �傫��
    [SerializeField] Material material; // �}�e���A��
    [SerializeField] bool fixedStar;    // �P�����ۂ�
}