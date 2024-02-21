using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �|�b�v�A�b�v���Ǘ�
public class PopupController : Lerp
{
    [SerializeField] private GameObject popUp;                  // �|�b�v�A�b�v�̃v���n�u
    [SerializeField] private UIController uIController;         // Inspector��UIController���w��
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��
    [SerializeField] private Initialize initialize;             // Inspector��Initialize���w��
    [SerializeField] private Lerp lerp;                         // Inspector��Lerp���w��

    [System.NonSerialized] public GameObject[] drawingPopup = new GameObject[10]; // �|�b�v�A�b�v�̔z��

    // �|�b�v�A�b�v��`��
    private IEnumerator DrawPopup(string text)
    {
        float destroyTime = 10.0f;   // �f����j�󂷂�܂ł̎���
        int i = 0;                   // ���𐔂���ϐ�
        float fadeTime = 1.0f;       // �t�F�[�h����
        float moveDistance = 300.0f; // �ړ�����
        Vector3 defaultPosition;     // �f�t�H���g�̈ʒu

        // �z��̋󂫂�������܂ŌJ��Ԃ�
        while ((drawingPopup[i]))
        {
            // �z��͈̔͊O�Ȃ�R���[�`���I��
            if (i > drawingPopup.Length)
                yield break;
            i++;
        }

        // �|�b�v�A�b�v�̃C���X�^���X�𐶐�
        drawingPopup[i] = Instantiate(popUp);

        // �|�b�v�A�b�v�̖��O��ݒ�
        drawingPopup[i].name = text;

        // �e��ݒ�
        drawingPopup[i].transform.SetParent(uIController.messageUI.Message.transform, false);

        // �ʒu��ݒ�
        drawingPopup[i].transform.localPosition += new Vector3(-moveDistance, i * -20.0f, 0.0f);

        // �v���n�u�̃e�L�X�g���擾
        Text popupText = drawingPopup[i].transform.GetChild(1).GetComponent<Text>();

        // �v���n�u�̃e�L�X�g��ݒ�
        popupText.text = text;

        // �f�t�H���g�ʒu��ݒ�
        defaultPosition = drawingPopup[i].transform.localPosition;

        // �|�b�v�A�b�v�𓮂���
        yield return Position_GameObject(drawingPopup[i], defaultPosition, defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), fadeTime);

        // �|�b�v�A�b�v�����Ԃ��o�߂���܂ő҂�
        yield return new WaitForSeconds(destroyTime);

        // �|�b�v�A�b�v�𓮂���
        if (drawingPopup[i] != null)
            yield return Position_GameObject(drawingPopup[i], defaultPosition, defaultPosition - new Vector3(moveDistance, 0.0f, 0.0f), fadeTime);

        // �|�b�v�A�b�v���폜
        Destroy(drawingPopup[i]);
    }

    // �f�����j�󂳂ꂽ�ۂ̃|�b�v�A�b�v���Ăяo��
    public void DrawDestroyPlanetPopUp(string name)
    {
        StartCoroutine(DrawPopup(name + " was destroyed"));
    }

    // �|�b�v�A�b�v��������
    public void Init()
    {
        for(int i = 0; i < drawingPopup.Length; i++)
        {
            // �C���X�^���X�����݂�����폜
            if (drawingPopup[i])
            {
                Destroy(drawingPopup[i]);
            }
        }

        // �|�b�v�A�b�v���`�悳��Ă��邩���Ǘ�����ϐ���������
        for (int i = 0; i < drawingPopup.Length; i++)
            drawingPopup[i] = null;
    }

    void Start()
    {
        // �f���Q�[�g�ɏ������֐���o�^
        initialize.init_Stage += Init;

        // �ŏ��̏�����
        Init();
    }

    void Update()
    {
        // �|�b�v�A�b�v�̌��J��Ԃ�
        for (int i = 0; i < drawingPopup.Length; i++)
        {
            // �C���X�^���X����������Ă����
            if (drawingPopup[i] != null)
            {
                // �Q�[����ʂ���\���Ȃ�
                if ((screenController.screenNum == 5) && (!drawingPopup[i].activeSelf))

                    // �\������
                    drawingPopup[i].SetActive(true);

                // �Q�[����ʈȊO���\������Ă���Ȃ�
                else if ((screenController.screenNum != 5) && (drawingPopup[i].activeSelf))

                    // ��\���ɂ���
                    drawingPopup[i].SetActive(false);
            }
        }
    }
}
