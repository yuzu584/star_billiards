using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���C���J�������Ǘ�
public class CameraController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��
    [SerializeField] private TPSCamera tPSCamera;               // Inspector��TPSCamera���w��
    [SerializeField] private GameObject player;                 // �v���C���[

    private bool chasePlayer = false; // �v���C���[��Ǐ]���Ă��邩�ǂ���

    void Update()
    {
        // �Q�[����ʂȂ�
        if(screenController.screenNum == 0)
        {
            // �v���C���[��Ǐ]���Ă��Ȃ����
            if (!chasePlayer)
            {
                // �v���C���[��Ǐ]
                chasePlayer = true;

                // �J�����̍��W�������
                transform.position = new Vector3(5.0f, 1.0f, 0.0f);

                // �v���C���[�̎q�I�u�W�F�N�g�ɐݒ�
                transform.SetParent(player.transform, false);
            }

            // ���_�ړ�
            tPSCamera.MoveCameraAngle();
        }
        // �X�e�[�W�I����ʂȂ�
        else if (screenController.screenNum == 4)
        {
            // �e�q�֌W������
            chasePlayer = false;
            transform.parent = null;

            // ���W�ƌ�����ύX
            transform.position = new Vector3(0.0f, 50.0f, 0.0f);
            transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
    }
}
