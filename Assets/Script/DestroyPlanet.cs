using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ɏՓ˂����f�����폜
public class DestroyPlanet : MonoBehaviour
{
    [SerializeField] private UIController uIController;       // Inspector��UIController���w��
    [SerializeField] private StageData stageData;             // Inspector��StageData���w��
    [SerializeField] private StageController stageController; // Inspector��StageController���w��

    [System.NonSerialized] public int planetDestroyAmount = 0; // �f����j�󂵂���

    // ���������ƏՓ˂�����
    void OnCollisionEnter(Collision collision)
    {
        // �f���ƏՓ˂�����
        if (collision.gameObject.CompareTag("Planet"))
        {
            // �|�b�v�A�b�v�̐����J�E���g
            uIController.popupAmount++;

            // �f�����j�󂳂ꂽ�|��`����|�b�v�A�b�v��`��
            StartCoroutine(uIController.DrawDestroyPlanetPopup(collision.gameObject.name));

            // �~�b�V������"�S�Ă̘f����j��"�Ȃ�
            if(stageData.stageList[stageController.stageNum].missionNum == 0)
            {
                // �f����j�󂵂������J�E���g
                planetDestroyAmount++;

                // �~�b�V����UI���X�V
                uIController.DrawMissionUI();
            }

            // �f�����폜
            Destroy(collision.gameObject);
        }
    }
}
