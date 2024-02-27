using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�N���[���̐e�N���X
public class Screen : MonoBehaviour, IScreen
{
    [SerializeField] protected int num = 0;                       // ���̃X�N���[����\�������ʔԍ�
    [SerializeField] protected ScreenController screenController; // Inspector��ScreenController���w��

    // �\�������ʂ�؂�ւ�
    public void SwitchScreen()
    {
        // ��ʔԍ���num�Ɠ����Ȃ�\��
        if((screenController.screenNum == num) && (!gameObject.activeSelf))
        {
            gameObject.SetActive(true);
        }
        // �Ⴄ�Ȃ��\��
        else if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
