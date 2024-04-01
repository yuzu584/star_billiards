using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 惑星を破壊
public class DestroyPlanet : Singleton<DestroyPlanet>
{
    [SerializeField] private StageData stageData; // InspectorでStageDataを指定

    private ScreenController scrCon;
    private StageController stageCon;
    private PlanetAmount planetAmount;
    private PopupManager popupMana;
    private Localize localize;

    public delegate void DestroyPlanetDele();
    public DestroyPlanetDele DPdele;

    private void Start()
    {
        scrCon = ScreenController.instance;
        stageCon = StageController.instance;
        planetAmount = PlanetAmount.instance;
        popupMana = PopupManager.instance;
        localize = Localize.instance;
    }

    // 惑星を破壊
    public void DestroyPlanetProcess(GameObject obj)
    {
        // ゲーム中に惑星と衝突したら
        if ((obj.CompareTag("Planet")) && (scrCon.Screen == ScreenController.ScreenType.InGame))
        {
            // 惑星が破壊された旨を伝えるポップアップを描画
            popupMana.DrawPopup(PopupManager.PopupType.InGamePopup1, obj.name + " " + localize.GetString_Message(EnumMessage.WasDestroyed));

            // ミッションが"全ての惑星を破壊"なら
            if (stageData.stageList[stageCon.stageNum].missionNum == 0)
            {
                // 惑星を破壊した数をカウント
                planetAmount.planetDestroyAmount++;

                DPdele?.Invoke();
            }

            // 惑星を削除
            Destroy(obj);
        }
    }
}
