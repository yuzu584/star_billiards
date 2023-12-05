using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �f�����X�g���Ǘ�
public class PlanetListController : MonoBehaviour
{
    [SerializeField] StageData stageData;             // Inspector��StageData���w��
    [SerializeField] StageController stageController; // Inspector��StageController���w��
    [SerializeField] GameObject starParentObj;        // �X�e�[�W�̐����܂Ƃ߂�e�I�u�W�F�N�g

    public List<GameObject> planetList; // �f���̃��X�g

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
}
