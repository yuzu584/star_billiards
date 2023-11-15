using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// ポーズ画面のUIを管理
public class PauseUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController;                   // InspectorでUIControllerを指定
    [SerializeField] private PostProcessController postProcessController; // InspectorでPostProcessControllerを指定

    // ポーズ画面のUIを表示又は非表示にする
    public void DrawPauseUI(bool draw)
    {
        // ポーズ画面を表示又は非表示
        uIController.pauseUI.allPauseUI.SetActive(draw);

        // 被写界深度のONOFF切り替え
        postProcessController.DepthOfFieldOnOff(draw);

        // レティクルを表示又は非表示
        uIController.otherUI.reticle.enabled = !(draw);
    }
}
