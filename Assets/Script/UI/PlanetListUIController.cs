using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �f�����X�gUI���Ǘ�
public class PlanetListUIController : MonoBehaviour
{
    [SerializeField] private GameObject planetListBtn;                  // �{�^���̃v���n�u
    [SerializeField] private GameObject parentObj;                      // �{�^���̃v���n�u�̐e�I�u�W�F�N�g
    [SerializeField] private PlanetListController planetListController; // Inspector��PlanetListController���w��

    public List<GameObject> btnList; // �{�^���̃v���n�u�̃��X�g

    // �f�����X�gUI��`��
    public void DrawPlanetList()
    {
        // �f�����X�g���쐬
        planetListController.CreateList();

        // �f�����X�g�̗v�f�����J��Ԃ�
        for (int i = 0; i < planetListController.planetList.Count; i++)
        {
            // �{�^���̃C���X�^���X�𐶐�
            btnList.Add(Instantiate(planetListBtn));

            // �|�b�v�A�b�v�̖��O��ݒ�
            btnList[i].name = planetListController.planetList[i].name;

            // �e��ݒ�
            btnList[i].transform.SetParent(parentObj.transform, false);

            // �ʒu��ݒ�
            btnList[i].transform.position += new Vector3(0.0f, i * -75.0f, 0.0f);

            // �v���n�u�̃e�L�X�g���擾
            Text btnText = btnList[i].transform.GetChild(2).GetComponent<Text>();

            // �v���n�u�̃e�L�X�g��ݒ�
            btnText.text = planetListController.planetList[i].name;

            // �{�^�����������Ƃ��̌��ʂ�ݒ�
            ButtonController buttonController = btnList[i].transform.GetChild(0).GetComponent<ButtonController>();
            buttonController.clickAction = ButtonController.ClickAction.LockOnPlanet;
        }
    }

    // �f�����X�gUI�̗v�f���폜
    public void DeletePlanetListContent()
    {
        // �C���X�^���X���폜
        for (int i = 0; i < btnList.Count; i++)
        {
            Destroy(btnList[i].gameObject);
        }

        // ���X�g��������
        btnList.Clear();
    }
}
