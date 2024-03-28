using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

// �Q�[�����̃|�b�v�A�b�v�S�ʂ��Ǘ�
[DefaultExecutionOrder(-100)]
public class PopupManager : Singleton<PopupManager>
{
    // �|�b�v�A�b�v�̗v�f
    [System.Serializable]
    public struct PopupContent
    {
        public GameObject obj;              // �|�b�v�A�b�v�̃v���n�u
        public Transform parentTransform;   // �|�b�v�A�b�v�̃v���n�u�̐e�I�u�W�F�N�g�� Transform
        public int maxDraw;                 // �|�b�v�A�b�v�̍ő�`�搔
        public GameObject[] instance;       // �|�b�v�A�b�v�̃C���X�^���X
        public PopupParent[] component;     // �擾�����|�b�v�A�b�v�̃R���|�[�l���g
        public Coroutine[] coroutines;      // �|�b�v�A�b�v�̃R���[�`��
    }

    public PopupContent[] popupContent;

    private int count = 0;

    public enum PopupType
    {
        InGamePopup1,
        InMenuPopup1,
        DialogPopup1,
    }

    private void Start()
    {
        // �\���̗̂v�f�̒�����ݒ�
        for(int i = 0; i < popupContent.Length; i++)
        {
            popupContent[i].instance = new GameObject[popupContent[i].maxDraw];
            popupContent[i].component = new PopupParent[popupContent[i].maxDraw];
            popupContent[i].coroutines = new Coroutine[popupContent[i].maxDraw];
        }
    }

    // �w�肵���|�b�v�A�b�v��`��
    public GameObject DrawPopup(PopupType pType, string text)
    {
        // �󂢂Ă���z��̏ꏊ����
        int count = CheckInstance(popupContent[(int)pType]);

        // �z�񂪊J���Ă��Ȃ���ΏI��
        if (count == -1) return null;

        // �C���X�^���X����
        popupContent[(int)pType].instance[count] = Instantiate(popupContent[(int)pType].obj);

        // �R���|�[�l���g�擾
        popupContent[(int)pType].component[count] = popupContent[(int)pType].instance[count].GetComponent<PopupParent>();

        // �|�b�v�A�b�v�̏������s��
        popupContent[(int)pType].coroutines[count] = StartCoroutine(popupContent[(int)pType].component[count].Process(text, popupContent[(int)pType].parentTransform, count));

        // ���������C���X�^���X���Ԃ�l
        return popupContent[(int)pType].instance[count];
    }

    // �C���X�^���X�̔z��̊J���Ă���ꏊ��T���ĕԂ�
    public int CheckInstance(PopupContent pCon)
    {
        count = 0;

        for (int i = 0; i < pCon.instance.Length; i++)
        {
            if (pCon.instance[i] == null) return count;

            ++count;
        }

        return -1;
    }

    // �z���������
    public void Init(PopupContent pCon)
    {
        for(int i = 0; i < pCon.instance.Length; ++i)
        {
            // PopupParent �̃R���|�[�l���g���擾�ς݂Ȃ�|�b�v�A�b�v�폜�������s��
            if (pCon.component[i] != null)
                pCon.component[i].Destroy();
        }
    }

    // ����̃R���[�`�����~������(StopCoroutine ���O������Ăяo���ƃG���[���o�邽�߂��̊֐����g�p���ăR���[�`�����~������)
    public void StopCoroutineOfPopupContent(PopupContent pCon, int index)
    {
        StopCoroutine(pCon.coroutines[index]);
    }
}
