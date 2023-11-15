using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �|�b�v�A�b�v���Ǘ�
public class PopupController : MonoBehaviour
{
    [SerializeField] private GameObject popUp;                  // �|�b�v�A�b�v�̃v���n�u
    [SerializeField] private UIController uIController;         // Inspector��UIController���w��
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��

    [System.NonSerialized] public int popupAmount = 0;                // �|�b�v�A�b�v�̐�
    [System.NonSerialized] public bool[] drawingPopup = new bool[10]; // �|�b�v�A�b�v���`�悳��Ă��邩

    // �|�b�v�A�b�v�𓮂���
    public IEnumerator MovePopup(float time, float fadeTime, GameObject popup, float moveDistance, Vector3 defaultPosition, int i)
    {
        // �w�肵�����Ԃ��o�߂���܂ŌJ��Ԃ�
        while (time < fadeTime)
        {
            // ���Ԃ��J�E���g
            time += Time.deltaTime;

            // �i�݋���v�Z
            float t = time / fadeTime;

            // �|�b�v�A�b�v���ړ�
            popup.transform.position = Vector3.Lerp(defaultPosition, defaultPosition + new Vector3(moveDistance, 0.0f, 0.0f), t);

            // 1�t���[���҂�
            yield return null;
        }

        if (moveDistance < 0)
        {
            // �`�悵�Ă��Ȃ���Ԃɂ���
            drawingPopup[i] = false;
        }
    }

    // �|�b�v�A�b�v��`��
    public IEnumerator DrawDestroyPlanetPopup(string text)
    {
        float destroyTime = 10.0f;   // �f����j�󂷂�܂ł̎���
        int i = 0;                   // ���𐔂���ϐ�
        float fadeTime = 0.2f;       // �t�F�[�h����
        float moveDistance = 300.0f; // �ړ�����
        Vector3 defaultPosition;     // �f�t�H���g�̈ʒu

        // false��������܂ŌJ��Ԃ�
        while ((drawingPopup[i]))
        {
            // �z��͈̔͊O�Ȃ�R���[�`���I��
            if (i > drawingPopup.Length)
                yield break;
            i++;
        }

        // �`��ς݂ɂ���
        drawingPopup[i] = true;

        // �|�b�v�A�b�v�̃C���X�^���X�𐶐�
        GameObject popup = Instantiate(popUp);

        // �e��ݒ�
        popup.transform.SetParent(uIController.messageUI.Message.transform, false);

        // �ʒu��ݒ�
        popup.transform.position += new Vector3(-moveDistance, i * -40.0f, 0.0f);

        // �v���n�u�̃e�L�X�g���擾
        Text popupText = popup.transform.GetChild(1).GetComponent<Text>();

        // �v���n�u�̃e�L�X�g��ݒ�
        popupText.text = text;

        // �o�ߎ��Ԃ��J�E���g
        float time = 0;

        // �f�t�H���g�ʒu��ݒ�
        defaultPosition = popup.transform.position;

        // �|�b�v�A�b�v�𓮂���
        StartCoroutine(MovePopup(time, fadeTime, popup, moveDistance, defaultPosition, i));

        // �|�b�v�A�b�v�����Ԃ��o�߂���܂ő҂�
        yield return new WaitForSeconds(destroyTime);

        // �|�b�v�A�b�v�𓮂���
        StartCoroutine(MovePopup(time, fadeTime, popup, -moveDistance, defaultPosition, i));

        yield return new WaitUntil(() => !(drawingPopup[i]));

        // �|�b�v�A�b�v���폜
        Destroy(popup.gameObject);

        // �|�b�v�A�b�v�J�E���g�����炷
        popupAmount--;
    }

    void Start()
    {
        // �|�b�v�A�b�v���`�悳��Ă��邩���Ǘ�����ϐ���������
        for (int i = 0; i > drawingPopup.Length; i++)
            drawingPopup[i] = false;
    }
}