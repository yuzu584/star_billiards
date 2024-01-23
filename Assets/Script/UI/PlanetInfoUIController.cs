using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �f�����UI���Ǘ�
public class PlanetInfoUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController; // Inspector��UIController���w��

    Vector3 PIL1; // �f�����UI�̐��̎n�_���W
    Vector3 PIL2; // �f�����UI�̐��̒��ԍ��W
    Vector3 PIL3; // �f�����UI�̐��̏I�_���W

    // �f�����UI��`��
    public void DrawPlanetInfoUI(Vector3 position, string planetName)
    {
        // �f�����UI�̉~�̃X�N���[�����W��ύX
        uIController.planetInfoUI.targetRing.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, position);

        // �f�����UI�̐��̃X�N���[�����W�����[���h���W�ɕϊ�
        PIL1 = Camera.main.ScreenToWorldPoint(uIController.planetInfoUI.targetRing.rectTransform.position + new Vector3(0, 0, 10));
        PIL2 = Camera.main.ScreenToWorldPoint(uIController.planetInfoUI.targetRing.rectTransform.position + new Vector3(50, 50, 10));
        PIL3 = Camera.main.ScreenToWorldPoint(uIController.planetInfoUI.targetRing.rectTransform.position + new Vector3(150, 50, 10));

        // ����`��
        uIController.planetInfoUI.planetInfoLine.SetPosition(0, PIL1);
        uIController.planetInfoUI.planetInfoLine.SetPosition(1, PIL2);
        uIController.planetInfoUI.planetInfoLine.SetPosition(2, PIL3);

        // �f���̖��O���e�L�X�g�ɐݒ�
        uIController.planetInfoUI.planetName.text = planetName;

        // �f���̖��OUI�̈ʒu��ݒ�
        uIController.planetInfoUI.planetName.rectTransform.position = uIController.planetInfoUI.targetRing.rectTransform.position + new Vector3(160, 80, 10);
    }
}
