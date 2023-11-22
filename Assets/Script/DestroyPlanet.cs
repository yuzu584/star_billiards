using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 恒星に衝突したら自分を削除
public class DestroyPlanet : MonoBehaviour
{
    [SerializeField] private StageData stageData; // InspectorでStageDataを指定

    // Findで探すGameObject
    private GameObject canvas;
    private GameObject stageController;
    private GameObject uIFunctionController;
    private GameObject screenController;
    private GameObject planetAmount;

    // Findで探したGameObjectのコンポーネント
    private ScreenController _screenController;
    private PopupController _popupController;
    private StageController _stageController;
    private PlanetAmount _planetAmount;
    private MissionUIController _missionUIController;

    // もし何かと衝突したら
    void OnCollisionEnter(Collision collision)
    {
        // ゲーム中に恒星と衝突したら
        if ((collision.gameObject.CompareTag("FixedStar")) && (_screenController.screenNum == 0))
        {
            // 惑星が破壊された旨を伝えるポップアップを描画
            _planetAmount.DrawDestroyPlanetPopup(_popupController, gameObject.name);

            // ミッションが"全ての惑星を破壊"なら
            if (stageData.stageList[_stageController.stageNum].missionNum == 0)
            {
                // 惑星を破壊した数をカウント
                _planetAmount.planetDestroyAmount++;

                // ミッションUIを更新
                _missionUIController.DrawMissionUI();
            }

            // 惑星を削除
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        // GameObjectを探す
        canvas = GameObject.Find("Canvas");
        stageController = GameObject.Find("StageController");
        uIFunctionController = GameObject.Find("UIFunctionController");
        screenController = GameObject.Find("ScreenController");
        planetAmount = GameObject.Find("PlanetAmount");

        // 探したGameObjectのコンポーネントを取得
        _screenController = screenController.gameObject.GetComponent<ScreenController>();
        _popupController = uIFunctionController.gameObject.GetComponent<PopupController>();
        _stageController = stageController.gameObject.GetComponent<StageController>();
        _planetAmount = planetAmount.gameObject.GetComponent<PlanetAmount>();
        _missionUIController = uIFunctionController.gameObject.GetComponent<MissionUIController>();
    }
}
