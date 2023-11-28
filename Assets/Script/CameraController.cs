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

                // �J�����̈ʒu�������
                transform.position = new Vector3(5.0f, 1.0f, 0.0f);

                // �v���C���[�̎q�I�u�W�F�N�g�ɐݒ�
                transform.SetParent(player.transform, false);
            }

            // ���_�ړ�
            tPSCamera.MoveCameraAngle();
        }
    }
}
