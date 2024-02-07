using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �W���X�g�V���b�g���Ǘ�
public class JustShot : MonoBehaviour
{
    [SerializeField] private UIController uIController;  // Inspector��StageController���w��
    [SerializeField] private float justShotGrace = 0.1f; // �W���X�g�V���b�g�̗P�\����

    [System.NonSerialized] public float time = 0.0f;     // �W���X�g�V���b�g�̗P�\���Ԃ��J�E���g

    // �W���X�g�V���b�g�̗P�\���Ԃ��J�E���g
    public IEnumerator JustShotCount()
    {
        // �P�\���Ԃ��J�E���g
        while (time < justShotGrace)
        {
            time += Time.deltaTime;

            yield return null;
        }
        time = 0.0f;
    }

    // �W���X�g�V���b�gUI�̃A�j���[�V����
    public IEnumerator UIAnimation()
    {
        // �W���X�g�V���b�g�̃e�L�X�g��\��
        uIController.otherUI.justShotText.enabled = true;

        // �����҂�
        yield return new WaitForSeconds(1.5f);

        // �W���X�g�V���b�g�̃e�L�X�g���\��
        uIController.otherUI.justShotText.enabled = false;
    }

    void Start()
    {
        // �W���X�g�V���b�g�̃e�L�X�g���\��
        uIController.otherUI.justShotText.enabled = false;
    }
}
