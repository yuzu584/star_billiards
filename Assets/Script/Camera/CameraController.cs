using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// ���C���J�������Ǘ�
public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player; // �v���C���[

    private ScreenController scrCon;
    private FOV fov;

    private bool chasePlayer = false;           // �v���C���[��Ǐ]���Ă��邩�ǂ���
    
    void Start()
    {
        scrCon = ScreenController.instance;
        fov = FOV.instance;
    }

    void Update()
    {
        // �Q�[����ʂȂ�
        if(scrCon.Screen == ScreenController.ScreenType.InGame)
        {
            // �v���C���[��Ǐ]���Ă��Ȃ����
            if (!chasePlayer)
            {
                // �v���C���[��Ǐ]
                chasePlayer = true;

                // ���W�ƌ�����ύX
                transform.position = AppConst.CAMERA_DEFAULT_IN_GAME_POS;
                transform.rotation = Quaternion.Euler(AppConst.CAMERA_DEFAULT_IN_GAME_ANGLE);

                // �v���C���[�̎q�I�u�W�F�N�g�ɐݒ�
                transform.SetParent(player.transform, false);
            }

            // ����p��ύX
            fov.ChangeFOV();
        }
        // �X�e�[�W�I����ʂȂ�
        else if (scrCon.Screen == ScreenController.ScreenType.StageSelect)
        {
            // �e�q�֌W������
            if (chasePlayer)
                chasePlayer = false;

            if(transform.parent != null)
                transform.parent = null;

            // ���W�ƌ�����ύX
            transform.position = AppConst.DEFAULT_STAGE_SELECT_POS;
            transform.rotation = Quaternion.Euler(AppConst.DEFAULT_STAGE_SELECT_ANGLE);
        }
    }
}
