using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �L�[����̃K�C�h��UI���Ǘ�
public class KeyGuideUI : Singleton<KeyGuideUI>
{
    [SerializeField] private GameObject guideObj;       // �L�[����K�C�h�̃v���n�u
    [SerializeField] private GameObject parentObj;      // �L�[����K�C�h�̃v���n�u�̐e�I�u�W�F�N�g
    [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;

    public List<GameObject> keyGuides = new List<GameObject>();

    private bool isFirstDraw = true;

    // �L�[����̃K�C�h��UI��`��
    public void DrawGuide(KeyGuide.KeyGuideIconAndTextType[] types)
    {
        // �K�C�h�������\������Ă��Ȃ���΍���̕`��͍ŏ��̕`��
        if (keyGuides.Count == 0) isFirstDraw = true;

        // ���ɃK�C�h���`��ς݂Ȃ�
        if (!isFirstDraw)
        {
            // ���ݕ\�����Ă���K�C�h�Ɠ����K�C�h��`�悵�悤�Ƃ��Ă�����I��
            if (!RedrawCheck(types)) return;
        }
        // ���ꂪ�ŏ��̕`��Ȃ� RedrawCheck ���s��Ȃ�
        else if(isFirstDraw)
        {
            isFirstDraw = false;
        }

        // List �̒��g����ɂ���
        DestroyGuide();

        // �����̐��K�C�h�𐶐�
        for (int i = 0; i < types.Length; i++)
        {
            GameObject obj = Instantiate(guideObj);                 // �C���X�^���X����
            obj.transform.SetParent(parentObj.transform, false);    // �e�I�u�W�F�N�g��ݒ�
            var component = obj.GetComponent<KeyGuide>();           // �R���|�[�l���g�擾
            component.TextType = types[i].text;         // �K�C�h�̃e�L�X�g�̎�ނ�ݒ�
            component.IconType = types[i].icon;         // �K�C�h�̃A�C�R���̎�ނ�ݒ�
            keyGuides.Add(obj);                                     // ���X�g�ɒǉ�
        }

        // HorizontalLayoutGroup ���X�V������
        StartCoroutine(UpdateLayoutGroup());
    }

    // �L�[����K�C�hUI���폜
    public void DestroyGuide()
    {
        // List �̒��g����ɂ���
        for (int i = 0; i < keyGuides.Count; ++i)
        {
            Destroy(keyGuides[i]);
        }
        keyGuides.Clear();
    }

    // ���ݕ\�����Ă���K�C�h�Ɠ����K�C�h��`�悵�悤�Ƃ��Ă��邩�`�F�b�N
    bool RedrawCheck(KeyGuide.KeyGuideIconAndTextType[] types)
    {
        int redrawCount = 0;

        // ��r����z��̒������Ⴄ�Ȃ� true ��Ԃ�
        if (keyGuides.Count != types.Length) return true;

        for (int i = 0; i < keyGuides.Count; ++i)
        {
            // �R���|�[�l���g�擾
            KeyGuide component = keyGuides[i].GetComponent<KeyGuide>();

            // �����K�C�h�Ȃ�J�E���g
            if ((component.TextType == types[i].text) && (component.IconType == types[i].icon))
                ++redrawCount;
        }

        // �S�ẴK�C�h���`��ς݂Ȃ� false
        if (redrawCount >= keyGuides.Count)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // HorizontalLayoutGroup ���X�V������
    public IEnumerator UpdateLayoutGroup()
    {
        horizontalLayoutGroup.enabled = false;

        // ��u�҂�
        yield return new WaitForSecondsRealtime(0.03f);
        horizontalLayoutGroup.enabled = true;
    }
}
