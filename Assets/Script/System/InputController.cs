using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// InputSystemの入力を管理
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
        // インスタンスを生成
        inputs = new PlayerActions();
    }

    void Start()
    {
        // 画面遷移時に入力状態の有効無効を設定する
        screenController.changeScreen += SetInputs;
    }

    // 入力状態の有効無効を設定
    public void SetInputs()
    {
        // 入力を有効化
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
        // 入力を無効化
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
