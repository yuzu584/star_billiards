using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �f���̕��������������Ǘ�
public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;  // ���̃v���n�u
    [SerializeField] private GameObject player; // �v���C���[

    // �����쐬
    public void Create(GameObject target)
    {
        // ���̃C���X�^���X�𐶐�
        GameObject arrowObj = Instantiate(arrow);

        // �e��ݒ�
        arrowObj.transform.SetParent(player.transform, false);

        // �^�[�Q�b�g���w��
        LookAt lookTarget = arrowObj.GetComponent<LookAt>();
        lookTarget.target = target;
    }
}
