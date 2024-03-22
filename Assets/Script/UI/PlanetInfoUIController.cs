using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �f�����UI���Ǘ�
public class PlanetInfoUIController : Singleton<PlanetInfoUIController>
{
    private UIController uICon;
    private Converter converter;

    private void Start()
    {
        uICon = UIController.instance;
        converter = Converter.instance;
    }

    // �f�����UI��`��
    public void DrawPlanetInfoUI(Vector3 position, string planetName)
    {
        // �f�����UI�̉~�̃X�N���[�����W��ύX
        uICon.planetInfoUI.targetRing.rectTransform.localPosition = converter.WSVConvert(position);

        // �f���̖��OUI�̃e�L�X�g��ݒ�
        uICon.planetInfoUI.planetName.text = planetName;

        // �f���̖��OUI�̈ʒu��ݒ�
        uICon.planetInfoUI.planetName.rectTransform.localPosition = converter.WSVConvert(position);
    }
}
