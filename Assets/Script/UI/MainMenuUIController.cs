using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���C�����j���[��UI���Ǘ�
public class MainMenuUIController : MonoBehaviour
{
    // ���C�����j���[��\��/��\��
    public void DrawMainMenu(bool draw, GameObject allMainMenuUI)
    {
        // �\��/ ��\���؂�ւ�
        if(allMainMenuUI.activeSelf != draw)
            allMainMenuUI.SetActive(draw);
    }
}
