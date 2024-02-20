using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// InputSystem�̓��͂��Ǘ�
public class InputController : MonoBehaviour
{
    private PlayerActions _inputs; // InputActionAsset�̃��b�p�[�N���X

    private void Awake()
    {
        // InputActionAsset�̃��b�p�[�N���X���C���X�^���X��
        _inputs = new PlayerActions();

        // ���͂�L����
        SetInputs(true);
    }

    // ���͏�Ԃ̗L��������ݒ�
    public void SetInputs(bool canInput)
    {
        // ���͂�L����
        if(canInput)
        {
            _inputs.Enable();
        }
        // ���͂𖳌���
        else
        {
            _inputs.Disable();
        }
    }
}
