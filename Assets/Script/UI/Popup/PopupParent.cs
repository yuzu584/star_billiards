using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �|�b�v�A�b�v�̐e�N���X
public class PopupParent : MonoBehaviour
{
    protected Lerp lerp;

    protected virtual void Start()
    {
        // Lerp ���A�^�b�`
        lerp ??= gameObject.AddComponent<Lerp>();
    }

    // �|�b�v�A�b�v�̏���
    public virtual IEnumerator Process(string text, Transform parentT, int num)
    {
        Debug.Log("�|�b�v�A�b�v�̏������ݒ肳��Ă��܂���");
        yield return null;
    }
}
