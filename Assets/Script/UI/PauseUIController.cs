using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �|�[�Y��ʂ�UI���Ǘ�
public class PauseUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController;                   // Inspector��UIController���w��
    [SerializeField] private PostProcessController postProcessController; // Inspector��PostProcessController���w��

    // �|�[�Y��ʂ�UI��\�����͔�\���ɂ���
    public void DrawPauseUI(bool draw)
    {
        // �|�[�Y��ʂ�\�����͔�\��
        uIController.pauseUI.allPauseUI.SetActive(draw);

        // ��ʊE�[�x��ONOFF�؂�ւ�
        postProcessController.DepthOfFieldOnOff(draw);

        // ���e�B�N����\�����͔�\��
        uIController.otherUI.reticle.enabled = !(draw);
    }
}
