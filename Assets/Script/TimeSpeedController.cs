using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���Ԃ̗���鑬�����Ǘ�
public class TimeSpeedController : MonoBehaviour
{
    [SerializeField] private ScreenData screenData;             // Inspector��ScreenData���w��
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��

    void Update()
    {
        // ���Ԃ̑���������łȂ����
        if(Time.timeScale != screenData.screenList[screenController.screenNum].timeScale)
        {
            // ���Ԃ̑����𐳏�ɂ���
            Time.timeScale = screenData.screenList[screenController.screenNum].timeScale;
        }
    }
}
