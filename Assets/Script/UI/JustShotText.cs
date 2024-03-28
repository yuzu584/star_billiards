using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �W���X�g�V���b�g���ɕ\�������e�L�X�g���Ǘ�
public class JustShotText : MonoBehaviour
{
    [SerializeField] private Text justShotText; // �W���X�g�V���b�g�̃e�L�X�g

    private JustShot jShot;

    private void Start()
    {
        jShot = JustShot.instance;

        // �W���X�g�V���b�g���Ƀe�L�X�g��\������f���Q�[�g�ɓo�^
        jShot.drawJustShotText += Draw;

        // �e�L�X�g���\��
        Draw(false);
    }

    private void OnDestroy()
    {
        jShot.drawJustShotText -= Draw;
    }

    // �e�L�X�g��\��/��\��
    void Draw(bool orDraw)
    {
        justShotText.enabled = orDraw;
    }
}
