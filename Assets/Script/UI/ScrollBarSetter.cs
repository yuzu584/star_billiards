using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ScrollBarController �ɃX�N���[���o�[���Z�b�g����
public class ScrollBarSetter : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;                       // �X�N���[���o�[
    [SerializeField] private RectTransform parentRect;                  // �e�I�u�W�F�N�g��RectTransform
    [SerializeField] private RectTransform contentParentRect;           // �X�N���[�������R���e���c�̐e�I�u�W�F�N�g��RectTransform
    public Button.Group group;                                          // �X�N���[������{�^���̃O���[�v

    private ScrollBarController scrollBarCon;

    private void Start()
    {
        scrollBarCon = ScrollBarController.instance;

        // �ݒ�
        scrollBarCon.scrollbar = scrollbar;
        scrollBarCon.parentRect = parentRect;
        scrollBarCon.contentParentRect = contentParentRect;
        scrollBarCon.group = group;
    }

    private void OnDestroy()
    {
        // �폜
        scrollBarCon.scrollbar = null;
        scrollBarCon.parentRect = null;
        scrollBarCon.contentParentRect = null;
    }
}
