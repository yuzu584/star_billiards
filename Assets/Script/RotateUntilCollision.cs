using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���]������
public class RotateUntilCollision : MonoBehaviour
{
    public float speed = 1f;  // ��]���x
    float x = 0f;             // �}�e���A���̃I�t�Z�b�g��X�̐��l
    int maxX = 1;             // �}�e���A���̃I�t�Z�b�g��X�̍ő�l

    // �Փ˔��肪�I�������
    void OnCollisionExit(Collision collision)
    {
        // �ő�l��0�ɂ���
        maxX = 0;
    }

    void Update()
    {
        // �܂��Փ˂��Ă��Ȃ����
        if (maxX == 1)
        {
            // �}�e���A���̃I�t�Z�b�g��X�̐��l�𑝉�
            x += speed * Time.deltaTime * 0.01f;

            // x�̐��l���ő�l�𒴂�����
            if (x > maxX)
            {
                // x�̐��l��0�ɖ߂�
                x = 0f;
            }

            // �}�e���A���̃I�t�Z�b�g���X�V
            GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", new Vector2(x, 0f));
        }
    }
}
