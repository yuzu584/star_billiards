using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ˜f¯‚ğ”j‰ó
public class DestroyPlanet : Singleton<DestroyPlanet>
{
    [SerializeField] private StageData stageData; // Inspector‚ÅStageData‚ğw’è

    private ScreenController scrCon;
    private PopupController popupCon;
    private StageController stageCon;
    private PlanetAmount planetAmount;
    private MissionUIController missionUICon;

    private void Start()
    {
        scrCon = ScreenController.instance;
        popupCon = PopupController.instance;
        stageCon = StageController.instance;
        planetAmount = PlanetAmount.instance;
        missionUICon = MissionUIController.instance;
    }

    // ˜f¯‚ğ”j‰ó
    public void DestroyPlanetProcess(GameObject obj)
    {
        // ƒQ[ƒ€’†‚É˜f¯‚ÆÕ“Ë‚µ‚½‚ç
        if ((obj.CompareTag("Planet")) && (scrCon.Screen == ScreenController.ScreenType.InGame))
        {
            // ˜f¯‚ª”j‰ó‚³‚ê‚½|‚ğ“`‚¦‚éƒ|ƒbƒvƒAƒbƒv‚ğ•`‰æ
            popupCon.DrawDestroyPlanetPopUp(obj.name);

            // ƒ~ƒbƒVƒ‡ƒ“‚ª"‘S‚Ä‚Ì˜f¯‚ğ”j‰ó"‚È‚ç
            if (stageData.stageList[stageCon.stageNum].missionNum == 0)
            {
                // ˜f¯‚ğ”j‰ó‚µ‚½”‚ğƒJƒEƒ“ƒg
                planetAmount.planetDestroyAmount++;

                // ƒ~ƒbƒVƒ‡ƒ“UI‚ğXV
                missionUICon.DrawMissionUI();
            }

            // ˜f¯‚ğíœ
            Destroy(obj);
        }
    }
}
