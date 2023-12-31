using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 時間の流れる速さを管理
public class TimeSpeedController : MonoBehaviour
{
    [SerializeField] private ScreenData screenData;             // InspectorでScreenDataを指定
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定

    void Update()
    {
        // 時間の速さが正常でなければ
        if(Time.timeScale != screenData.screenList[screenController.screenNum].timeScale)
        {
            // 時間の速さを正常にする
            Time.timeScale = screenData.screenList[screenController.screenNum].timeScale;
        }
    }
}
