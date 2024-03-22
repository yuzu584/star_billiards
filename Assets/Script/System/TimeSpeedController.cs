using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ŠÔ‚Ì—¬‚ê‚é‘¬‚³‚ğŠÇ—
public class TimeSpeedController : MonoBehaviour
{
    [SerializeField] private ScreenData screenData;             // Inspector‚ÅScreenData‚ğw’è

    private ScreenController scrCon;

    private void Start()
    {
        scrCon = ScreenController.instance;
    }

    void Update()
    {
        // ŠÔ‚Ì‘¬‚³‚ª³í‚Å‚È‚¯‚ê‚Î
        if(Time.timeScale != screenData.screenList[scrCon.ScreenNum].timeScale)
        {
            // ŠÔ‚Ì‘¬‚³‚ğ³í‚É‚·‚é
            Time.timeScale = screenData.screenList[scrCon.ScreenNum].timeScale;
        }
    }
}
