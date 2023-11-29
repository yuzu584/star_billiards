using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// ���C���J�������Ǘ�
public class CameraController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��
    [SerializeField] private TPSCamera tPSCamera;               // Inspector��TPSCamera���w��
    [SerializeField] private FOV fOV;                           // Inspector��FOV���w��
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

                // ���W�ƌ�����ύX
                transform.position = AppConst.DEFAULT_IN_GAME_POS;
                transform.rotation = Quaternion.Euler(AppConst.DEFAULT_IN_GAME_ANGLE);

                // �v���C���[�̎q�I�u�W�F�N�g�ɐݒ�
                transform.SetParent(player.transform, false);
            }

            // ���_�ړ�
            tPSCamera.MoveCameraAngle();

            // ����p��ύX
            fOV.ChangeFOV();
        }
        // �X�e�[�W�I����ʂȂ�
        else if (screenController.screenNum == 4)
        {
            // �e�q�֌W������
            if (chasePlayer)
                chasePlayer = false;

            if(transform.parent != null)
                transform.parent = null;

            // ���W�ƌ�����ύX
            transform.position = AppConst.DEFAULT_STAGE_SELECT_POS;
            transform.rotation = Quaternion.Euler(AppConst.DEFAULT_STAGE_SELECT_ANGLE);

            // ����p�����Z�b�g
            fOV.ResetFOV();
        }
    }
}
