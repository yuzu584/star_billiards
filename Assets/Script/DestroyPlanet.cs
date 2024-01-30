using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ˜f¯‚ğ”j‰ó
public class DestroyPlanet : MonoBehaviour
{
    [SerializeField] private StageData stageData; // Inspector‚ÅStageData‚ğw’è
    [SerializeField] private ScreenController screenController;
    [SerializeField] private PopupController popupController;
    [SerializeField] private StageController stageController;
    [SerializeField] private PlanetAmount planetAmount;
    [SerializeField] private MissionUIController missionUIController;

    // ˜f¯‚ğ”j‰ó
    public void DestroyPlanetPrpcess(GameObject obj)
    {
        // ƒQ[ƒ€’†‚É˜f¯‚ÆÕ“Ë‚µ‚½‚ç
        if ((obj.CompareTag("Planet")) && (screenController.screenNum == 5))
        {
            // ˜f¯‚ª”j‰ó‚³‚ê‚½|‚ğ“`‚¦‚éƒ|ƒbƒvƒAƒbƒv‚ğ•`‰æ
            popupController.DrawDestroyPlanetPopUp(obj.name);

            // ƒ~ƒbƒVƒ‡ƒ“‚ª"‘S‚Ä‚Ì˜f¯‚ğ”j‰ó"‚È‚ç
            if (stageData.stageList[stageController.stageNum].missionNum == 0)
            {
                // ˜f¯‚ğ”j‰ó‚µ‚½”‚ğƒJƒEƒ“ƒg
                planetAmount.planetDestroyAmount++;

                // ƒ~ƒbƒVƒ‡ƒ“UI‚ğXV
                missionUIController.DrawMissionUI();
            }

            // ˜f¯‚ğíœ
            Destroy(obj);
        }
    }
}
