using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ݒ��ʂ��Ǘ�
public class OptionsController : Singleton<OptionsController>
{
    public enum Loot   // �K�w
    {
        Top = 0,       // �ŏ��̉��
        GamePlay = 1,  // �Q�[�����ݒ�
        Video = 2,     // �r�f�I�ݒ�
        Audio = 3,     // �I�[�f�B�I�ݒ�
        KeyConfig = 4, // �L�[�z�u�ݒ�
        Language = 5,  // ����ݒ�
    }
    public Loot loot = 0;
    public Loot oldLoot = 0;        // 1�t���[���O�̊K�w

    public OptionsUIController opUICon;

    // �\������K�w��؂�ւ�
    public void SwitchLoot(Loot l)
    {
        if (opUICon == null) return;

        // �K�w��ݒ�
        loot = l;
    }
}
