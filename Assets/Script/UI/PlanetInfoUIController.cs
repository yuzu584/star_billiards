using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �f�����UI���Ǘ�
public class PlanetInfoUIController : MonoBehaviour
{
    [SerializeField] private Image targetRing;
    [SerializeField] private Text planetName;

    private UIController uICon;
    private Converter converter;
    private SphereRay sphereRay;
    private ScreenController scrCon;

    private void Start()
    {
        uICon = UIController.instance;
        converter = Converter.instance;
        sphereRay = SphereRay.instance;
        scrCon = ScreenController.instance;
    }

    // �f�����UI��`��
    void Draw(Vector3 position, string name)
    {
        targetRing.enabled = true;
        planetName.enabled = true;

        // �f�����UI�̉~�̃X�N���[�����W��ύX
        //uICon.planetInfoUI.targetRing.rectTransform.localPosition = converter.WSVConvert(position);
        targetRing.rectTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        // �f���̖��OUI�̃e�L�X�g��ݒ�
        planetName.text = name;

        // �f���̖��OUI�̈ʒu��ݒ�
        //uICon.planetInfoUI.planetName.rectTransform.localPosition = converter.WSVConvert(position);
        planetName.rectTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        bool isPlanet = sphereRay.hitObjectTag == "Planet";         // �Ώۂ��f����
        bool isFixedStar = sphereRay.hitObjectTag == "FixedStar";   // �Ώۂ��P����

        // �Q�[�������Ώۂ��f�����P���Ȃ�`��
        if ((scrCon.Screen == ScreenController.ScreenType.InGame) && ((isPlanet || isFixedStar)))
        {
            // �f�����UI��`��
            Draw(sphereRay.hitObjectPosition, sphereRay.hitObjectName);

            // ���_�ړ����x��x������
            TPSCamera.instance.rate = AppConst.CAMERA_SLOW_SPEED_RATE;
        }
        // �Ώۂ��f�����P���ȊO�Ȃ�
        else
        {
            targetRing.enabled = false;
            planetName.enabled = false;

            // ���_�ړ����x�����ɖ߂�
            TPSCamera.instance.rate = AppConst.CAMERA_DEFAULT_SPEED_RATE;
        }
    }
}
