using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �L�[����̃K�C�h��UI���Ǘ�
public class KeyGuideUI : Singleton<KeyGuideUI>
{
    [SerializeField] private GameObject guideObj;       // �L�[����K�C�h�̃v���n�u
    [SerializeField] private GameObject parentObj;      // �L�[����K�C�h�̃v���n�u�̐e�I�u�W�F�N�g

    public List<GameObject> keyGuides = new List<GameObject>();

    private void Start()
    {
        EnumKeyGuide[] g =
        {
            EnumKeyGuide.None,
            EnumKeyGuide.Positive,
            EnumKeyGuide.Negative,
            EnumKeyGuide.MoveCursol,
        };

        Draw(g);
    }

    // �L�[����̃K�C�h��UI��`��
    void Draw(EnumKeyGuide[] types)
    {
        // List �̒��g����ɂ���
        for(int i = 0; i < keyGuides.Count; ++i)
        {
            Destroy(keyGuides[i]);
        }
        keyGuides.Clear();

        // �����̐��K�C�h�𐶐�
        for (int i = 0; i < types.Length; i++)
        {
            GameObject obj = Instantiate(guideObj);                 // �C���X�^���X����
            obj.transform.SetParent(parentObj.transform, false);    // �e�I�u�W�F�N�g��ݒ�
            var component = obj.GetComponent<KeyGuide>();           // �R���|�[�l���g�擾
            component.EnumKeyGuide = types[i];                      // �K�C�h�̎�ނ�ݒ�
            keyGuides.Add(obj);                                     // ���X�g�ɒǉ�
        }
    }
}
