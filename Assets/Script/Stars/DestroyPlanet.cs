using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ˜f¯‚ğ”j‰ó
public class DestroyPlanet : Singleton<DestroyPlanet>
{
    [SerializeField] private StageData stageData; // Inspector‚ÅStageData‚ğw’è

    private ScreenController scrCon;
    private StageController stageCon;
    private PlanetAmount planetAmount;
    private PopupManager popupMana;

    public delegate void DestroyPlanetDele();
    public DestroyPlanetDele DPdele;

    private void Start()
    {
        scrCon = ScreenController.instance;
        stageCon = StageController.instance;
        planetAmount = PlanetAmount.instance;
        popupMana = PopupManager.instance;
    }

    // ˜f¯‚ğ”j‰ó
    public void DestroyPlanetProcess(GameObject obj)
    {
        // ƒQ[ƒ€’†‚É˜f¯‚ÆÕ“Ë‚µ‚½‚ç
        if ((obj.CompareTag("Planet")) && (scrCon.Screen == ScreenController.ScreenType.InGame))
        {
            // ˜f¯‚ª”j‰ó‚³‚ê‚½|‚ğ“`‚¦‚éƒ|ƒbƒvƒAƒbƒv‚ğ•`‰æ
            popupMana.DrawPopup(PopupManager.PopupType.InGamePopup1, obj.name +" was destroyed");

            // ƒ~ƒbƒVƒ‡ƒ“‚ª"‘S‚Ä‚Ì˜f¯‚ğ”j‰ó"‚È‚ç
            if (stageData.stageList[stageCon.stageNum].missionNum == 0)
            {
                // ˜f¯‚ğ”j‰ó‚µ‚½”‚ğƒJƒEƒ“ƒg
                planetAmount.planetDestroyAmount++;

                DPdele?.Invoke();
            }

            // ˜f¯‚ğíœ
            Destroy(obj);
        }
    }
}
