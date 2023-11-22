using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �P���ɏՓ˂����玩�����폜
public class DestroyPlanet : MonoBehaviour
{
    [SerializeField] private StageData stageData; // Inspector��StageData���w��

    // Find�ŒT��GameObject
    private GameObject canvas;
    private GameObject stageController;
    private GameObject uIFunctionController;
    private GameObject screenController;
    private GameObject planetAmount;

    // Find�ŒT����GameObject�̃R���|�[�l���g
    private ScreenController _screenController;
    private PopupController _popupController;
    private StageController _stageController;
    private PlanetAmount _planetAmount;
    private MissionUIController _missionUIController;

    // ���������ƏՓ˂�����
    void OnCollisionEnter(Collision collision)
    {
        // �Q�[�����ɍP���ƏՓ˂�����
        if ((collision.gameObject.CompareTag("FixedStar")) && (_screenController.screenNum == 0))
        {
            // �f�����j�󂳂ꂽ�|��`����|�b�v�A�b�v��`��
            _planetAmount.DrawDestroyPlanetPopup(_popupController, gameObject.name);

            // �~�b�V������"�S�Ă̘f����j��"�Ȃ�
            if (stageData.stageList[_stageController.stageNum].missionNum == 0)
            {
                // �f����j�󂵂������J�E���g
                _planetAmount.planetDestroyAmount++;

                // �~�b�V����UI���X�V
                _missionUIController.DrawMissionUI();
            }

            // �f�����폜
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        // GameObject��T��
        canvas = GameObject.Find("Canvas");
        stageController = GameObject.Find("StageController");
        uIFunctionController = GameObject.Find("UIFunctionController");
        screenController = GameObject.Find("ScreenController");
        planetAmount = GameObject.Find("PlanetAmount");

        // �T����GameObject�̃R���|�[�l���g���擾
        _screenController = screenController.gameObject.GetComponent<ScreenController>();
        _popupController = uIFunctionController.gameObject.GetComponent<PopupController>();
        _stageController = stageController.gameObject.GetComponent<StageController>();
        _planetAmount = planetAmount.gameObject.GetComponent<PlanetAmount>();
        _missionUIController = uIFunctionController.gameObject.GetComponent<MissionUIController>();
    }
}
