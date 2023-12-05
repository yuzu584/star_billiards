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
        // ���X�g�̗v�f�����ׂč폜
        btnList.Clear();

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
            btnList[i].transform.position += new Vector3(0.0f, i * -40.0f, 0.0f);

            // �v���n�u�̃e�L�X�g���擾
            Text btnText = btnList[i].transform.GetChild(2).GetComponent<Text>();

            // �v���n�u�̃e�L�X�g��ݒ�
            btnText.text = planetListController.planetList[i].name;

            // �{�^�����ʗp�ԍ���ݒ�
            ButtonController btnNum = btnList[i].GetComponent<ButtonController>();
            btnNum.number = i;
        }
    }
}
