using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �f����j��
public class DestroyPlanet : Singleton<DestroyPlanet>
{
    [SerializeField] private StageData stageData; // Inspector��StageData���w��

    private ScreenController scrCon;
    private StageController stageCon;
    private PlanetAmount planetAmount;
    private MissionUIController missionUICon;
    private PopupManager popupMana;

    private void Start()
    {
        scrCon = ScreenController.instance;
        stageCon = StageController.instance;
        planetAmount = PlanetAmount.instance;
        missionUICon = MissionUIController.instance;
        popupMana = PopupManager.instance;
    }

    // �f����j��
    public void DestroyPlanetProcess(GameObject obj)
    {
        // �Q�[�����ɘf���ƏՓ˂�����
        if ((obj.CompareTag("Planet")) && (scrCon.Screen == ScreenController.ScreenType.InGame))
        {
            // �f�����j�󂳂ꂽ�|��`����|�b�v�A�b�v��`��
            popupMana.DrawPopup(PopupManager.PopupType.InGamePopup1, obj.name +" was destroyed");

            // �~�b�V������"�S�Ă̘f����j��"�Ȃ�
            if (stageData.stageList[stageCon.stageNum].missionNum == 0)
            {
                // �f����j�󂵂������J�E���g
                planetAmount.planetDestroyAmount++;

                // �~�b�V����UI���X�V
                missionUICon.DrawMissionUI();
            }

            // �f�����폜
            Destroy(obj);
        }
    }
}
