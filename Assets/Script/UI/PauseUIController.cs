using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ポーズ画面のUIを管理
public class PauseUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController;                   // InspectorでUIControllerを指定
    [SerializeField] private PostProcessController postProcessController; // InspectorでPostProcessControllerを指定

    void Update()
    {
        // 被写界深度をON/OFF
        if(uIController.pauseUI.allPauseUI.activeSelf != postProcessController.GetDepthOfFieldOnOff())
        {
            postProcessController.DepthOfFieldOnOff(uIController.pauseUI.allPauseUI.activeSelf);
        }
    }
}
