using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����GameObject�ɃA�^�b�`����Ǝw�肵����ʔԍ��̎��̂ݕ\�������悤�ɂȂ�
public class Screen : MonoBehaviour
{
    [SerializeField] private int num = 0;                       // ����GameObject��\�������ʔԍ�
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��

    // �\�������ʂ�؂�ւ�
    public void SwitchScreen()
    {
        // ��ʔԍ���num�Ɠ����Ȃ�\��
        if((screenController.ScreenNum == num) && (!gameObject.activeSelf))
        {
            gameObject.SetActive(true);
        }
        // �Ⴄ�Ȃ��\��
        else if ((screenController.ScreenNum != num) && (gameObject.activeSelf))
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        screenController.changeScreen += SwitchScreen;
        SwitchScreen();
    }
}
