using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ɏՓ˂����f�����폜
public class DestroyPlanet : MonoBehaviour
{
    [SerializeField] private UIController uIController;               // Inspector��UIController���w��
    [SerializeField] private StageData stageData;                     // Inspector��StageData���w��
    [SerializeField] private StageController stageController;         // Inspector��StageController���w��
    [SerializeField] private PopupController popupController;         // Inspector��PopupController���w��
    [SerializeField] private MissionUIController missionUIController; // Inspector��MissionUIController���w��
    [SerializeField] private ScreenController screenController;       // Inspector��ScreenController���w��

    [System.NonSerialized] public int planetDestroyAmount = 0; // �f����j�󂵂���

    // ���������ƏՓ˂�����
    void OnCollisionEnter(Collision collision)
    {
        // �Q�[�����ɘf���ƏՓ˂�����
        if ((collision.gameObject.CompareTag("Planet")) && (screenController.screenNum == 0))
        {
            // �f�����j�󂳂ꂽ�|��`����|�b�v�A�b�v��`��
            StartCoroutine(popupController.DrawDestroyPlanetPopup(collision.gameObject.name + " was destroyed"));

            // �~�b�V������"�S�Ă̘f����j��"�Ȃ�
            if(stageData.stageList[stageController.stageNum].missionNum == 0)
            {
                // �f����j�󂵂������J�E���g
                planetDestroyAmount++;

                // �~�b�V����UI���X�V
                missionUIController.DrawMissionUI();
            }

            // �f�����폜
            Destroy(collision.gameObject);
        }
    }
}
