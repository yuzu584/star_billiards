using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �f����j��
public class DestroyPlanet : MonoBehaviour
{
    [SerializeField] private StageData stageData; // Inspector��StageData���w��
    [SerializeField] private ScreenController screenController;
    [SerializeField] private PopupController popupController;
    [SerializeField] private StageController stageController;
    [SerializeField] private PlanetAmount planetAmount;
    [SerializeField] private MissionUIController missionUIController;

    // �f����j��
    public void DestroyPlanetPrpcess(GameObject obj)
    {
        // �Q�[�����ɘf���ƏՓ˂�����
        if ((obj.CompareTag("Planet")) && (screenController.screenNum == 5))
        {
            // �f�����j�󂳂ꂽ�|��`����|�b�v�A�b�v��`��
            popupController.DrawDestroyPlanetPopUp(obj.name);

            // �~�b�V������"�S�Ă̘f����j��"�Ȃ�
            if (stageData.stageList[stageController.stageNum].missionNum == 0)
            {
                // �f����j�󂵂������J�E���g
                planetAmount.planetDestroyAmount++;

                // �~�b�V����UI���X�V
                missionUIController.DrawMissionUI();
            }

            // �f�����폜
            Destroy(obj);
        }
    }
}
