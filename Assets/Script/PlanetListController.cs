using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �f�����X�g���Ǘ�
public class PlanetListController : MonoBehaviour
{
    [SerializeField] private StageData stageData;                           // Inspector��StageData���w��
    [SerializeField] private StageController stageController;               // Inspector��StageController���w��
    [SerializeField] private PlanetListUIController planetListUIController; // Inspector��PlanetListUIController���w��
    [SerializeField] private ScreenController screenController;             // Inspector��ScreenController���w��
    [SerializeField] private GameObject starParentObj;                      // �X�e�[�W�̐����܂Ƃ߂�e�I�u�W�F�N�g

    public List<GameObject> planetList;   // �f���̃��X�g
    public bool uiDrawing = false;        // �f�����X�gUI���`�悳��Ă��邩

    private bool isPushKey = false; // �L�[��������Ă��邩

    // �f�����X�g���쐬
    public void CreateList()
    {
        // ���X�g�̗v�f�����ׂč폜
        planetList.Clear();

        // �q�I�u�W�F�N�g�̐��J��Ԃ�
        for (int i = 0; i < starParentObj.transform.childCount; i++)
        {
            // �q�I�u�W�F�N�g���擾
            Transform childTransform = starParentObj.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;

            // �q�I�u�W�F�N�g���f���Ȃ烊�X�g�ɒǉ�
            if (childObject.tag == "Planet")
                planetList.Add(childObject);
        }
    }

    void Update()
    {
        // �Q�[�������f�����X�g��ʂŘf�����X�g�\���{�^���������ꂽ��\��/��\���؂�ւ�
        if (((screenController.screenNum == 0) || (screenController.screenNum == 5)) && (Input.GetAxisRaw("PlanetList") != 0) && (!isPushKey))
        {
            isPushKey = true;
            uiDrawing = !uiDrawing;

            // UI���`�悳��Ă��邩�ŕ���
            if (uiDrawing)
            {
                // �f�����X�g��ʂɑJ��
                screenController.screenNum = 5;

                // UI��`��
                planetListUIController.DrawPlanetList();
            }
            else
            {
                // �Q�[����ʂɑJ��
                screenController.screenNum = 0;

                // UI���폜
                planetListUIController.DeletePlanetListContent();
            }
        }
        // �Q�[�������f�����X�g��ʂŃL�[��������Ă��Ȃ����isPushKey��������Ă��Ȃ���Ԃɂ���
        else if (((screenController.screenNum == 0) || (screenController.screenNum == 5)) && (Input.GetAxisRaw("PlanetList") == 0) && (isPushKey))
            isPushKey = false;
    }
}
