using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �f���̗ʂ��Ǘ�
public class PlanetAmount : MonoBehaviour
{
    public int planetDestroyAmount = 0; // �f����j�󂵂���

    // �f�����j�󂳂ꂽ�ۂ̃|�b�v�A�b�v��`�悷��R���[�`�����Ăяo��
    public void DrawDestroyPlanetPopup(PopupController popupController, string name)
    {
        StartCoroutine(popupController.DrawDestroyPlanetPopup(name + " was destroyed"));
    }
}
