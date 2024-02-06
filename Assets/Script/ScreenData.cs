using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �X�N���[���̃��X�g
[CreateAssetMenu(menuName = "MyScriptable/Create ScreenData")]
public class ScreenData : ScriptableObject
{
    public List<ScreenDataContent> screenList;
}

// �X�N���[���̃��X�g
[System.Serializable]
public class ScreenDataContent
{
    public string screenName;                                    // ���O
    public bool drawCursol;                                      // �J�[�\����\�����邩
    public float timeScale = 1.0f;                               // ���Ԃ�����鑬��
    public bool drawStage = false;                               // �X�e�[�W��`�悷�邩
    public bool[] uIDrawList = new bool[AppConst.SCREEN_AMOUNT]; // �`�悷��UI�̔z��
}
