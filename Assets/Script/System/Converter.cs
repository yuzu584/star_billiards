using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ϊ�����
public class Converter : MonoBehaviour
{
    [SerializeField]
    private RectTransform canvasRect; // �L�����o�X��RectTransform

    // ���[���h���W���X�N���[�����W���r���[�|�[�g���W�ɕϊ����ĕԂ�
    // Screen Space�� Camera�̎��Ɏg�p����
    // WSV(Would Screen ViewPort)
    public Vector2 WSVConvert(Vector3 targetPos)
    {
        Vector2 pos = Vector2.zero;
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, Camera.main, out pos);
        return pos;
    }
}
