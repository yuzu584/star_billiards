using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �f����j��
public class DestroyPlanet : Singleton<DestroyPlanet>
{
    [SerializeField] private StageData stageData; // Inspector��StageData���w��

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

    // �f����j��
    public void DestroyPlanetProcess(GameObject obj)
    {
        // �Q�[�����ɘf���ƏՓ˂�����
        if ((obj.CompareTag("Planet")) && (scrCon.Screen == ScreenController.ScreenType.InGame))
        {
            // �f�����j�󂳂ꂽ�|��`����|�b�v�A�b�v��`��
            popupCon.DrawDestroyPlanetPopUp(obj.name);

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
