using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �f�����UI���Ǘ�
public class PlanetInfoUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController; // Inspector��UIController���w��
    [SerializeField] private Converter converter;       // Inspector��Converter���w��

    // �f�����UI��`��
    public void DrawPlanetInfoUI(Vector3 position, string planetName)
    {
        // �f�����UI�̉~�̃X�N���[�����W��ύX
        uIController.planetInfoUI.targetRing.rectTransform.localPosition = converter.WSVConvert(position);

        // �f���̖��OUI�̃e�L�X�g��ݒ�
        uIController.planetInfoUI.planetName.text = planetName;

        // �f���̖��OUI�̈ʒu��ݒ�
        uIController.planetInfoUI.planetName.rectTransform.localPosition = converter.WSVConvert(position);
    }
}
