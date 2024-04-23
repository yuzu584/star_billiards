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

    public List<GameObject> keyGuideObjs = new List<GameObject>();
    public List<KeyGuide> keyGuideComponents = new List<KeyGuide>();

    public bool smoothSwitching = true;                 // �L�[����K�C�hUI�̃X���[�Y�Ȑ؂�ւ����s����

    private bool isFirstDraw = true;

    // �L�[����̃K�C�h��UI��`��
    public void DrawGuide(KeyGuide.KeyGuideIconAndTextType[] types)
    {
        // �K�C�h�������\������Ă��Ȃ���΍���̕`��͍ŏ��̕`��
        if (keyGuideObjs.Count == 0)
        {
            isFirstDraw = true;
        }

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
            keyGuideComponents.Add(obj.GetComponent<KeyGuide>());   // �R���|�[�l���g�擾

            // �K�C�h�̃e�L�X�g�ƃA�C�R���̎�ނ�ݒ�(�A�C�R���̐��ɂ���ĕ���)
            if (types[i].icon.Length == 1)
                keyGuideComponents[i].IconAndText = types[i];
            else if (types[i].icon.Length > 1)
            {
                for (int j = 0; j < types[i].icon.Length - 1; j++)
                {
                    keyGuideComponents[i].DuplicateImage(types[i]);
                }
            }

            keyGuideObjs.Add(obj);                                     // ���X�g�ɒǉ�
        }

        // HorizontalLayoutGroup ���X�V������
        StartCoroutine(UpdateLayoutGroup());
    }

    // �L�[����K�C�hUI���폜
    public void DestroyGuide()
    {
        // List �̒��g����ɂ���
        for (int i = 0; i < keyGuideObjs.Count; ++i)
        {
            Destroy(keyGuideObjs[i]);
        }
        keyGuideObjs.Clear();
        keyGuideComponents.Clear();
    }

    // ���ݕ\�����Ă���K�C�h�Ɠ����K�C�h��`�悵�悤�Ƃ��Ă��邩�`�F�b�N
    bool RedrawCheck(KeyGuide.KeyGuideIconAndTextType[] types)
    {
        int redrawCount = 0;

        // ��r����z��̒������Ⴄ�Ȃ� true ��Ԃ�
        if (keyGuideObjs.Count != types.Length) return true;

        for (int i = 0; i < keyGuideObjs.Count; ++i)
        {
            // �R���|�[�l���g�擾
            KeyGuide component = keyGuideObjs[i].GetComponent<KeyGuide>();

            // �����K�C�h�Ȃ�J�E���g
            if ((component.IconAndText.CheckIconEquals(types[i].icon)) && (component.IconAndText.text == types[i].text))
                ++redrawCount;
        }

        // �S�ẴK�C�h���`��ς݂Ȃ� false
        if (redrawCount >= keyGuideObjs.Count)
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

        // �X���[�Y�Ȑ؂�ւ����s���Ȃ�
        if (smoothSwitching)
        {
            for (int i = 0; i < keyGuideComponents.Count; ++i)
                keyGuideComponents[i].GuideEnabled(false);
        }

        // ��u�҂�
        //yield return new WaitForSecondsRealtime(0.01f);
        yield return null;

        // �X���[�Y�Ȑ؂�ւ����s���Ȃ�
        if (smoothSwitching)
        {
            for (int i = 0; i < keyGuideComponents.Count; ++i)
                keyGuideComponents[i].GuideEnabled(true);
        }

        horizontalLayoutGroup.enabled = true;
    }
}
