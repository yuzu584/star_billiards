using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �|�[�Y��ʂ�UI���Ǘ�
public class PauseUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController;                   // Inspector��UIController���w��
    [SerializeField] private PostProcessController postProcessController; // Inspector��PostProcessController���w��

    void Update()
    {
        // ��ʊE�[�x��ON/OFF
        if(uIController.pauseUI.allPauseUI.activeSelf != postProcessController.GetDepthOfFieldOnOff())
        {
            postProcessController.DepthOfFieldOnOff(uIController.pauseUI.allPauseUI.activeSelf);
        }
    }
}
