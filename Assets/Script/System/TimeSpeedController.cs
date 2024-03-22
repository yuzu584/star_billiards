using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���Ԃ̗���鑬�����Ǘ�
public class TimeSpeedController : MonoBehaviour
{
    [SerializeField] private ScreenData screenData;             // Inspector��ScreenData���w��

    private ScreenController scrCon;

    private void Start()
    {
        scrCon = ScreenController.instance;
    }

    void Update()
    {
        // ���Ԃ̑���������łȂ����
        if(Time.timeScale != screenData.screenList[scrCon.ScreenNum].timeScale)
        {
            // ���Ԃ̑����𐳏�ɂ���
            Time.timeScale = screenData.screenList[scrCon.ScreenNum].timeScale;
        }
    }
}
