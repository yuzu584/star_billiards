using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 時間の流れる速さを管理
public class TimeSpeedController : MonoBehaviour
{
    [SerializeField] private ScreenData screenData;             // InspectorでScreenDataを指定

    private ScreenController scrCon;

    private void Start()
    {
        scrCon = ScreenController.instance;
    }

    void Update()
    {
        // 時間の速さが正常でなければ
        if(Time.timeScale != screenData.screenList[scrCon.ScreenNum].timeScale)
        {
            // 時間の速さを正常にする
            Time.timeScale = screenData.screenList[scrCon.ScreenNum].timeScale;
        }
    }
}
