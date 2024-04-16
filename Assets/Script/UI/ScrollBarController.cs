using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �X�N���[���o�[�̋������Ǘ�
public class ScrollBarController : Singleton<ScrollBarController>
{
    public Scrollbar scrollbar;                     // �X�N���[���o�[
    public RectTransform parentRect;                // �e�I�u�W�F�N�g��RectTransform
    public RectTransform contentParentRect;         // �X�N���[�������R���e���c�̐e�I�u�W�F�N�g��RectTransform
    public Button.Group group;                      // �X�N���[������{�^���̃O���[�v

    public int num = 0;

    // �X�N���[������
    public void Scroll(Scrollbar sBar, bool up, float value)
    {
        // ��ɃX�N���[������Ȃ�
        if(up)
        {
            // �o�[����ɓ�����
            sBar.value += value * 3.0f;
        }

        // ���ɃX�N���[������Ȃ�
        else
        {
            // �o�[�����ɓ�����
            sBar.value -= value * 3.0f;
        }
    }
}
