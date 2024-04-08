using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �w�i�F���Ǘ�
public class BackGroundColor : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Material material;
    [SerializeField] private Transform parentT;
    private BackGroundEffect bgEffect;

    // �F�������_���Őݒ�
    private void Start()
    {
        bgEffect = BackGroundEffect.instance;

        // �w�i�̃G�t�F�N�g�𐶐�
        bgEffect.DrawEffect(parentT);

        // �w�i�� ButtomColor ��ݒ�
        material.SetColor("_TopColor", bgEffect.mainColor);
        material.SetColor("_ButtomColor", bgEffect.buttomColor);
    }
}
