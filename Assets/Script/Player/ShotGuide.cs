using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �V���b�g���̃K�C�h
public class ShotGuide : MonoBehaviour
{
    [SerializeField] private GameObject guideObj;           // �K�C�h�Ɏg�p����v���C���[�̌����ڂ̃I�u�W�F�N�g
    [SerializeField] private GameObject parentObj;          // �e�I�u�W�F�N�g

    private PredictionLine pl;
    private EnergyController eneCon;
    private InputController input;

    public GameObject instance;                             // �v���n�u�̃C���X�^���X

    private void Start()
    {
        pl = PredictionLine.instance;
        eneCon = EnergyController.instance;
        input = InputController.instance;

        input.game_OnShotDele += GuideProcess;
    }

    // �K�C�h�̏���
    public void GuideProcess(float value)
    {
        // �C���X�^���X�������Ȃ琶��
        if(instance == null)
        {
            // �C���X�^���X����
            instance = Instantiate(guideObj);

            // ���������C���X�^���X���\��
            instance.SetActive(false);

            // �e�I�u�W�F�N�g��ݒ�
            instance.transform.SetParent(parentObj.transform, false);
        }

        // �G�l���M�[�������ԂŃV���b�g�{�^����������Ă�����
        if ((value > 0) && (eneCon.energy.Value > 0))
        {
            // ��\���Ȃ�\��
            if (!instance.activeSelf)
                instance.SetActive(true);

            // �K�C�h�̍��W���ړ�
            Vector3 pos = pl.hit1.point;
            pos += (pl.hit1.normal * (instance.transform.localScale.x / 2));
            instance.transform.localPosition = pos;
        }
        // �K�C�h���\������Ă������\��
        else if(instance.activeSelf)
        {
            instance.SetActive(false);
        }
    }
}
