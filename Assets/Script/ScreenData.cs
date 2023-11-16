using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�N���[���̃��X�g
[CreateAssetMenu(menuName = "MyScriptable/Create ScreenData")]
public class ScreenData : ScriptableObject
{
    public List<ScreenDataContent> screenList = new List<ScreenDataContent>();
}

// �X�N���[���̃��X�g
[System.Serializable]
public class ScreenDataContent
{
    public string screenName;      // ���O
    public bool drawCursol;        // �J�[�\����\�����邩
    public float timeScale = 1.0f; // ���Ԃ�����鑬��
}
