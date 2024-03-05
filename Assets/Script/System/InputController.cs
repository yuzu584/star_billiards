using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// InputSystem�̓��͂��Ǘ�
public class InputController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController;
    [SerializeField] private ScreenData screenData;

    private PlayerActions inputs; // InputSystem
    public Vector2 Game_Move;
    public Vector2 Game_Look;
    public float Game_Shot;
    public float Game_Aim;
    public float Game_UseSkill;
    public float Game_ChangeSkill;
    public float Game_Pause;

    void Update()
    {
        Game_Move = inputs.Game.Move.ReadValue<Vector2>();
        Game_Look = inputs.Game.Look.ReadValue<Vector2>();
        Game_Shot = inputs.Game.Shot.ReadValue<float>();
        Game_Aim = inputs.Game.Aim.ReadValue<float>();
        Game_UseSkill = inputs.Game.UseSkill.ReadValue<float>();
        Game_ChangeSkill = inputs.Game.ChangeSkill.ReadValue<float>();
        Game_Pause = inputs.Game.Pause.ReadValue<float>();
    }

    private void Awake()
    {
        // �C���X�^���X�𐶐�
        inputs = new PlayerActions();
    }

    void Start()
    {
        // ��ʑJ�ڎ��ɓ��͏�Ԃ̗L��������ݒ肷��
        screenController.changeScreen += SetInputs;
    }

    // ���͏�Ԃ̗L��������ݒ�
    public void SetInputs()
    {
        // ���͂�L����
        switch (screenData.screenList[screenController.ScreenNum].inputType)
        {
            case -1:
                inputs.Enable();
                break;
            case 0:
                inputs.Game.Enable();
                break;
            case 1:
                inputs.UI.Enable();
                break;
        }
        // ���͂𖳌���
        switch (screenData.screenList[screenController.oldScreenNum].inputType)
        {
            case -1:
                inputs.Disable();
                break;
            case 0:
                inputs.Game.Disable();
                break;
            case 1:
                inputs.UI.Disable();
                break;
        }
    }
}
